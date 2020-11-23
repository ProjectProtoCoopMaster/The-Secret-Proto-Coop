﻿//#define isDebugging
using System.Collections;
using UnityEngine;
using Valve.VR;
using Sirenix.OdinInspector;

namespace Gameplay.VR.Player
{
    public class AgentTeleportationManager : MonoBehaviour
    {
        [SerializeField] [FoldoutGroup("Teleportation Transition")] float tweenDuration = .25f;
        [SerializeField] [FoldoutGroup("Teleportation Transition")] ParticleSystem particleDash;
        [SerializeField] [FoldoutGroup("Teleportation Transition")] TweenFunctions tweenFunction;
        TweenManagerLibrary.TweenFunction delegateTween;
        Vector3 movingPosition, change;
        float time;

        [SerializeField] [FoldoutGroup("SteamVR Components")] internal Transform playerHead;
        [SerializeField] [FoldoutGroup("SteamVR Components")] Transform cameraRig;
        SteamVR_Behaviour_Pose controllerPose;
        private bool showRayPointer = false;

        // [SerializeField] [FoldoutGroup("Teleportation")] Transform startPoint, endPoint;
        [SerializeField] [FoldoutGroup("Teleportation")] float maxDistance = 10f;
        [SerializeField] [FoldoutGroup("Teleportation")] float castingHeight = 2f;
        [SerializeField] [FoldoutGroup("Teleportation")] float minControllerAngle = 30f, maxControllerAngle = 150f;

        [SerializeField] [FoldoutGroup("Teleportation Pointer")] LineRenderer bezierVisualization;
        [SerializeField] [FoldoutGroup("Teleportation Pointer")] float lineWidth;
        [SerializeField] [FoldoutGroup("Teleportation Pointer")] int smoothness;
        Vector3 posContainer;
        Vector3 p0, p1, p2;
        float t;

        Ray horizontalRay, tallRay;
        RaycastHit hitTallRay, hitHorizontal;
        GameObject pointer;
        internal Transform pointerOrigin;

        private void Awake()
        {
            pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pointer.GetComponent<Collider>().enabled = false;

            bezierVisualization.startWidth = lineWidth;
            bezierVisualization.endWidth = lineWidth;
            bezierVisualization.useWorldSpace = true;
            bezierVisualization.positionCount = smoothness;
        }

        private void Start()
        {
            delegateTween = TweenManagerLibrary.GetTweenFunction((int)tweenFunction);
        }

        private void Update()
        {
            if (showRayPointer)
                ShowRayPointer();
        }

        #region Tall Ray Variables
        Vector3 controllerForward
        {
            get
            {
#if isDebugging
                return playerHead.transform.forward;
#else 
                return controllerPose.transform.forward;
#endif
            }
        }

        public float horizontalDistance
        {
            get
            {
                float controllerAngle = Vector3.Angle(Vector3.up * -1.0f, controllerForward);
                float pitch = Mathf.Clamp(controllerAngle, minControllerAngle, maxControllerAngle);
                float pitchRange = maxControllerAngle - minControllerAngle;
                float t = (pitch - minControllerAngle) / pitchRange; // Normalized pitch within range
                return maxDistance * t;
            }
        }

        // project a Ray on a plane, determined by the angle of the controller's forward
        Vector3 horizontalDirection
        {
            get
            {
                return Vector3.ProjectOnPlane(controllerForward, Vector3.up);
            }
        }

        // you must take into account the player's distance from the center
        Vector3 difference
        {
            get
            {
                return cameraRig.position - playerHead.position;
            }
        }

        Vector3 teleportTarget
        {
            get
            {
                return pointer.transform.position + difference;
            }
        }

        // player position in world coords
        Vector3 playerPosition
        {
            get
            {
                return playerHead.position;
            }
        }

        // determine where along the ray the max distance point is
        Vector3 pointAlongRay
        {
            get
            {
                return horizontalRay.origin + horizontalDirection * horizontalDistance;
            }
        }

        // determine the tallRay's casting position
        Vector3 castingPosition
        {
            get
            {
                return playerHead.position + Vector3.up * castingHeight;
            }
        }
        #endregion

        // 1. fire a ray pointing in the direction of the controller
        // 2. find a point on the horizontalRay based on the normalized pitch (defined in a range, eg. 60° to 120°) of the controller * maxDistance
        // 3. fire a raycast from above the player. Substract the point on the horiz ray from the new ray's position and normalize the result to get the direction
        // 4. if anything is hit by the second ray, it is the endpoint, otherwise, the end point will be somewhere on the horizontal axis.
        // 5. render a bézier curve between the controller and the end point

        // show the pointer, using the referenced controller's transform.forward
        internal void TallRayPointer(SteamVR_Behaviour_Pose _controllerPose)
        {
            controllerPose = _controllerPose;
            showRayPointer = true;
        }

        void ShowRayPointer()
        {

            // assign the Ray values
#if isDebugging
            horizontalRay.origin = pointerOrigin.position;
#else 
            horizontalRay.origin = controllerPose.transform.position;
#endif
            horizontalRay.direction = horizontalDirection;

            tallRay.origin = castingPosition;
            tallRay.direction = (pointAlongRay - castingPosition).normalized;

            // if you hit something with the Tall Ray, define it as the endpoint
            if (Physics.Raycast(tallRay, out hitTallRay))
                pointer.transform.position = hitTallRay.point;

            // otherwise, the endpoint must be on the horizontal axis
            else if (Physics.Raycast(horizontalRay, out hitHorizontal))
                pointer.transform.position = hitHorizontal.point;

            Debug.DrawRay(horizontalRay.origin, horizontalRay.direction * 20f, Color.blue); // draw the initial horizontal Ray
            Debug.DrawLine(horizontalRay.origin, pointAlongRay, Color.white); // draw the teleportation distance limit
            Debug.DrawRay(tallRay.origin, tallRay.direction * 50f, Color.red); // draw the Tall Ray shooting down

            #region Bézier Curve
            bezierVisualization.enabled = true;

            p1 = p2 = pointer.transform.position;

#if isDebugging
            p0 = pointerOrigin.transform.position;
            p1.y = pointerOrigin.position.y;
#else
            p0 = controllerPose.transform.position;
            p1.y = controllerPose.transform.position.y;
#endif
            for (int i = 0; i < smoothness; i++)
            {
                t = i / (smoothness - 1.0f);
                posContainer = (1.0f - t) * (1.0f - t) * p0
                + 2.0f * (1.0f - t) * t * p1 + t * t * p2;
                bezierVisualization.SetPosition(i, posContainer);
            }
            #endregion                       
        }

        public void TryTeleporting()
        {
            StartCoroutine(TeleportThePlayer());
            bezierVisualization.enabled = false;
        }

        IEnumerator TeleportThePlayer()
        {
            Debug.Log("Teleporting");
            Vector3 startPos = playerPosition, targetPos = teleportTarget;
            showRayPointer = false;

            particleDash.Play();

            time = 0;
            change = targetPos - startPos;

            // don't change yPos
            movingPosition.y = startPos.y;

            while (time <= tweenDuration)
            {
                time += Time.deltaTime;
                movingPosition.x = delegateTween(time, startPos.x, change.x, tweenDuration);
                movingPosition.z = delegateTween(time, startPos.z, change.z, tweenDuration);
                movingPosition.y = cameraRig.position.y;
                cameraRig.position = movingPosition;
                yield return null;
            }
            particleDash.Stop();
        }
    }
}