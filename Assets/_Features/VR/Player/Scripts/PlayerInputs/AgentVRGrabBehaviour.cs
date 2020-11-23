using UnityEngine;
using Valve.VR;
using Sirenix.OdinInspector;
using System;

namespace Gameplay.VR.Player
{
    public class AgentVRGrabBehaviour : MonoBehaviour
    {
        [SerializeField] [FoldoutGroup("Testing Data")] float disToPickup = .3f;
        [SerializeField] [FoldoutGroup("Testing Data")] LayerMask pickupLayer;
        [SerializeField] [FoldoutGroup("Testing Data")] Rigidbody holdingTarget, thisRb;

        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Input_Sources handSource;
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Behaviour_Pose controllerPose;
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Action_Boolean grabAction;

        [SerializeField]
        [FoldoutGroup("Testing Data")]
        bool handClosed
        {
            get
            {
                return grabAction.GetStateDown(handSource);
            }
        }

        private void Awake()
        {
            grabAction.AddOnStateDownListener(Pickup, handSource);
            grabAction.AddOnStateUpListener(Release, handSource);
            thisRb = GetComponent<Rigidbody>();
        }

        private void Pickup(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            Collider[] colliders = Physics.OverlapSphere(controllerPose.transform.position, disToPickup, pickupLayer);
            if (colliders.Length > 0)
                holdingTarget = colliders[0].transform.root.GetComponent<Rigidbody>();

            if (holdingTarget != null)
            {
                holdingTarget.transform.parent = controllerPose.transform;
                holdingTarget.isKinematic = true;
            }
        }

        private void Release(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            Debug.Log("FLY");
            holdingTarget.isKinematic = false;
            holdingTarget.transform.SetParent(null);
            holdingTarget.velocity = controllerPose.GetVelocity();
            holdingTarget.angularVelocity = controllerPose.GetAngularVelocity();

            holdingTarget = null;
        }

        private void FixedUpdate()
        {
            if (controllerPose == null) controllerPose = GetComponent<SteamVR_Behaviour_Pose>();


            /*else
            {
                if (holdingTarget)
                {
                    // ajdust velocity to move around
                    holdingTarget.velocity = (controllerPose.transform.position - holdingTarget.transform.position) / Time.fixedDeltaTime;

                    // ajust velocity to rotate to hand
                    holdingTarget.maxAngularVelocity = 20;
                    Quaternion deltRot = transform.rotation * Quaternion.Inverse(holdingTarget.transform.rotation);
                    Vector3 eulerRot = new Vector3(Mathf.DeltaAngle(0, deltRot.eulerAngles.x), Mathf.DeltaAngle(0, deltRot.eulerAngles.y), Mathf.DeltaAngle(0, deltRot.eulerAngles.z));
                    eulerRot *= 0.95f;
                    eulerRot *= Mathf.Deg2Rad;
                    holdingTarget.angularVelocity = eulerRot / Time.fixedDeltaTime;
                }
            }*/
        }
    }
}