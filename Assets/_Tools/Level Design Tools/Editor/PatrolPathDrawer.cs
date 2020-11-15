using Gameplay.VR;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class PatrolPathDrawer : EditorWindow
{
    EntityPatrolData[] patrolEntities;
    EntityPatrolData currentPatrolEntity;
    int selectedIndex = 0;

    string[] patrolEntityNames;
    private bool creatingPath, editingPath;

    Event curEvent;
    List<GameObject> points = new List<GameObject>();
    List<PatrolPoint> patrolPoints = new List<PatrolPoint>();
    GameObject selectedPatrolPoint;

    #region Base Methods
    [MenuItem("Window/Patrol Path Drawer %#x")]
    public static void Init()
    {
        PatrolPathDrawer window = GetWindow<PatrolPathDrawer>();
        window.Show();
    }

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

        foreach (EntityPatrolData entityPatrolEntity in patrolEntities)
        {
            foreach (PatrolPoint patrolPoint in entityPatrolEntity.patrolPointsList)
            {
                GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                newObj.transform.position = patrolPoint.worldPosition;
            }
        }
    }

    private void OnGUI()
    {
        selectedIndex = EditorGUILayout.Popup(selectedIndex, patrolEntityNames);
        currentPatrolEntity = patrolEntities[selectedIndex];

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

        currentPatrolEntity.patrolPointsList = patrolPoints;
    }
    #endregion

    #region Path Methods
    private void CreatePath()
    {
        if (curEvent.type == EventType.MouseDown && curEvent.button == 1)
        {
            GenericMenu generic = new GenericMenu();
            generic.AddDisabledItem(new GUIContent("Patrol Points"), false);
            generic.AddItem(new GUIContent("Create a Patrol Point"), false, () =>
            {
                selectedPatrolPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
                points.Add(selectedPatrolPoint); 
                currentPatrolEntity.patrolPointsList.Add(new PatrolPoint());
                Handles.SphereHandleCap(0, curEvent.mousePosition, Quaternion.identity, 5f, EventType.MouseDrag);
            });

            generic.ShowAsContext();
        }

        foreach (var patrolPoint in points)
            Handles.DoPositionHandle(patrolPoint.transform.position, patrolPoint.transform.rotation);

        for (int i = 0; i < points.Count - 1; i++)
            Handles.DrawLine(points[i].transform.position, points[i + 1].transform.position);

        for (int i = 0; i < points.Count; i++)
            currentPatrolEntity.patrolPointsList[i] = new PatrolPoint(points[i].transform.position, i);
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
    #endregion

    #region Scene GUI Methods
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
    #endregion
}
