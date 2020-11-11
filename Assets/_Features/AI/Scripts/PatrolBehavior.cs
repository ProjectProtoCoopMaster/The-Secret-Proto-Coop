using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehavior : MoveBehavior
{
    public PatrolPath path;

    public bool loopPath = true;

    private Waypoint currentWaypoint;
    private int _way;
    private Action currentAction;
    private int _act;

    public WatchBehavior watching;

    private float currentWaitTime;

    public bool isMoving { get; set; }
    public bool isWaiting { get; set; }
    public bool isWatching { get; set; }

    private LastAction lastAction;

    void Start()
    {
        string msg = "There is no path attached to the " + this.gameObject.name + " patrol behavior. Select one so that the little fella can move !";
        
        if (SafeCheck(path, msg))
        {
            StartPatrol();
        }
    }

    public void StartPatrol()
    {
        _way = 0;
        SetWaypoint(_way);
    }
    
    public void SetWaypoint(int index)
    {
        currentWaypoint = path.waypoints[index];

        SetMove(currentWaypoint.position, true);

        isMoving = true;
    }

    public void SetAction(int index)
    {
        if (currentWaypoint.actions.Count == 0)
        {
            NextWaypoint();
        }
        else
        {
            currentAction = currentWaypoint.actions[index];

            if (currentAction.type == ActionType.Wait)
            {
                isWaiting = true;
                currentWaitTime = currentAction.timeToWait;
            }
            else if (currentAction.type == ActionType.Watching)
            {
                isWatching = true;

                if (currentAction.watchDirections == null) return;
                else
                {
                    WatchBehavior w = watching;
                    w.directions = currentAction.watchDirections;
                    w._pos = 0;
                    w.SetDirection(w._pos);
                    w.watch = true;
                }
            }
        }
    }

    void Update()
    {
        if (isMoving) CheckPosition();

        else if (isWaiting) Wait();

        else if (isWatching) CheckWatch();
    }

    private void CheckPosition()
    {
        if (IsInArea(transform.position, currentWaypoint.position, path.pointArea))
        {
            SetMove(transform.position, false);

            isMoving = false;
            _act = 0;
            SetAction(_act);
        }
    }

    private void Wait()
    {
        if (currentWaitTime <= 0.0f)
        {
            isWaiting = false;
            NextAction();
        }
        else currentWaitTime -= Time.deltaTime;
    }

    private void CheckWatch()
    {
        if (!watching.watch)
        {
            isWatching = false;
            NextAction();
        }
    }

    private void NextAction()
    {
        _act++;

        if (currentWaypoint.actions.Count == _act) NextWaypoint();

        else SetAction(_act);
    }

    private void NextWaypoint()
    {
        _way++;

        if (path.waypoints.Count == _way && loopPath) StartPatrol();

        else SetWaypoint(_way);
    }

    public void StopPatrol()
    {
        lastAction = isMoving ? LastAction.Move : isWatching ? LastAction.Watch : isWaiting ? LastAction.Wait : LastAction.None;

        isWaiting = false;

        SetMove(currentWaypoint.position, false);
        isMoving = false;

        isWatching = false;
        watching.watch = false;
    }

    public void ResumePatrol()
    {
        switch (lastAction)
        {
            case LastAction.Move:
                SetMove(currentWaypoint.position, true);
                isMoving = true;
                break;
            case LastAction.Wait:
                isWaiting = true;
                break;
            case LastAction.Watch:
                watching.watch = true;
                isWatching = true;
                break;
        }
    }
}
