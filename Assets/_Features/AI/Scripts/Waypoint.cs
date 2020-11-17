using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum ActionType { Wait, Watching }

public enum Orientations { North, South, East, West, North_West, North_East, South_West, South_East }

[System.Serializable]
public class Orientation
{
    public Orientations orientation;

    public Vector3 dir
    {
        get
        {
            switch (orientation)
            {
                case Orientations.North: return Vector3.forward;
                case Orientations.South: return Vector3.back;
                case Orientations.East: return Vector3.right;
                case Orientations.West: return Vector3.left;

                case Orientations.North_West: return (Vector3.forward + Vector3.left);
                case Orientations.North_East: return (Vector3.forward + Vector3.right);
                case Orientations.South_West: return (Vector3.back + Vector3.left);
                case Orientations.South_East: return (Vector3.back + Vector3.right);

                default: return Vector3.zero;
            }
        }
    }
}

[System.Serializable]
public class Action
{
    public ActionType type;

    [ShowIf("type", ActionType.Wait)]
    public float timeToWait;

    [ShowIf("type", ActionType.Watching)]
    public List<Orientation> orientations;

    private List<Vector3> directions
    {
        get
        {
            List<Vector3> dirs = new List<Vector3>();
            foreach (Orientation o in orientations)
            {
                dirs.Add(o.dir);
            }
            return dirs;
        }
    }
    public List<Vector3> watchDirections { get => directions; }
}

public class Waypoint : MonoBehaviour
{
    public Vector3 position { get => this.transform.position; }

    public List<Action> actions = new List<Action>();
}
