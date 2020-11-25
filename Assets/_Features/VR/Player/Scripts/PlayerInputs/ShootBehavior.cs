#if UNITY_STANDALONE
using Sirenix.OdinInspector;
using UnityEngine;
using Valve.VR;

namespace Gameplay.VR.Player
{
    public class ShootBehavior : MonoBehaviour
    {
        [SerializeField] [FoldoutGroup("Shooting")] LayerMask shootingLayer;
        [SerializeField] [FoldoutGroup("Shooting")] ParticleSystem shotTrail;
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Action_Pose controllerPose = SteamVR_Input.GetAction<SteamVR_Action_Pose>("Pose");
        [SerializeField] [FoldoutGroup("SteamVR Components")] Transform controllerPosition;

        RaycastHit hit;

        // a reference to the action
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Action_Boolean shootAction;
        // a reference to the hand
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Input_Sources handType;

        private void Start()
        {
            controllerPosition = this.transform;
            shootAction.AddOnStateUpListener(TriggerRelease, handType);
        }

        private void TriggerRelease(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            shotTrail.transform.position = controllerPosition.position;
            shotTrail.transform.rotation = controllerPosition.rotation;

            shotTrail.Play();
            Debug.Log("Shooting");
            Debug.DrawRay(controllerPosition.position, controllerPosition.forward * 50f, Color.magenta);

            if (Physics.SphereCast(controllerPosition.position, 0.25f, controllerPosition.forward, out hit, 100f, shootingLayer))
            {
                if (hit.collider.gameObject.CompareTag("Guard"))
                    hit.collider.GetComponent<GuardBehaviour>().Shot();

                else Debug.Log("Bullet hit " + hit.collider.name);
            }
        }
    }
} 
#endif