using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Gameplay;

public class PathfindingBehavior : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    
    public Vector3 destination { get; set; }

    public bool move { get; set; }

    public float angular;

    private void Start()
    {
        destination = transform.position;
    }

    void Update()
    {
        if (move) SetNavDestination(destination);
    }

    public void SetNavDestination(Vector3 dest)
    {
        navMeshAgent.SetDestination(dest);
    }

    public void SetNavAgent(bool locked)
    {
        if (locked) navMeshAgent.angularSpeed = 0.0f;

        else navMeshAgent.angularSpeed = angular;
    }
}
