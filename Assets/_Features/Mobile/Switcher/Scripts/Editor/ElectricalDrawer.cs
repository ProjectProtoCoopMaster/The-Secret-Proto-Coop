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
        Vector2 mousePos;

        private void OnEnable()
        {
            t = target as ElectricalDrawingView;
        }
        private void OnSceneGUI()
        {
            Event current = Event.current;
            HandleUtility.AddDefaultControl(0);
            


            if (current.type == EventType.MouseDown)
            {
                if (current.button == 1)
                {
                    t.isSelectionning = true;
                    OpenSelectionPanel();
                }
                else if (current.button == 0)
                {
                    if (!selectionRect.Contains(HandleUtility.GUIPointToWorldRay(current.mousePosition).origin))
                    {
                        t.isSelectionning = false;
                    }

                    t.isMouse0Pressed = true;

                }

            }
            else if (current.type == EventType.MouseUp )
            {
                if (current.button == 0)
                {
                    t.isMouse0Pressed = false;
                }

            
            }

            if (t.isSelectionning)
            {
                DrawSelectionRects();
            }

            SceneView.currentDrawingSceneView.Repaint();

        }

        private void OpenSelectionPanel()
        {
            Event current = Event.current;
            Ray ray = HandleUtility.GUIPointToWorldRay(current.mousePosition);
            mousePos = ray.origin;
        }

        private void DrawSelectionRects()
        {
            Event current = Event.current;

            if (t.numberOfSelectionRect == 1)
                selectionRect = new Rect(mousePos.x, mousePos.y, 4, (1.5f * t.numberOfSelectionRect));
            else selectionRect = new Rect(mousePos.x, mousePos.y, 4, (1.5f * t.numberOfSelectionRect - (t.numberOfSelectionRect * .5f)));


            
            Handles.DrawSolidRectangleWithOutline(selectionRect, Color.cyan, Color.black);
            for (int i = 0; i < t.numberOfSelectionRect; i++)
            {

                Rect genericRect = new Rect(selectionRect.x + selectionRect.width - 3.5f , selectionRect.y + selectionRect.height -1.1f, 3, .8f);
                Rect switcherRect = new Rect(genericRect.x, genericRect.y- (i * (genericRect.height *1.1f) ), genericRect.width, genericRect.height);

                if (switcherRect.Contains(HandleUtility.GUIPointToWorldRay(current.mousePosition).origin))
                {
                   
                    if (t.isMouse0Pressed)
                    {
                        Handles.DrawSolidRectangleWithOutline(switcherRect, Color.red, Color.black);
                        if (current.type == EventType.MouseDown)
                            t.CreateSwitcher();
                    }
                    else
                    {
                        Handles.DrawSolidRectangleWithOutline(switcherRect, Color.cyan, Color.black);
                    }

                }
                else
                {
                    Handles.DrawSolidRectangleWithOutline(switcherRect, Color.cyan, Color.black);
                }


                
                
               
            }


        }
    }
}

