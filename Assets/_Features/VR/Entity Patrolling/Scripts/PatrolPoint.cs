using UnityEngine;

namespace Gameplay.VR
{
    [System.Serializable]
    public struct PatrolPoint
    {
        public Vector3 worldPosition;
        public int patrolPointIndex;

        public PatrolPoint(Vector3 pos = default, int index = default)
        {
            worldPosition = pos;
            patrolPointIndex = index;
        }
    }
}