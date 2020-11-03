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
        Event current;

        private void OnEnable()
        {
            t = target as ElectricalDrawingView;
        }
        private void OnSceneGUI()
        {
            current = Event.current;

            t.selectedGO = Selection.activeGameObject;




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
                HandleUtility.AddDefaultControl(0);
            }

                


            Undo.RecordObject(t, "Undo Electrical");

            if (t.canDraw) DrawLines();

            if(current.type == EventType.KeyDown)
            {
                if (current.keyCode == KeyCode.A)
                {
                    t.CreateLine();

                    current.Use();
                }
                else if (current.keyCode == KeyCode.E)
                {
                    t.ChangeTangentToALine();
                }
                else if (current.keyCode == KeyCode.R)
                {
                    t.ChangeTangentToUpLeft();
                }
                else if (current.keyCode == KeyCode.T)
                {
                    t.ChangeTangentToDownLeft();
                }
                else if (current.keyCode == KeyCode.Space)
                {
                    if (t.isDrawingLine)
                    {
                        if (t.firstSelectedGO == null)
                        {
                            t.firstSelectedGO = t.selectedGO;

                            current.Use();
                        }
                        else
                        {
                            
                            t.secondSelectedGO = t.selectedGO;

                            t.AddLine();

                            current.Use();
                        }
                    }
                }
            }



            SceneView.currentDrawingSceneView.Repaint();

        }

        private void OpenSelectionPanel()
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(current.mousePosition);
            mousePos = ray.origin;
        }

        private void DrawSelectionRects()
        {

            if (t.numberOfSelectionRect == 1)
                selectionRect = new Rect(mousePos.x, mousePos.y, 4, (1.5f * t.numberOfSelectionRect));
            else selectionRect = new Rect(mousePos.x, mousePos.y, 4, (1.5f * t.numberOfSelectionRect - (t.numberOfSelectionRect * .4f)));


            
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
                        {
                            switch (i)
                            {
                                case 0:
                                    t.CreateSwitcher();
                                    current.Use();
                                    break;
                                case 1:

                                    t.isDrawingLine = true;
                                    current.Use();
                                    break;
                            }

                        }
                            

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

        private void DrawLines()
        {

            int ID = t.startPoint.Count - 1;


            t.startPoint[ID] = Handles.PositionHandle(t.startPoint[ID], Quaternion.identity);
            Handles.DrawSolidRectangleWithOutline(new Rect(t.startPoint[ID].x, t.startPoint[ID].y, .1f, .1f), Color.red, Color.black);
            t.startTangent[ID] = Handles.PositionHandle(t.startTangent[ID], Quaternion.identity);
            Handles.DrawSolidRectangleWithOutline(new Rect(t.startTangent[ID].x, t.startTangent[ID].y, .1f, .1f), Color.green, Color.black);
            t.endTangent[ID] = Handles.PositionHandle(t.endTangent[ID], Quaternion.identity);
            Handles.DrawSolidRectangleWithOutline(new Rect(t.endTangent[ID].x, t.endTangent[ID].y, .1f, .1f), Color.cyan, Color.black);
            t.endPoint[ID] = Handles.PositionHandle(t.endPoint[ID], Quaternion.identity);
            Handles.DrawSolidRectangleWithOutline(new Rect(t.endPoint[ID].x, t.endPoint[ID].y, .1f, .1f), Color.magenta, Color.black);


            for (int i = 0; i < t.startPoint.Count; i++)
            {
                Handles.DrawBezier(t.startPoint[i], t.endPoint[i], t.startTangent[i], t.endTangent[i], Color.red, null, 5f);
            }

        }

    }
}

