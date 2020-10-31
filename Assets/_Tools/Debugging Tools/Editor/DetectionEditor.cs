using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Gameplay.VR;

[CustomEditor(typeof(DetectionBehavior))]
public class DetectionEditor : Editor
{
    DetectionBehavior detectionBehavior;
    Vector3 leftPoint, rightPoint;

    private void OnEnable()
    {
        detectionBehavior = target as DetectionBehavior;
    }

    public void OnSceneGUI()
    {
        leftPoint = detectionBehavior.transform.forward + new Vector3(detectionBehavior.transform.position.x - detectionBehavior.coneOfVision, 0, detectionBehavior.rangeOfVision);
        rightPoint = detectionBehavior.transform.forward + new Vector3(detectionBehavior.transform.position.x + detectionBehavior.coneOfVision, 0, detectionBehavior.rangeOfVision);

        Handles.color = Color.red;
        Handles.DrawWireArc(detectionBehavior.transform.forward, 
                             Vector3.up,
                             detectionBehavior.transform.forward - detectionBehavior.transform.right, 
                             detectionBehavior.coneOfVision, 
                             detectionBehavior.rangeOfVision);
    }
}
