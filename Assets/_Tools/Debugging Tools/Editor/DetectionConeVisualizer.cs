﻿using UnityEngine;
using UnityEditor;
using Gameplay.VR;

namespace Tools.Debugging
{
    [CanEditMultipleObjects]
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

            Handles.color = Color.white;
            Handles.DrawWireDisc(localTransform.position, Vector3.up, detectionBehavior.rangeOfVision);

            Handles.DrawLine(localTransform.position, leftPoint);
            Handles.DrawLine(localTransform.position, rightPoint);

            /*
            if (detectionBehavior.entityVisionData.playerHead  != null)
            {
                // floor the values to get the "shadow" image but locked at 0y
                Vector3 flooredPosition = new Vector3(localTransform.position.x, detectionBehavior.entityVisionData.playerHead .position.y, localTransform.position.z);
                Vector3 flooredleftPoint = new Vector3(leftPoint.x, detectionBehavior.entityVisionData.playerHead .position.y, leftPoint.z);
                Vector3 flooredrightPoint = new Vector3(rightPoint.x, detectionBehavior.entityVisionData.playerHead .position.y, rightPoint.z);

                Handles.color = Color.red;
                Handles.DrawWireDisc(flooredPosition, Vector3.up, detectionBehavior.rangeOfVision);

                Handles.DrawLine(flooredPosition, flooredleftPoint);
                Handles.DrawLine(flooredPosition, flooredrightPoint);
            }*/
        }
    }
}