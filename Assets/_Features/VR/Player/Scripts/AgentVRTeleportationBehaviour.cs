﻿using UnityEngine;
using Valve.VR;
using Sirenix.OdinInspector;

namespace Gameplay.VR.Player
{
    public class AgentVRTeleportationBehaviour : MonoBehaviour
    {
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Input_Sources handType;
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Behaviour_Pose controllerPose;
        [SerializeField] [FoldoutGroup("SteamVR Components")] SteamVR_Action_Boolean teleportAction;
        [SerializeField] [FoldoutGroup("Manager")] AgentTeleportationManager teleportationManager;

        private void Start()
        {
            teleportAction.AddOnStateDownListener(ShowLaserPointer, handType);
            teleportAction.AddOnStateUpListener(Teleport, handType);
        }

        void ShowLaserPointer(SteamVR_Action_Boolean action, SteamVR_Input_Sources source)
        {
           if(teleportationManager == null) 
                teleportationManager = FindObjectOfType<AgentTeleportationManager>();

            teleportationManager.TallRayPointer(controllerPose);
        }

        void Teleport(SteamVR_Action_Boolean action, SteamVR_Input_Sources source)
        {
            if (teleportationManager == null) 
                teleportationManager = FindObjectOfType<AgentTeleportationManager>();

            teleportationManager.TryTeleporting();
        }
    }
}