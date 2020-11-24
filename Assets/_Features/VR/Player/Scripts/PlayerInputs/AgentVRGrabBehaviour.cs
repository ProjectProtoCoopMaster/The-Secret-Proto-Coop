﻿using UnityEngine;
using Valve.VR;
using Sirenix.OdinInspector;

namespace Gameplay.VR.Player
{
    public class AgentVRGrabBehaviour : MonoBehaviour
    {
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Input_Sources handSource;
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Action_Pose controllerPose = SteamVR_Input.GetAction<SteamVR_Action_Pose>("Pose");
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Action_Boolean grabAction;

        [SerializeField] [FoldoutGroup("Testing Data")] float disToPickup = .3f;
        [SerializeField] [FoldoutGroup("Testing Data")] LayerMask pickupLayer;
        [SerializeField] [FoldoutGroup("Testing Data")] Rigidbody holdingTarget;
        [SerializeField] [FoldoutGroup("SteamVR Components")] Transform controllerPosition;
               
        private void Awake()
        {
            controllerPosition = this.transform;
            grabAction.AddOnStateDownListener(TryPickup, handSource);
            grabAction.AddOnStateUpListener(Release, handSource);
        }

        internal void TryPickup(SteamVR_Action_Boolean action, SteamVR_Input_Sources fromSource)
        {
            Collider[] colliders = Physics.OverlapSphere(controllerPosition.position, disToPickup, pickupLayer);
            if (colliders.Length > 0) holdingTarget = colliders[0].transform.GetComponent<Rigidbody>();

            if (holdingTarget != null)
            {
                holdingTarget.transform.parent = controllerPosition;
                holdingTarget.isKinematic = true;
            }
        }

        void Release(SteamVR_Action_Boolean action, SteamVR_Input_Sources fromSource)
        {
            Debug.Log("FLY");
            holdingTarget.isKinematic = false;
            holdingTarget.velocity = controllerPose[handSource].velocity;
            holdingTarget.angularVelocity = controllerPose[handSource].angularVelocity;

            holdingTarget.transform.SetParent(null);
            holdingTarget = null;
        }
    }
}