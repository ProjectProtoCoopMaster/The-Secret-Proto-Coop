using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Gameplay.VR.Player
{
    public class AgentVRTeleportation : MonoBehaviour
    {
        float time;
        Vector3 change;
        Vector3 movingPosition;
        [SerializeField] [FoldoutGroup("Teleportation Transition")] float tweenDuration = .25f;
        [SerializeField] [FoldoutGroup("Teleportation Transition")] ParticleSystem particleDash;
        ParticleSystem.MainModule main; // assigned in awake

        // a reference to the teleportation input
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Action_Boolean teleportAction;
        // a reference to the hand you are using
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Input_Sources handType;
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Behaviour_Pose pose = null;
        SteamVR_Behaviour_Skeleton skeleton;

        Ray horizontalRay, tallRay;
        Vector3 horizontalDirection, playerPosition;
        [SerializeField] [FoldoutGroup("Teleportation")] Transform startPoint, endPoint;
        [SerializeField] [FoldoutGroup("Teleportation")] float maxTeleportationDistance = 10f;
        [SerializeField] [FoldoutGroup("Teleportation")] float castingHeight = 2f;
        [SerializeField] [FoldoutGroup("Teleportation")] GameObject pointer;

        private void Awake()
        {
            //pose = GetComponent<SteamVR_Behaviour_Pose>();
            //teleportAction.AddOnStateUpListener(TryTeleport, handType);
            pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pointer.GetComponent<Collider>().enabled = false;
        }

        void TryTeleport(SteamVR_Action_Boolean action, SteamVR_Input_Sources source)
        {
            TallRayPointer();
        }

        private void Update()
        {
            TallRayPointer();
        }

        // uses the "Tall Raycast" method
        private void TallRayPointer()
        {
            // fire a ray pointing in the direction of the controller
            // find a point on the horizontalRay based on the normalized pitch (defined in a range, eg. 60° to 120°) of the controller * maxDistance
            // fire a raycast from above the player. Substract the point on the horiz ray from the new ray's position and normalize the result to get the direction
            // if anything is hit by the second ray, it is the endpoint, otherwise, the end point will be somewhere on the horizontal axis.
            // render a bézier curve between the controller and the end point

            // project a Ray on a plane
            horizontalDirection = Vector3.ProjectOnPlane(Vector3.forward, Vector3.up);
            playerPosition = transform.position;

            // assign the Ray values
            horizontalRay.origin = playerPosition;
            horizontalRay.direction = horizontalDirection;
            
            // determine where along the ray the max distance point is
            Vector3 pointAlongRay = horizontalRay.origin + horizontalDirection*maxTeleportationDistance;

            // determine the tallRay's casting position
            Vector3 castingPosition = playerPosition + Vector3.up * castingHeight;

            tallRay.origin = castingPosition;
            tallRay.direction = (pointAlongRay - castingPosition).normalized;

            if (Physics.Raycast(tallRay, out RaycastHit hitTall))
            {
                pointer.transform.position = hitTall.point;
            }
            else if (Physics.Raycast(horizontalRay, out RaycastHit hitHoriz))
            {
                pointer.transform.position = hitHoriz.point;
            }

            Debug.DrawRay(horizontalRay.origin, horizontalRay.direction * 20f, Color.blue); // draw the initial horizontal Ray
            Debug.DrawLine(horizontalRay.origin, pointAlongRay, Color.white); // draw the teleportation distance limit
            Debug.DrawRay(tallRay.origin, tallRay.direction * 50f, Color.red); // draw the Tall Ray shooting down
        }

        IEnumerator TeleportThePlayer(Vector3 startPos, Vector3 targetPos)
        {
            particleDash.Play();

            time = 0;
            change = targetPos - startPos;

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