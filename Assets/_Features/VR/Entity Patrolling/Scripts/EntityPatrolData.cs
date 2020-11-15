using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class EntityPatrolData : MonoBehaviour
    {
        public List<GameObject> gameObjects = new List<GameObject>();
        protected Queue<GameObject> queue = new Queue<GameObject>();

        [SerializeField] public List<PatrolPoint> patrolPointsList = new List<PatrolPoint>();
        [SerializeField] public Queue<PatrolPoint> patrolPointsQueue = new Queue<PatrolPoint>();
    }
}