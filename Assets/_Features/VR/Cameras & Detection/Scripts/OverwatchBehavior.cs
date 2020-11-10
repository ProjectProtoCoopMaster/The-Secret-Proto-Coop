using System.Collections;
using UnityEngine;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gameplay.VR
{
    public class OverwatchBehavior : EntityVisionDataInterface
    {
        private bool ready;
        Vector2 myPos, targetPos;
        Vector3 myFinalPos;

        float distance;

#if UNITY_EDITOR
        public List<GameObject> visibleGuards = new List<GameObject>();
#endif
        private void Awake()
        {
            foreach (GameObject item in FindObjectsOfType<GameObject>())
            {
                if (item.CompareTag("Guard") && item != gameObject) guards.Add(item);
            }
        }

        private void Start()
        {
            StartCoroutine(DoWork());
        }

        IEnumerator DoWork()
        {
            ThreadPool.QueueUserWorkItem(StartRunning);

            while (!ready) yield return null;
            StartCoroutine(PingForGuards());
        }

        private void StartRunning(object state)
        {
            ready = !ready;
            StartCoroutine(PingForGuards());
        }

        public void GE_OverwatchSwitch()
        {
            isActive = !isActive;
            ready = false;

            if (isActive) StartCoroutine(DoWork());
            else StopAllCoroutines();
        }

        IEnumerator PingForGuards()
        {
            visibleGuards.Clear();
            for (int i = 0; i < guards.Count; i++)
            {
                myPos.x = transform.position.x;
                myPos.y = transform.position.z;

                targetPos.x = guards[i].transform.position.x;
                targetPos.y = guards[i].transform.position.z;

                myFinalPos.x = transform.position.x;
                myFinalPos.y = guards[i].transform.position.y;
                myFinalPos.z = transform.position.z;

                distance = (targetPos - myPos).sqrMagnitude;

                // if the target guard is within the vision range
                if (distance < rangeOfVision)
                {
                    // get the direction of the player's head...
                    targetDir = guards[i].transform.position - myFinalPos;
                    //...if the angle between the looking dir of the cam and the player is less than the cone of vision, then you are inside the cone of vision
                    if (Vector3.Angle(targetDir, transform.forward) <= coneOfVision * .5f) CheckGuardState(guards[i]);
                }

                else continue;
            }

            yield return null;
            ready = !ready;
            StartCoroutine(DoWork());
        }

        void CheckGuardState(GameObject guard)
        {
#if UNITY_EDITOR
            if (!visibleGuards.Contains(guard)) visibleGuards.Add(guard);
#endif
            if (guard.name == "DEAD") Debug.Log("Game over, bub");
        }
    }
}