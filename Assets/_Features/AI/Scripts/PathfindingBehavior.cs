using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Gameplay;

public class PathfindingBehavior : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    
    public Vector3 destination { get; set; }

    private void Start()
    {
        destination = this.transform.position;
    }

    void Update()
    {
        SetNavDestination(destination);
    }

    public void SetNavDestination(Vector3 dest)
    {
        navMeshAgent.SetDestination(dest);
    }
}
