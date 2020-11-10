using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR.Player
{
    public class ProjectileBehavior : BallistixData
    {
        Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void Shoot()
        {
            StartCoroutine(Move());
        }

        IEnumerator Move()
        {
            decay = 0;
            rigidbody.velocity = Vector3.zero;

            while (decay < lifetime)
            {
                decay += Time.deltaTime;

                rigidbody.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);

                yield return null;
            }

            //gameObject.SetActive(false);
            yield return null;
        }
    }
}
