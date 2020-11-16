using UnityEngine;
using Valve.VR;

namespace Gameplay.VR.Player
{
    public class ShootBehavior : BallistixData
    {
        public Transform gunBarrel;
        //public ProjectileBehavior projectile;
        public LayerMask shootingLayer;

        RaycastHit hit;

        // a reference to the action
        public SteamVR_Action_Boolean shootAction;

        // a reference to the hand
        public SteamVR_Input_Sources handType;

        private void Start()
        {
            shootAction.AddOnStateUpListener(TriggerRelease, handType);
        }

        private void Update()
        {
            Debug.DrawRay(gunBarrel.position, transform.forward * 50f, Color.magenta);

            if (Physics.Raycast(gunBarrel.position, gunBarrel.transform.forward, out hit, shootingLayer))
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                    hit.collider.GetComponent<GuardMortalityBehavior>().Shot();
            }
        }

        private void TriggerRelease(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            Debug.DrawRay(gunBarrel.position, transform.forward * 50f, Color.magenta);

            if (Physics.Raycast(gunBarrel.position, transform.forward, out hit, shootingLayer))
            {
                if(hit.collider.gameObject.CompareTag("Enemy")) 
                    GetComponent<GuardMortalityBehavior>().Shot();
            }
        }
    }
}