using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class EntityPatrolData : MonoBehaviour
    {
        protected List<PatrolPoint> fullPatrolPath = new List<PatrolPoint>();
        protected Queue<PatrolPoint> patrolQueue = new Queue<PatrolPoint>();
    }
}