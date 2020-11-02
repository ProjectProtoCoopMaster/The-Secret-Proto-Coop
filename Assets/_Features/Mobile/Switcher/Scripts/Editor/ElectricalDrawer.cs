using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Gameplay.Mobile;

namespace Tools.LevelDesign
{
    [CustomEditor(typeof(ElectricalDrawingView))]
    public class ElectricalDrawer : Editor
    {
        ElectricalDrawingView t;
        Rect selectionRect;
        private void OnEnable()
        {
            t = target as ElectricalDrawingView;
        }
        private void OnSceneGUI()
        {
            Event current = Event.current;

            if (current.type == EventType.MouseDown && current.button == 1)
            {
                t.isSelectionning = true;
                OpenSelectionPanel();
                

            }
            else if (current.type == EventType.MouseDown && current.button == 0)
            {
                t.isSelectionning = false;
            }

            if(t.isSelectionning) Handles.DrawSolidRectangleWithOutline(selectionRect, Color.cyan, Color.black);


            SceneView.currentDrawingSceneView.Repaint();

        }

        private void OpenSelectionPanel()
        {
            Event current = Event.current;
            Ray ray = HandleUtility.GUIPointToWorldRay(current.mousePosition);
            current.mousePosition = ray.origin;
            selectionRect = new Rect(current.mousePosition.x, current.mousePosition.y, 2, 1);


        }
    }
}

