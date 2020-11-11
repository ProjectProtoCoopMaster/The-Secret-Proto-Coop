using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PatrolPath : MonoBehaviour
{
    public Color pathColor;

    public List<Waypoint> waypoints;

    public float pointArea;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach (Waypoint point in waypoints)
        {
            Vector3 center = new Vector3(point.position.x, 0.0f, point.position.z);
            Vector3 size = new Vector3(pointArea, 0.0f, pointArea);
            Gizmos.DrawWireCube(center, size);
        }
    }
}
