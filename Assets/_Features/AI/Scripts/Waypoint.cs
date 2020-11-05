using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum ActionType { Wait, Watching }

[System.Serializable]
public class Action
{
    public ActionType type;

    [ShowIf("type", ActionType.Wait)]
    public float timeToWait;

    [ShowIf("type", ActionType.Watching)]
    public Vector3 lookToPosition;
}

public class Waypoint : MonoBehaviour
{
    public Vector3 position { get => this.transform.position; }

    public List<Action> actions = new List<Action>();
}
