using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class OverwatchBehavior : EntityVisionData
    {
        IEnumerator PingForGuards()
        {
            foreach (GameObject item in guards)
            {
                Debug.DrawLine(transform.position, item.transform.position);
            }
            yield return new WaitForSeconds(pingFrequency);
            StartCoroutine(PingForGuards());
        }
    }
}