using UnityEngine;
using Valve.VR;

namespace Gameplay.VR.Player
{
    public class ShootBehavior : BallistixData
    {
        public Transform gunBarrel;
        public LayerMask shootingLayer;

        RaycastHit hit;

        // a reference to the action
        public SteamVR_Action_Boolean shootAction;

        // a reference to the hand
        public SteamVR_Input_Sources handType;


        private void Start()
        {
            shootAction.AddOnStateDownListener(TriggerRelease, handType);
        }
        
        /*private void Update()
        {
            Debug.DrawRay(gunBarrel.position, gunBarrel.transform.forward * 50f, Color.magenta);

            if (Physics.Raycast(gunBarrel.position, gunBarrel.transform.forward, out hit, shootingLayer))
            {
                if (hit.collider.gameObject.CompareTag("Guard"))
                    hit.collider.GetComponent<GuardMortalityBehavior>().Shot();
            }
        }*/

        private void TriggerRelease(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            Debug.DrawRay(gunBarrel.position, gunBarrel.transform.forward * 50f, Color.magenta);
            if (Physics.Raycast(gunBarrel.position, gunBarrel.transform.forward, out hit, shootingLayer))
            {
                if (hit.collider.gameObject.CompareTag("Guard"))
                    hit.collider.GetComponent<GuardMortalityBehavior>().Shot();
            }
        }
    }
}