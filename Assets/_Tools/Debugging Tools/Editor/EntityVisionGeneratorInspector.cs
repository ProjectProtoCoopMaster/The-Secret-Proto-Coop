using Gameplay.VR;
using UnityEngine;
using UnityEditor;
using System;

namespace Tools.Debugging
{
    [CustomEditor(typeof(EntityVisionDataInterface))]
    public class EntityVisionGeneratorInspector : Editor
    {
        bool askingUser = true, existingData, localData;
        SerializedProperty entityVisionScriptableProperty, rangeOfVision, coneOfVision, layerMask;
        SerializedObject entityVisionScriptableObject;

        EntityVisionDataInterface entityVisionDataInterface;
        DetectionBehavior detectionBehavior;
        OverwatchBehavior overwatchBehavior;

        private void OnEnable()
        {
            entityVisionDataInterface = target as EntityVisionDataInterface;
            detectionBehavior = entityVisionDataInterface.gameObject.GetComponent<DetectionBehavior>();
            overwatchBehavior = entityVisionDataInterface.gameObject.GetComponent<OverwatchBehavior>();

            entityVisionScriptableProperty = serializedObject.FindProperty("entityVisionData");
            rangeOfVision = serializedObject.FindProperty("rangeOfVision");
            coneOfVision = serializedObject.FindProperty("coneOfVision");
            layerMask = serializedObject.FindProperty("layerMask");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (askingUser) UserChoice();


            else
            {
                if (existingData) DisplayPropField();
                else if (localData) DrawPropFields();
            }

            serializedObject.ApplyModifiedProperties();

            Debug.Log(detectionBehavior.rangeOfVision);
            Debug.Log(detectionBehavior.coneOfVision);
        }

        // ask the user if he's assigning or creating data
        private void UserChoice()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent("Apply an existing data", "Open an objectfield that allows you to assign pre-existing data. Useful for re-using data that is the same for other entities.")))
            {
                existingData = true;
                localData = false;
                askingUser = false;

            }

            if (GUILayout.Button(new GUIContent("Create local data", "Opens property fields for you to set the entity's data. Useful for creating data that won't be the same for other entities")))
            {
                localData = true;
                existingData = false;
                askingUser = false;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        // display a single property field for assigning exsiting scriptableObject data
        private void DisplayPropField()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(entityVisionScriptableProperty);
            if (entityVisionScriptableProperty != null)
            {
                entityVisionScriptableObject = new SerializedObject(entityVisionScriptableProperty.objectReferenceValue);
                detectionBehavior.rangeOfVision = overwatchBehavior.rangeOfVision = entityVisionScriptableObject.FindProperty("rangeOfVision").floatValue;
                detectionBehavior.coneOfVision = overwatchBehavior.coneOfVision = entityVisionScriptableObject.FindProperty("coneOfVision").floatValue;
            }
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button(new GUIContent("Change Data", "Go back to select data")))
            {
                existingData = false;
                askingUser = true;
            }
            EditorGUILayout.EndVertical();
        }

        // draw a property field for creating custom data
        private void DrawPropFields()
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.EndHorizontal();

            detectionBehavior.rangeOfVision = overwatchBehavior.rangeOfVision = rangeOfVision.floatValue = EditorGUILayout.FloatField(new GUIContent("Range of Vision", "Set the entity's range of vision (expressed in Unity's base units)."), rangeOfVision.floatValue);
            detectionBehavior.coneOfVision = overwatchBehavior.coneOfVision = coneOfVision.floatValue = EditorGUILayout.FloatField(new GUIContent("Cone of Vision", "Set the entity's cone of vision (expressed in degrees)."), coneOfVision.floatValue);

            if (GUILayout.Button(new GUIContent("Change Data", "Go back to select data")))
            {
                localData = false;
                askingUser = true;
            }
            EditorGUILayout.EndVertical();
        }
    }
}