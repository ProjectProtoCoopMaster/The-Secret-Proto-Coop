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

    public State state { get; set; }
    public State save { get; set; }

    void Start()
    {
        string msg = "There is no path attached to the " + this.gameObject.name + " patrol behavior. Select one so that the little fella can move !";
        
        if (SafeCheck(path, msg))
        {
            StartPatrol();
        }
    }

    #region Set
    public void StartPatrol()
    {
        _way = 0;
        SetWaypoint(_way);
    }

    public void SetWaypoint(int index)
    {
        currentWaypoint = path.waypoints[index];

        SetMove(currentWaypoint.position, true);

        state = State.Move;
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
                state = State.Wait;
                currentWaitTime = currentAction.timeToWait;
            }
            else if (currentAction.type == ActionType.Watching)
            {
                state = State.Watch;

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
    #endregion

    #region Loop
    void Update()
    {
        if (state == State.Move) CheckPosition();

        else if (state == State.Wait) Wait();

        else if (state == State.Watch) CheckWatch();
    }

    private void CheckPosition()
    {
        if (IsInArea(transform.position, currentWaypoint.position, 0.5f))
        {
            SetMove(transform.position, false);

            _act = 0;
            SetAction(_act);
        }
    }

    private void Wait()
    {
        if (currentWaitTime <= 0.0f) NextAction();

        else currentWaitTime -= Time.deltaTime;
    }

    private void CheckWatch()
    {
        if (!watching.watch) NextAction();
    } 
    #endregion

    #region Next
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
    #endregion

    #region Pause
    public void StopPatrol()
    {
        save = state;
        state = State.None;

        watching.watch = false;

        SetMove(transform.position, false);
    }

    public void ResumePatrol()
    {
        state = save;

        if (state == State.Watch) watching.watch = true;

        if (state == State.Move) SetMove(currentWaypoint.position, true);
    }
    #endregion
}
