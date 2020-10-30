using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class DetectionBehavior : EntityVisionData
    {
        private void Awake()
        {
            coneOfVisionActual = coneOfVision / 2;
        }

        private void Start()
        {
            // have two seperate methods to 
            StartCoroutine(PlayerInRangeCheck());
        }


        private void Update()
        {
            Debug.DrawLine(transform.position, (transform.forward + transform.right) * 5f, Color.white);
            Debug.DrawLine(transform.position, (transform.forward - transform.right) * 5f, Color.white);            
        }

        // check if the player is in range 
        IEnumerator PlayerInRangeCheck()
        {
            while(true)
            {
                // if the player is within the vision range
                if (Vector2.Distance(transform.position, playerHead.position) < rangeOfVision)
                {
                    // if the player is within the angle of vision, call a sight check
                   var playerInCone = Vector3.Angle(transform.forward, playerHead.position) <= coneOfVisionActual ?
                        StartCoroutine(PlayerInSightCheck()) : default;
                }

                yield return null;
            }
        }

        // if the player is in range and in the cone of vision, check if you have line of sight to his head collider
        IEnumerator PlayerInSightCheck()
        {
            Debug.DrawLine(transform.position, playerHead.position, Color.green);

            // if you hit something between the camera and the player's head position on the player layer
            if (Physics.Linecast(this.transform.position, playerHead.position, out hitInfo, layerMask))
            {
                // call the gameOver event
            }

            yield return null;
        }
    }
}
