using System.Collections;
using UnityEngine;

namespace Gameplay.VR
{
    public class OverwatchBehavior : EntityVisionData
    {
        private void Awake()
        {
            /*rangeOfVision = entityData.rangeOfVision;
            coneOfVision = entityData.coneOfVision;
            playerHead = entityData.playerHead;
            layerMask = entityData.layerMask;
            hitInfo = entityData.hitInfo;*/
            coneOfVisionActual = coneOfVision / 2;

            foreach (GameObject item in FindObjectsOfType<GameObject>())
            {
                if (item.CompareTag("Guard") && item != gameObject) guards.Add(item);
            }
        }

        private void Start()
        {
            StartCoroutine(PingForGuards());
        }

        IEnumerator PingForGuards()
        {
            while (true)
            {
                foreach (GameObject guard in guards)
                {
                    Debug.Log("Scanning");
                    // if the player is within the vision range
                    if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(guard.transform.position.x, guard.transform.position.z)) < rangeOfVision)
                    {
                        // get the direction of the player's head, if the angle between the looking dir of the cam and the player is less than the cone of vision, then you are inside the cone of vision
                        Vector3 dirToGuard = guard.transform.position - new Vector3(transform.position.x, guard.transform.position.y, transform.position.z);
                        if (Vector3.Angle(dirToGuard, transform.forward) <= coneOfVisionActual)
                            Debug.DrawLine(transform.position, guard.transform.position, Color.red);
                    }

                }
                yield return null;
            }

            yield return new WaitForSeconds(.5f);
            StartCoroutine(PingForGuards());
        }
    }
}