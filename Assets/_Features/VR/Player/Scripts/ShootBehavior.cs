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
            shootAction.AddOnStateUpListener(TriggerRelease, handType);
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
            GameObject yes = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            yes.GetComponent<Collider>().enabled = false;
            yes.transform.position = gunBarrel.position;
            yes.transform.localScale = new Vector3(.2f, .2f, .2f);
            Rigidbody ohyeah = yes.AddComponent<Rigidbody>();
            ohyeah.AddForce(gunBarrel.transform.forward * 20f, ForceMode.Impulse);
                       
            Debug.DrawRay(gunBarrel.position, gunBarrel.transform.forward * 50f, Color.magenta);
            if (Physics.SphereCast(gunBarrel.position, 0.25f, gunBarrel.transform.forward, out hit, 100f, shootingLayer))
            {
                if (hit.collider.gameObject.CompareTag("Guard"))
                {
                    hit.collider.GetComponent<GuardMortalityBehavior>().Shot();
                    //hit.collider.GetComponent<RagdollBehavior>().ActivateRagdollWithForce(
                    //    yes.transform.forward * 100 + yes.transform.up, ForceMode.Impulse);
                }
                    

                else Debug.Log("Bullet hit " + hit.collider.name);
            }
        }
    }
}