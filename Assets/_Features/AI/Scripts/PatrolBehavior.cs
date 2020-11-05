using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehavior : MonoBehaviour
{
    public PatrolPath path;

    public bool loopPath = true;

    private Waypoint currentWaypoint;
    private int _way;
    private Action currentAction;
    private int _act;

    public PathfindingBehavior pathfindingBehavior;

    private float currentWaitTime;

    private bool isMoving;
    private bool isWaiting;
    private bool isWatching;

    public static bool SafeCheck<T>(T element, string message)
    {
        if (element == null)
        {
            Debug.Log(message);
            return false;
        }
        else return true;
    }

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

        SetDestination();
    }

    public void SetDestination()
    {
        pathfindingBehavior.destination = currentWaypoint.position;
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

                // Code to setup Rotation / Watching
            }
        }
    }

    void Update()
    {
        if (isMoving) CheckPosition();

        else if (isWaiting) Wait();

        else if (isWatching) Watch();
    }

    private void CheckPosition()
    {
        if (ComparePositions(transform.position, currentWaypoint.position))
        {
            isMoving = false;
            _act = 0;
            SetAction(_act);
        }
    }

    public bool ComparePositions(Vector3 objectPos, Vector3 destPos)
    {
        if (objectPos.x == destPos.x && objectPos.z == destPos.z) return true;

        else return false;
    }

    private void Wait()
    {
        if (currentWaitTime <= 0.0f)
        {
            isWaiting = false;
            NextAction();
        }
        else
        {
            currentWaitTime -= Time.deltaTime;
        }
    }

    private void Watch()
    {
        // Code to execute rotation / Watching

        isWatching = false;
        NextAction();
    }

    private void NextAction()
    {
        _act++;

        if (currentWaypoint.actions.Count == _act)
        {
            NextWaypoint();
        }
        else
        {
            SetAction(_act);
        }
    }

    private void NextWaypoint()
    {
        _way++;

        if (path.waypoints.Count == _way)
        {
            if (loopPath) StartPatrol();
        }
        else
        {
            SetWaypoint(_way);
        }
    }
}
