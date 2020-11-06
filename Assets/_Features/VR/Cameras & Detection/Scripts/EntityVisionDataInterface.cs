using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    [RequireComponent(typeof(OverwatchBehavior))]
    [RequireComponent(typeof(DetectionBehavior))]
    public class EntityVisionDataInterface : MonoBehaviour
    {
        [SerializeField] [HideInInspector] public EntityVisionScriptable entityVisionData;

        [SerializeField] [HideInInspector] public float rangeOfVision;
        [SerializeField] [HideInInspector] public float coneOfVision;
        [SerializeField] public Transform playerHead;

        [SerializeField] [HideInInspector] protected LayerMask layerMask;
        [SerializeField] [HideInInspector] protected RaycastHit hitInfo;

        [SerializeField] [HideInInspector] protected GameEvent gameOver;
        [SerializeField] [HideInInspector] protected Vector3 targetDir;

        // overwatch variables (do not show in Scriptable)
        protected float pingFrequency = 2f; // frequency at which you check up on nearby entities
        protected List<GameObject> guards = new List<GameObject>(); // list of guards in the scene
        protected List<Vector3> deadGuards = new List<Vector3>(); // list of guards in the scene
    }
}