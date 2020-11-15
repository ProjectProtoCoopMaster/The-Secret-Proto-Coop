using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DistractionBehavior : MoveBehavior
{
    public bool hasPatrol;
    [ShowIf("hasPatrol")]
    public PatrolBehavior patrol;

    private Vector3 distractionPosition;
    private Vector3 returnPosition;

    private Vector3 currentDestination;

    private State st;

    private bool hasSearched;

    public float waitTime;
    private float currentTime;

    public void DistractTo(Vector3 direction)
    {
        if (hasPatrol) patrol.StopPatrol();

        distractionPosition = direction;
        returnPosition = transform.position;

        currentDestination = distractionPosition;
        SetMove(currentDestination, true);

        st = State.Move;
    }

    private void Update()
    {
        if (st == State.Move) CheckForDestination(currentDestination);

        else if (st == State.Search) Waiting(); // Search or Wait Animation and talking
    }

    void CheckForDestination(Vector3 destination)
    {
        if (IsInArea(transform.position, destination, 0.5f))
        {
            SetMove(transform.position, false);

            if (!hasSearched) SetSearch();

            else EndDiversion();
        }
    }

    void SetSearch()
    {
        st = State.Search;

        currentTime = waitTime;
    }

    void EndDiversion()
    {
        if (hasPatrol) patrol.ResumePatrol();

        st = State.None;
    }

    void Waiting()
    {
        if (currentTime <= 0.0f)
        {
            currentDestination = returnPosition;
            SetMove(currentDestination, true);

            hasSearched = true;

            st = State.Move;
        }
        else currentTime -= Time.deltaTime;
    }
}
