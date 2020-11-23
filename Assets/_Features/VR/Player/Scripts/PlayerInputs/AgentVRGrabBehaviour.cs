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

        private void Start()
        {
            grabAction.AddOnStateDownListener(Pickup, handSource);
            grabAction.AddOnStateUpListener(Release, handSource);
            thisRb = GetComponent<Rigidbody>();
        }

        private void Pickup(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            if (controllerPose == null)
                controllerPose = GetComponent<SteamVR_Behaviour_Pose>();

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
            if (controllerPose == null) controllerPose = GetComponent<SteamVR_Behaviour_Pose>();
            Debug.Log("FLY");
            holdingTarget.isKinematic = false;
            holdingTarget.transform.SetParent(null);
            holdingTarget.velocity = controllerPose.GetVelocity();
            holdingTarget.angularVelocity = controllerPose.GetAngularVelocity();

            holdingTarget = null;
        }
    }
}