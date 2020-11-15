using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class AwarenessManager : MonoBehaviour
    {
        [SerializeField] List<EntityVisionDataInterface> detectionBehaviors = new List<EntityVisionDataInterface>();

        private void Awake()
        {
            //detectionBehaviors.AddRange(FindObjectsOfType<DetectionBehavior>());
        }

        public void GE_GuardDied()
        {
            for (int i = 0; i < detectionBehaviors.Count; i++)
            {
               // detectionBehaviors[i].scanForGuard.Raise();
            }
        }
    }
}