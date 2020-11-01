using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    // TODO : make these values auto assign with a Scriptable Object
    public class EntityVisionData : MonoBehaviour
    {
        public EntityVisionScriptable entityData;

        // common variables (detection and overwatch)
        public float rangeOfVision;
        public float coneOfVision;
        public Transform playerHead;
        public LayerMask layerMask;
        public RaycastHit hitInfo;

        // overwatch variables (do not show in Scriptable)
        [HideInInspector] public float pingFrequency;
        [HideInInspector] public float coneOfVisionActual;
        [HideInInspector] public List<GameObject> guards = new List<GameObject>();
    }
}