﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class DetectionBehavior : EntityVisionDataInterface
    {
        private void Start()
        {
            // have two seperate methods to 
            StartCoroutine(PlayerInRangeCheck());
        }

        public void CE_DetectionSwitch()
        {
            isActive = !isActive;

            if (isActive) StartCoroutine(PlayerInRangeCheck());
            else StopAllCoroutines();
        }

        // check if the player is in range 
        IEnumerator PlayerInRangeCheck()
        {
            while (true)
            {
                myPos.x = transform.position.x;
                myPos.z = transform.position.z;

                targetPos.x = playerHead.transform.position.x;
                targetPos.z = playerHead.transform.position.z;

                myFinalPos.x = transform.position.x;
                myFinalPos.y = playerHead.transform.position.y;
                myFinalPos.z = transform.position.z;

                distToTarget = (targetPos - myPos).sqrMagnitude;
                // Debug.DrawLine(transform.position, playerHead.position, Color.white);

                // if the player is within the vision range
                if (distToTarget < rangeOfVision * rangeOfVision)
                {
                    // get the direction of the player's head...
                   // targetDir = playerHead.position - myFinalPos;
                    Vector3 targetDir = playerHead.position - transform.position;
                    
                    //...if the angle between the looking dir of the cam and the player is less than the cone of vision, then you are inside the cone of vision
                    if (Vector3.Angle(targetDir, transform.forward) <= coneOfVision * 0.5f) PlayerInSightCheck();
                }

                yield return null;
            }
        }

        // if the player is in range and in the cone of vision, check if you have line of sight to his head collider
        void PlayerInSightCheck()
        {
            Debug.Log("I'm checking");

            // if you hit something between the camera and the player's head position
            if (Physics.Linecast(transform.position, playerHead.position, out hitInfo, detectionMask))
            {
                if (hitInfo.collider.gameObject.name == playerHead.name)
                {
                    Debug.DrawLine(transform.position, playerHead.position, Color.green);
                    Debug.Log("I hit the player");
                    // call the gameOver event for a quick death
                    // gameOver.Raise();
                    // TODO : Implement a progressive spotting mechanic, based on distance
                }
                else if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Environment"))
                {
                    Debug.DrawLine(transform.position, playerHead.position, Color.red);
                    Debug.Log("I hit " + hitInfo.collider.gameObject.name);
                }
            }
            else if (!Physics.Linecast(transform.position, playerHead.position, out hitInfo, detectionMask))
                Debug.Log("No Collisions");
        }
    }
}
