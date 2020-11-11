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

    private bool diverted;
    private bool searching;
    private bool returning;

    public float waitTime;
    private float currentTime;

    public void DistractTo(Vector3 direction)
    {
        if (hasPatrol) patrol.StopPatrol();

        distractionPosition = direction;
        returnPosition = transform.position;

        SetMove(distractionPosition, true);

        diverted = true;
    }

    private void Update()
    {
        if (diverted) CheckForDestination();

        else if (searching) Waiting(); // Search or Wait Animation and talking

        else if (returning) CheckForReturn();
    }

    void CheckForDestination()
    {
        if (IsInArea(transform.position, distractionPosition, patrol.path.pointArea))
        {
            SetMove(transform.position, false);

            diverted = false;
            searching = true;

            currentTime = waitTime;
        }
    }

    void Waiting()
    {
        if (currentTime <= 0.0f)
        {
            SetMove(returnPosition, true);

            searching = false;
            returning = true;
        }
        else currentTime -= Time.deltaTime;
    }

    void CheckForReturn()
    {
        if (IsInArea(transform.position, returnPosition, patrol.path.pointArea))
        {
            SetMove(transform.position, false);

            if (hasPatrol) patrol.ResumePatrol();

            returning = false;
        }
    }
}
