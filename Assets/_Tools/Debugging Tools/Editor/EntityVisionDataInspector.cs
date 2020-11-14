using Gameplay.VR;
using UnityEngine;
using UnityEditor;

namespace Tools.Debugging
{
    [ExecuteAlways]
    [CanEditMultipleObjects]
    [CustomEditor(typeof(EntityVisionDataInterface))]
    public class EntityVisionDataInspector : Editor
    {
        bool existingData, localData;
        SerializedProperty entityVisionScriptableProperty, rangeOfVision, coneOfVision, playerTransform;

        EntityVisionDataInterface entityVisionDataInterface;
        DetectionBehavior detectionBehavior;
        OverwatchBehavior overwatchBehavior;

        private void OnEnable()
        {
            entityVisionDataInterface = target as EntityVisionDataInterface;
            detectionBehavior = entityVisionDataInterface.gameObject.GetComponent<DetectionBehavior>();
            overwatchBehavior = entityVisionDataInterface.gameObject.GetComponent<OverwatchBehavior>();

            entityVisionScriptableProperty = serializedObject.FindProperty(nameof(entityVisionDataInterface.entityVisionData));
            rangeOfVision = serializedObject.FindProperty(nameof(entityVisionDataInterface.rangeOfVision));
            coneOfVision = serializedObject.FindProperty(nameof(entityVisionDataInterface.coneOfVision));
            playerTransform = serializedObject.FindProperty(nameof(entityVisionDataInterface.playerHead));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (playerTransform.objectReferenceValue == null)
                playerTransform.objectReferenceValue = GameObject.Find("Player");

            if (playerTransform.objectReferenceValue != null)
                detectionBehavior.playerHead = overwatchBehavior.playerHead  = playerTransform.objectReferenceValue as Transform;

            EditorGUILayout.PropertyField(playerTransform);

            if (entityVisionScriptableProperty.objectReferenceValue != null || existingData) DrawScriptableObjProperty();
            else if (rangeOfVision.floatValue != 0 || coneOfVision.floatValue != 0 || localData) DrawLocalProperties();
            else UserChoice();

            serializedObject.ApplyModifiedProperties();
        }

        private void OnSceneGUI()
        {
            Handles.color = Color.red;
            foreach (GameObject guard in overwatchBehavior.visibleGuards)
                Handles.DrawLine(overwatchBehavior.gameObject.transform.position, guard.transform.position);
            Handles.color = Color.white;
        }


        #region User Input
        // ask the user if he's assigning or creating data
        private void UserChoice()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(new GUIContent("Apply an existing data", "Open an objectfield that allows you to assign pre-existing data. Useful for re-using data that is the same for other entities.")))
            {
                existingData = true;
                localData = false;

            }

            if (GUILayout.Button(new GUIContent("Create local data", "Opens property fields for you to set the entity's data. Useful for creating data that won't be the same for other entities")))
            {
                localData = true;
                existingData = false;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        #endregion

        #region Draw Fields
        // display a single property field for assigning exsiting scriptableObject data
        private void DrawScriptableObjProperty()
        {
            EditorGUILayout.BeginVertical();

            if (entityVisionScriptableProperty.objectReferenceValue != null)
            {
                SerializedObject entityVisionScriptableObject = new SerializedObject(entityVisionScriptableProperty.objectReferenceValue);
                detectionBehavior.rangeOfVision = overwatchBehavior.rangeOfVision = entityVisionScriptableObject.FindProperty("rangeOfVision").floatValue;
                detectionBehavior.coneOfVision = overwatchBehavior.coneOfVision = entityVisionScriptableObject.FindProperty("coneOfVision").floatValue;
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(entityVisionScriptableProperty);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button(new GUIContent("Change Data", "Go back to select data")))
            {
                entityVisionScriptableProperty.objectReferenceValue = null;
                existingData = false;
            }

            EditorGUILayout.EndVertical();


        }

        // draw a property field for creating custom data
        private void DrawLocalProperties()
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.EndHorizontal();

            detectionBehavior.rangeOfVision = overwatchBehavior.rangeOfVision = rangeOfVision.floatValue = EditorGUILayout.FloatField(new GUIContent("Range of Vision", "Set the entity's range of vision (expressed in Unity's base units)."), rangeOfVision.floatValue);
            detectionBehavior.coneOfVision = overwatchBehavior.coneOfVision = coneOfVision.floatValue = EditorGUILayout.FloatField(new GUIContent("Cone of Vision", "Set the entity's cone of vision (expressed in degrees)."), coneOfVision.floatValue);

            if (GUILayout.Button(new GUIContent("Change Data", "Go back to select data")))
            {
                rangeOfVision.floatValue = 0;
                coneOfVision.floatValue = 0;
                localData = false;
            }
            EditorGUILayout.EndVertical();
        }
        #endregion
    }
}