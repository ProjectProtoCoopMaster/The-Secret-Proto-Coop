using System.Collections;
using UnityEngine;

namespace Gameplay.VR
{
    public class CameraPatrolBehavior : EntityPatrolData
    {
        private void Update()
        {
            Debug.Log(patrolPointsList.Count);
            
            foreach (PatrolPoint item in patrolPointsList) 
                Debug.Log(item.worldPosition);
        }

        // move the camera to its next patrol point location
        IEnumerator MoveToAltPosition()
        {
            yield return null;
        }
    }
}
