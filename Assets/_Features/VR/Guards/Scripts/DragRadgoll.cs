using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class DragRadgoll : MonoBehaviour
    {
        public Rigidbody[] rb;
        public float velocity;
        public GameObject go;

        private void Update()
        {
            for (int i = 0; i < rb.Length; i++)
            {
                rb[i].position = new Vector3( go.transform.position.x, rb[i].position.y, rb[i].position.z);
            }
        }
    }
}

