using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Gameplay;

namespace Gameplay.AI
{
    public class PathfindingMove : MoveAction
    {
        public NavMeshAgent navMeshAgent;

        public Vector3 destination { get; set; }

        public bool move { get; set; }

        public float angular;

        #region Set
        void Start()
        {
            destination = target.position;
        }

        public override void SetMove(Vector3 direction, bool move)
        {
            destination = direction;
            SetNavAgent(!move);
            //this.move = move;
        }

        public void SetNavAgent(bool locked)
        {
            if (locked) navMeshAgent.angularSpeed = 0.0f;

            else navMeshAgent.angularSpeed = angular;
        }
        #endregion

        #region Loop
        void Update()
        {
            //if (move)
            SetNavDestination(destination);
        }

        private void SetNavDestination(Vector3 dest)
        {
            navMeshAgent.SetDestination(dest);
        }
        #endregion
    } 
}
