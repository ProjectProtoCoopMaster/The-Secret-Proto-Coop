using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class OverwatchBehavior : EntityVisionData
    {
        private void Awake()
        {
            rangeOfVision = entityData.rangeOfVision;
            coneOfVision = entityData.coneOfVision;
            playerHead = entityData.playerHead;
            layerMask = entityData.layerMask;
            hitInfo = entityData.hitInfo;

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
            while(true)
            {
                foreach (GameObject item in guards) Debug.DrawLine(transform.position, item.transform.position, Color.red);
                yield return null;
            }

            yield return new WaitForSeconds(.5f);
            StartCoroutine(PingForGuards());
        }
    }
}