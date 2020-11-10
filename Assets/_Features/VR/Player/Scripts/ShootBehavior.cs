using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Gameplay.VR.Player
{
    public class ShootBehavior : BallistixData
    {
        Transform gunBarrel;
        ProjectileBehavior projectile;

        // a reference to the action
        public SteamVR_Action_Boolean shootAction;

        // a reference to the hand
        public SteamVR_Input_Sources handType;

        private void Start()
        {
            shootAction.AddOnStateDownListener(TriggerDown, handType);
            shootAction.AddOnStateUpListener(TriggerUp, handType);
        }

        private void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {

        }

        private void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            projectile.transform.position = gunBarrel.position;
            projectile.transform.rotation = gunBarrel.rotation;
            projectile.Shoot();
        }
    }
}