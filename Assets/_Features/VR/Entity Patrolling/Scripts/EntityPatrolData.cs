using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class EntityPatrolData : MonoBehaviour
    {
        [SerializeField] public List<PatrolPoint> patrolPointsList = new List<PatrolPoint>();
        [SerializeField] public Queue<PatrolPoint> patrolPointsQueue = new Queue<PatrolPoint>();
    }
}