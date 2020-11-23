using Sirenix.OdinInspector;
using UnityEngine;
using Valve.VR;

namespace Gameplay.VR.Player
{
    public class ShootBehavior : MonoBehaviour
    {
        [SerializeField] [FoldoutGroup("Shooting Info")] LayerMask shootingLayer;

        RaycastHit hit;

        // a reference to the action
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Behaviour_Pose controllerPose;
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Action_Boolean shootAction;
        // a reference to the hand
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Input_Sources handType;


        private void Start()
        {
            shootAction.AddOnStateUpListener(TriggerRelease, handType);
        }
        
        private void TriggerRelease(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            if (controllerPose == null)
                controllerPose = FindObjectOfType<SteamVR_Behaviour_Pose>();

            Debug.DrawRay(controllerPose.transform.position, controllerPose.transform.forward * 50f, Color.magenta);
            if (Physics.SphereCast(controllerPose.transform.position, 0.25f, controllerPose.transform.forward, out hit, 100f, shootingLayer))
            {
                if (hit.collider.gameObject.CompareTag("Guard"))
                {
                    hit.collider.GetComponent<GuardMortalityBehavior>().Shot();
                }                    

                else Debug.Log("Bullet hit " + hit.collider.name);
            }
        }
    }
}