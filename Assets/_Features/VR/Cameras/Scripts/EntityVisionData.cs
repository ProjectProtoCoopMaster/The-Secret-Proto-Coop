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
        [HideInInspector] public float rangeOfVision;
        [HideInInspector] public float coneOfVision;
        [HideInInspector] public Transform playerHead;
        [HideInInspector] public LayerMask layerMask;
        [HideInInspector] public RaycastHit hitInfo;

        // overwatch variables (do not show in Scriptable)
        [HideInInspector] public float pingFrequency;
        [HideInInspector] public float coneOfVisionActual;
        [HideInInspector] public List<GameObject> guards = new List<GameObject>();
    }
}