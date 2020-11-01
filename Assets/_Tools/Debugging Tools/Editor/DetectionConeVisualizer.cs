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
        Vector3 localLeft = -detectionBehavior.transform.right;
        Vector3 localRight = detectionBehavior.transform.right;
        /*
        leftPoint = detectionBehavior.transform.forward + new Vector3(Mathf.Sin(-detectionBehavior.coneOfVision / 2 * Mathf.Deg2Rad), 0, Mathf.Cos(detectionBehavior.coneOfVision / 2 * Mathf.Deg2Rad));
        rightPoint = detectionBehavior.transform.forward + new Vector3(Mathf.Sin(detectionBehavior.coneOfVision / 2 * Mathf.Deg2Rad), 0, Mathf.Cos(detectionBehavior.coneOfVision / 2 * Mathf.Deg2Rad));


        leftPoint = new Vector3(localForward.x, localForward.y, localForward.z) * Mathf.Deg2Rad;
        rightPoint = new Vector3(localForward.x, localForward.y, localForward.z * detectionBehavior.rangeOfVision * 10) * Mathf.Deg2Rad;*/

        leftPoint = localTransform.forward + new Vector3(localLeft.x, localLeft.y, localLeft.z);
        rightPoint = localTransform.forward + new Vector3(localRight.x, localRight.y, localRight.z);

        Handles.color = Color.red;
        Handles.DrawWireArc(detectionBehavior.transform.position,
                             Vector3.up,
                             detectionBehavior.transform.position + leftPoint,
                             detectionBehavior.coneOfVision / 2,
                             detectionBehavior.rangeOfVision);

        Handles.color = Color.blue;
        Handles.DrawLine(detectionBehavior.transform.forward, leftPoint * detectionBehavior.rangeOfVision);
        Handles.color = Color.green;
        Handles.DrawLine(detectionBehavior.transform.forward, rightPoint * detectionBehavior.rangeOfVision);
    }
}
