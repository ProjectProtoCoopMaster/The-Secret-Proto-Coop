using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace Gameplay.VR.Player
{
    public class AgentVRTeleportation : MonoBehaviour
    {
        float time;
        Vector3 change;
        Vector3 movingPosition;
        [SerializeField] [FoldoutGroup("Teleportation Transition")] float tweenDuration = .25f;
        [SerializeField] [FoldoutGroup("Teleportation Transition")] ParticleSystem particleDash;

        // a reference to the teleportation input
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Action_Boolean teleportAction;
        // a reference to the hand you are using
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Input_Sources handType;

        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Action_Pose leftHandPose = null;
        [SerializeField] [FoldoutGroup("SteamVR Components")] Transform cameraRig;

        // [SerializeField] [FoldoutGroup("Teleportation")] Transform startPoint, endPoint;
        [SerializeField] [FoldoutGroup("Teleportation")] float maxDistance = 10f;
        [SerializeField] [FoldoutGroup("Teleportation")] float castingHeight = 2f;
        [SerializeField] [FoldoutGroup("Teleportation")] float minControllerAngle = 30f, maxControllerAngle = 150f;
        [SerializeField] [FoldoutGroup("Teleportation")] LineRenderer bezierVisualization;

        Ray horizontalRay, tallRay;
        RaycastHit hitTallRay, hitHorizontal;
        Quaternion controllerRotation;

        GameObject pointer;

        private void Awake()
        {
            teleportAction.AddOnStateUpListener(TryTeleport, handType);
            pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pointer.GetComponent<Collider>().enabled = false;
        }

        void TryTeleport(SteamVR_Action_Boolean action, SteamVR_Input_Sources source)
        {            
            TallRayPointer();
        }

        private void Update()
        {
            //TallRayPointer();
            if (Input.GetKey(KeyCode.Space)) TallRayPointer();
            if (Input.GetKeyUp(KeyCode.Space)) StartCoroutine(TeleportThePlayer(playerPosition, pointer.transform.position));
        }

        #region Tall Ray Variables

        public Vector3 getControllerPosition()
        {
            SteamVR_Action_Pose[] poseActions = SteamVR_Input.actionsPose;
            if (poseActions.Length > 0)
            {
                return poseActions[0].GetLocalPosition(handType);
            }
            return new Vector3(0, 0, 0);
        }

        public Quaternion getControllerRotation()
        {
            SteamVR_Action_Pose[] poseActions = SteamVR_Input.actionsPose;
            if (poseActions.Length > 0)
            {
                return poseActions[0].GetLocalRotation(handType);
            }
            return Quaternion.identity;
        }

        public Vector3 controllerForward
        {
            get
            {
                /*
                controllerRotation = leftHandPose.GetLocalRotation(handType);
                Debug.Log("Local Controller Rotation is " + controllerRotation * Vector3.forward);*/
                return getControllerRotation() * Vector3.forward;
                //return Vector3.forward;
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


        Vector3 playerPosition
        {
            get
            {
                return cameraRig.position;
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
                return playerPosition + Vector3.up * castingHeight;
            }
        }
        #endregion

        // 1. fire a ray pointing in the direction of the controller
        // 2. find a point on the horizontalRay based on the normalized pitch (defined in a range, eg. 60° to 120°) of the controller * maxDistance
        // 3. fire a raycast from above the player. Substract the point on the horiz ray from the new ray's position and normalize the result to get the direction
        // 4. if anything is hit by the second ray, it is the endpoint, otherwise, the end point will be somewhere on the horizontal axis.
        // 5. render a bézier curve between the controller and the end point
        private void TallRayPointer()
        {
            //skeleton.GetBonePosition();

            if(leftHandPose != null)
            {
                // assign the Ray values
                horizontalRay.origin = playerPosition;
                horizontalRay.direction = horizontalDirection;

                tallRay.origin = castingPosition;

                tallRay.direction = (pointAlongRay - castingPosition).normalized;

                // if you hit something with the Tall Ray, define it as the endpoint
                if (Physics.Raycast(tallRay, out hitTallRay))
                {
                    pointer.transform.position = hitTallRay.point;
                }
                // otherwise, the endpoint must be on the horizontal axis
                else if (Physics.Raycast(horizontalRay, out hitHorizontal))
                {
                    pointer.transform.position = hitHorizontal.point;
                }
            }

            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    pointer.transform.position = hit.point;
                }
            }

            bezierVisualization.SetPosition(0, playerPosition);
            bezierVisualization.SetPosition(1, pointer.transform.position);

            Debug.DrawRay(horizontalRay.origin, horizontalRay.direction * 20f, Color.blue); // draw the initial horizontal Ray
            Debug.DrawLine(horizontalRay.origin, pointAlongRay, Color.white); // draw the teleportation distance limit
            Debug.DrawRay(tallRay.origin, tallRay.direction * 50f, Color.red); // draw the Tall Ray shooting down
        }

        IEnumerator TeleportThePlayer(Vector3 startPos, Vector3 targetPos)
        {
            particleDash.Play();

            time = 0;
            change = targetPos - startPos;

            // don't change yPos
            movingPosition.y = startPos.y;

            while (time <= tweenDuration)
            {
                time += Time.deltaTime;
                movingPosition.x = TweenManager.LinearTween(time, startPos.x, change.x, tweenDuration);
                movingPosition.z = TweenManager.LinearTween(time, startPos.z, change.z, tweenDuration);
                transform.position = movingPosition;
                yield return null;
            }
            particleDash.Stop();
        }
    }

}