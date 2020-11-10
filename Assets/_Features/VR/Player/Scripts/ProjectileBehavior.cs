using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR.Player
{
    public class ProjectileBehavior : BallistixData
    {
        public void Shoot()
        {
            StartCoroutine(Move());
        }

        IEnumerator Move()
        {
            while(decay < lifetime)
            {
                decay += Time.deltaTime;
                transform.forward += new Vector3(0, 0, 1) * bulletSpeed * Time.deltaTime;
                yield return null;
            }

            decay = 0;
            gameObject.SetActive(false);
            yield return null;
        }
    }
}
