using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TransformDebug : MonoBehaviour
{
    public bool usePath;
    [ShowIf("usePath")]
    public PatrolPath path;

    [SerializeField] private Color color;
    //public Color Color { get { if (usePath) { path.pathColor; } else { color; } } set { } };

    public float radius;

    void OnDrawGizmos()
    {
        if (path != null) Gizmos.color = path.pathColor;
        else Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
