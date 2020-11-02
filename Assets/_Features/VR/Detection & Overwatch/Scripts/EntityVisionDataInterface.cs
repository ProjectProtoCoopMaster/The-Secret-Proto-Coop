using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    [RequireComponent(typeof(OverwatchBehavior))]
    [RequireComponent(typeof(DetectionBehavior))]
    public class EntityVisionDataInterface : MonoBehaviour
    {
        [SerializeField] public EntityVisionScriptable entityVisionData;

        [SerializeField] public float rangeOfVision;
        [SerializeField] public float coneOfVision;
        [SerializeField] public Transform playerHead;

        [SerializeField] protected LayerMask layerMask;
        [SerializeField] protected RaycastHit hitInfo;

        // overwatch variables (do not show in Scriptable)
        protected float pingFrequency = 2f; // frequency at which you check up on nearby entities
        protected List<GameObject> guards = new List<GameObject>(); // list of guards in the scene

        // if a scriptable object has been assigned through the inspector, unload its variables into the script
        private void Awake()
        {
            if(entityVisionData != null)
            {
                rangeOfVision = entityVisionData.rangeOfVision;
                rangeOfVision = entityVisionData.coneOfVision;
            }
        }
    }
}