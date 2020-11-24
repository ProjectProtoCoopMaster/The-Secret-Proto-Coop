using UnityEngine;
using Valve.VR;
using Sirenix.OdinInspector;

namespace Gameplay.VR.Player
{
    public class AgentVRGrabBehaviour : MonoBehaviour
    {
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Input_Sources handSource;
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Action_Pose controllerPose = SteamVR_Input.GetAction<SteamVR_Action_Pose>("Pose");
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Action_Boolean grabAction;

        [SerializeField] [FoldoutGroup("Manager")] AgentVRGrabManager grabManager;
        public Transform currentPos;

        private void Awake()
        {
            grabAction.AddOnStateDownListener(Pickup, handSource);
            grabAction.AddOnStateUpListener(Release, handSource);
        }

        void Pickup(SteamVR_Action_Boolean action, SteamVR_Input_Sources fromSource)
        {
            if (grabManager == null)
                grabManager = FindObjectOfType<AgentVRGrabManager>();
            /*if (controllerPose == null) 
                controllerPose = GetComponent<SteamVR_Behaviour_Pose>();*/

            grabManager.TryPickup(currentPos);
        }

        void Release(SteamVR_Action_Boolean action, SteamVR_Input_Sources fromSource)
        {
            if (grabManager == null)
                grabManager = FindObjectOfType<AgentVRGrabManager>();
            /*if (controllerPose == null) 
                controllerPose = GetComponent<SteamVR_Behaviour_Pose>();*/

            grabManager.TryRelease(controllerPose[handSource].velocity, controllerPose[handSource].angularVelocity);

        }

        private void Update()
        {
            //currentPos = this.transform.parent;
        }
    }
}