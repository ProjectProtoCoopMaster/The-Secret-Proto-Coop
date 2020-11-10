using UnityEngine;
using Valve.VR;

namespace Gameplay.VR.Player
{
    public class ShootBehavior : BallistixData
    {
        public Transform gunBarrel;
        public ProjectileBehavior projectile;

        // a reference to the action
        public SteamVR_Action_Boolean shootAction;

        // a reference to the hand
        public SteamVR_Input_Sources handType;

        private void Start()
        {
            shootAction.AddOnStateUpListener(TriggerRelease, handType);
        }

        private void TriggerRelease(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            projectile.transform.position = gunBarrel.position;
            projectile.transform.rotation = gunBarrel.rotation;
            projectile.Shoot();
        }
    }
}