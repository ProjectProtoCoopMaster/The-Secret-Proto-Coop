using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LastAction { Move, Wait, Watch, None }

public class MoveBehavior : MonoBehaviour
{
    public PathfindingBehavior pathfinding;

    protected void SetMove(Vector3 direction, bool move)
    {
        pathfinding.destination = direction;
        pathfinding.SetNavAgent(!move);
        pathfinding.move = move;
    }

    public bool IsInArea(Vector3 objectPos, Vector3 destPos, float area)
    {
        if (objectPos.x <= (destPos.x + area) && objectPos.x >= (destPos.x - area))
        {
            if (objectPos.z <= (destPos.z + area) && objectPos.z >= (destPos.z - area))
            {
                return true;
            }
            else return false;
        }
        else return false;
    }

    public static bool SafeCheck<T>(T element, string message)
    {
        if (element == null)
        {
            Debug.Log(message);
            return false;
        }
        else return true;
    }
}
