using Gameplay.VR;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class PatrolPathDrawer : EditorWindow
{
    EntityPatrolData[] patrolEntities;
    string[] patrolEntityNames;
    int index = 0;
    private bool creatingPath, editingPath;
    Event curEvent;
    List<GameObject> patrolPoints = new List<GameObject>();
    GameObject selectedPatrolPoint;

    private void OnEnable()
    {
        patrolEntities = FindObjectsOfType<EntityPatrolData>();
        patrolEntityNames = new string[patrolEntities.Length];

        for (int i = 0; i < patrolEntityNames.Length; i++)
        {
            string patrolEntityName = patrolEntities[i].gameObject.name;
            patrolEntityNames[i] = patrolEntityName;
        }
        curEvent = Event.current;
    }

    [MenuItem("Window/Patrol Path Drawer %#x")]
    public static void Init()
    {
        PatrolPathDrawer window = GetWindow<PatrolPathDrawer>();
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        EditorGUILayout.Popup(index, patrolEntityNames);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("Create Path", "Start creating a patrol path for the currently selected entity.")))
        {
            creatingPath = true;
            editingPath = false;
        }
        if (GUILayout.Button(new GUIContent("Edit Path", "")))
        {
            editingPath = true;
            creatingPath = false;
        }
        if (GUILayout.Button(new GUIContent("Delete Path", ""))) DeletePath();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }

    private void CreatePath()
    {
        if (curEvent.type == EventType.MouseDown && curEvent.button == 1)
        {
            GenericMenu generic = new GenericMenu();
            generic.AddDisabledItem(new GUIContent("Patrol Points"), false);
            generic.AddItem(new GUIContent("Create a Patrol Point"), false, ()=> { patrolPoints.Add(selectedPatrolPoint = GameObject.CreatePrimitive(PrimitiveType.Cube)); });
            generic.ShowAsContext();
        }
        foreach (GameObject patrolPoint in patrolPoints)
            patrolPoint.transform.position = Handles.PositionHandle(patrolPoint.transform.position, patrolPoint.transform.rotation);
    }

    private void EditPath()
    {
        // Do your drawing here using Handles.
        Handles.BeginGUI();
        // Do your drawing here using GUI.
        Handles.EndGUI();
    }

    private void DeletePath()
    {
        throw new NotImplementedException();
    }

    void OnFocus()
    {
        // Remove delegate listener if it has previously
        // been assigned.
        SceneView.duringSceneGui -= CustomOnSceneGUI;
        // Add (or re-add) the delegate.
        SceneView.duringSceneGui += CustomOnSceneGUI;
    }

    void OnDestroy()
    {
        // When the window is destroyed, remove the delegate
        // so that it will no longer do any drawing.
        SceneView.duringSceneGui -= CustomOnSceneGUI;
    }

    void CustomOnSceneGUI(SceneView sceneView)
    {
        if (creatingPath) CreatePath();
        else if (editingPath) EditPath();
    }
}
