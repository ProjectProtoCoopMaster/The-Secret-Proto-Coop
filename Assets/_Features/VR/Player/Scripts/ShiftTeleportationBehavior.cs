using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Valve.VR;

namespace Gameplay.VR.Player
{
    public class ShiftTeleportationBehavior : AgentStateData
    {
        // shift teleportation behavior
        [SerializeField] protected float movementSpeed;

        [SerializeField] float minDashRange = 0.5f, maxDashRange = 40f, dashTime = 0.2f;

        [SerializeField] Animator maskAnimator;

        SteamVR_Action teleportAction;
        Transform cameraRigRoot;

        private void Start()
        {
            
        }

        IEnumerator Dashing()
        {
            float elapsed = 0f;
            Vector3 startPoint = cameraRigRoot.position;

            while (elapsed > dashTime)
            {
                elapsed += Time.deltaTime;
                float elapsedPct = elapsed / Time;

                cameraRigRoot.position = Vector3.Lerp(startPoint, )

            }
        }

    }
}