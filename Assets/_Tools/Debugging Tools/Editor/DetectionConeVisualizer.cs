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
        Transform localTransform = detectionBehavior.transform;

        leftPoint = localTransform.position + (localTransform.rotation * new Vector3(Mathf.Sin(-detectionBehavior.coneOfVision / 2 * Mathf.Deg2Rad), 0, Mathf.Cos(-detectionBehavior.coneOfVision / 2 * Mathf.Deg2Rad)) * detectionBehavior.rangeOfVision);
        rightPoint = localTransform.position + (localTransform.rotation * new Vector3(Mathf.Sin(detectionBehavior.coneOfVision / 2 * Mathf.Deg2Rad), 0, Mathf.Cos(detectionBehavior.coneOfVision / 2 * Mathf.Deg2Rad)) * detectionBehavior.rangeOfVision);

        Handles.color = Color.red; Handles.DrawWireArc(detectionBehavior.transform.position,
                                   Vector3.up,
                                   leftPoint,
                                   360,//.coneOfVision / 2,
                                   detectionBehavior.rangeOfVision);

        Handles.color = Color.blue; Handles.DrawLine(localTransform.position, leftPoint);
        Handles.color = Color.green; Handles.DrawLine(localTransform.position, rightPoint);
    }
}
