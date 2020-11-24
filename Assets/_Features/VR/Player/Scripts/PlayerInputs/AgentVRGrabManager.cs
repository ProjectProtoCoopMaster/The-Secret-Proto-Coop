using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Valve.VR;

namespace Gameplay.VR.Player
{
    public class AgentVRGrabManager : MonoBehaviour
    {
        [SerializeField] [FoldoutGroup("Testing Data")] float disToPickup = .3f;
        [SerializeField] [FoldoutGroup("Testing Data")] LayerMask pickupLayer;
        [SerializeField] [FoldoutGroup("Testing Data")] Rigidbody holdingTarget;

        internal void TryPickup(Transform controllerPos)
        {
            //controllerPose = _controllerPose;

            Collider[] colliders = Physics.OverlapSphere(controllerPos.position, disToPickup, pickupLayer);
            if (colliders.Length > 0) holdingTarget = colliders[0].transform.GetComponent<Rigidbody>();

            if (holdingTarget != null)
            {
                holdingTarget.transform.parent = controllerPos;
                holdingTarget.isKinematic = true;
            }
        }

        internal void TryRelease(Vector3 velocity, Vector3 angularVelocity)
        {
            Debug.Log("FLY");
            holdingTarget.isKinematic = false;
            holdingTarget.velocity = velocity;
            holdingTarget.angularVelocity = angularVelocity;

            holdingTarget.transform.SetParent(null);
            holdingTarget = null;
        }
    }
}