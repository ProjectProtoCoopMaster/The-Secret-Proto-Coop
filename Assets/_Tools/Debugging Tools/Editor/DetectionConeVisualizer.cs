using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Gameplay.VR;

[CustomEditor(typeof(DetectionBehavior))]
public class DetectionConeVisualizer : Editor
{
    DetectionBehavior detectionBehavior;
    Vector3 leftPoint, rightPoint;

    private void OnEnable()
    {
        detectionBehavior = target as DetectionBehavior;
    }

    public void OnSceneGUI()
    {
        leftPoint = detectionBehavior.transform.forward + new Vector3(Mathf.Sin(-detectionBehavior.coneOfVision / 2 * Mathf.Deg2Rad), 0, Mathf.Cos(-detectionBehavior.coneOfVision / 2 * Mathf.Deg2Rad));
        rightPoint = detectionBehavior.transform.forward + new Vector3(Mathf.Sin(detectionBehavior.coneOfVision / 2 * Mathf.Deg2Rad), 0, Mathf.Cos(detectionBehavior.coneOfVision / 2 * Mathf.Deg2Rad));

        Handles.color = Color.red;
        Handles.DrawWireArc(detectionBehavior.transform.position,
                             Vector3.up,
                             detectionBehavior.transform.position + leftPoint,
                             detectionBehavior.coneOfVision/2,
                             detectionBehavior.rangeOfVision);

        Handles.DrawLine(detectionBehavior.transform.forward, leftPoint * detectionBehavior.rangeOfVision);
        Handles.DrawLine(detectionBehavior.transform.forward, rightPoint * detectionBehavior.rangeOfVision);
    }
}
