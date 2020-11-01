using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class DetectionBehavior : EntityVisionData
    {
        private void Awake()
        {
            rangeOfVision = entityData.rangeOfVision;
            coneOfVision = entityData.coneOfVision;
            playerHead = entityData.playerHead;
            layerMask = entityData.layerMask;
            hitInfo = entityData.hitInfo;

            coneOfVisionActual = coneOfVision / 2;
        }

        private void Start()
        {
            // have two seperate methods to 
            StartCoroutine(PlayerInRangeCheck());
        }

        // check if the player is in range 
        IEnumerator PlayerInRangeCheck()
        {
            while (true)
            {
                Debug.Log("Scanning");
                // if the player is within the vision range
                if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(playerHead.position.x, playerHead.position.z)) < rangeOfVision)
                {
                    // get the direction of the player's head, if the angle between the looking dir of the cam and the player is less than the cone of vision, then you are inside the cone of vision
                    Vector3 targetDir = playerHead.position - new Vector3(transform.position.x, playerHead.position.y, transform.position.z);
                    var playerInCone = Vector3.Angle(targetDir, transform.forward) <= coneOfVisionActual ?
                        StartCoroutine(PlayerInSightCheck()) : default;
                }

                yield return null;
            }
        }

        // if the player is in range and in the cone of vision, check if you have line of sight to his head collider
        IEnumerator PlayerInSightCheck()
        {
            Debug.Log("I can see the player");
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
