using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class EntityVisionData : MonoBehaviour
    {
        public float rangeOfVision;
        public float coneOfVision;
        public Transform playerHead;
        public LayerMask layerMask;
        public RaycastHit hitInfo;

        public float pingFrequency;
        protected float coneOfVisionActual;
        internal List<GameObject> guards = new List<GameObject>();
    }
}