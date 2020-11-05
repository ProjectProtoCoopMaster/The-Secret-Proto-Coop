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
        public LayerMask layerMask;
        public Transform playerHead;
        public RaycastHit hitInfo;

        // overwatch variables (do not show in Scriptable)
        protected float pingFrequency = 2f; // frequency at which you check up on nearby entities
        protected List<GameObject> guards = new List<GameObject>(); // list of guards in the scene


    }
}