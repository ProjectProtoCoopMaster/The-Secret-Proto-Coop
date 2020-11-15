using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformDebug : MonoBehaviour
{
    public PatrolPath path;
    public float radius;

    void OnDrawGizmos()
    {
        if (path != null) Gizmos.color = path.pathColor;
        else Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
