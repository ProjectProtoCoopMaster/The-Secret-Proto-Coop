using System;
using System.Collections;
using UnityEngine;

namespace Gameplay.VR
{
    public class OverwatchBehavior : EntityVisionDataInterface
    {
        private void Awake()
        {
            guards.AddRange(GameObject.FindGameObjectsWithTag("Guard"));
        }

        private void Start()
        {
            //StartCoroutine(PingForGuards());
        }

        public void GE_SonarForGuards()
        {
            StartCoroutine(PingForGuards());
        }

        IEnumerator PingForGuards()
        {
            Debug.Log("Checking for more");
            for (int i = 0; i < guards.Count; i++)
            {
                // if the player is within the vision range
                if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(guards[i].transform.position.x, guards[i].transform.position.z)) < rangeOfVision)
                {
                    // get the direction of the player's head...
                    targetDir = guards[i].transform.position - new Vector3(transform.position.x, guards[i].transform.position.y, transform.position.z);
                    //...if the angle between the looking dir of the cam and the player is less than the cone of vision, then you are inside the cone of vision
                    if (Vector3.Angle(targetDir, transform.forward) <= coneOfVision * .5f) CheckGuardState(guards[i]);
                }

                else continue;
            }

            yield return null;
            StartCoroutine(PingForGuards());
        }

        void CheckGuardState(GameObject guard)
        {
            Debug.DrawLine(transform.position, guard.transform.position, Color.red);
            if (guard.name == "DEAD") Debug.Log("Game over, bub");
        }
    }
}