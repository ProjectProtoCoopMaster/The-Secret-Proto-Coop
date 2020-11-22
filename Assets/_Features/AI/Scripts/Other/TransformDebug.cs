﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Gameplay.AI;

public class TransformDebug : MonoBehaviour
{
    public bool usePath;
    [ShowIf("usePath")]
    public PatrolPath path;

    [HideIf("usePath")]
    [SerializeField] private Color color;
    public Color32 Color { get { if (usePath) { return path.pathColor; } else { return color; } } }

    public bool otherRadius;
    [ShowIf("otherRadius")]
    public SoundObject script;

    [HideIf("otherRadius")]
    [SerializeField] private float radius;
    public float Radius { get { if (otherRadius) { return script.radius; } else { return radius; } } }

    void OnDrawGizmos()
    {
        Gizmos.color = Color;
        Gizmos.DrawWireSphere(transform.position, Radius);
        Gizmos.color = new Color32(Color.r, Color.g, Color.b, 25);
        Gizmos.DrawSphere(transform.position, Radius);
    }
}
