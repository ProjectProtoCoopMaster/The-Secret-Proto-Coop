using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    // TODO : make these values auto assign with a Scriptable Object
    public class EntityVisionData : MonoBehaviour
    {
        // common variables (detection and overwatch)
        public float rangeOfVision;
        public float coneOfVision;
        public Transform playerHead;
        public LayerMask layerMask;
        public RaycastHit hitInfo;

        // overwatch variables
        public float pingFrequency;
        protected float coneOfVisionActual;
        internal List<GameObject> guards = new List<GameObject>();
    }
}