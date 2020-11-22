using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Valve.VR;

namespace Gameplay.VR.Player
{
    public class AgentVRTeleportationBehaviour : MonoBehaviour
    {
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Input_Sources handType;
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Behaviour_Pose controllerPose;
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Action_Boolean teleportAction;
        [SerializeField] [FoldoutGroup("Manager")] AgentVRTeleportationManager manager;

        private void Awake()
        {
            teleportAction.AddOnStateDownListener(ShowLaserPointer, handType);
            teleportAction.AddOnStateUpListener(Teleport, handType);
        }

        void ShowLaserPointer(SteamVR_Action_Boolean action, SteamVR_Input_Sources source)
        {
            manager.TallRayPointer(controllerPose);
        }

        void Teleport(SteamVR_Action_Boolean action, SteamVR_Input_Sources source)
        {
            manager.TryTeleporting();
        }
    }
}