using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Mobile
{
    public class ElectricalDrawingView : MonoBehaviour
    {
        public bool isSelectionning;
        public bool isMouse0Pressed;
        public int numberOfSelectionRect;
        public GameObject switcher;
        public GameObject line;
        public Canvas canvas;
        public GUIStyle selectionNameStyle;
        public List<Vector3> startPoint;
        public List<Vector3> endPoint;
        public List<Vector3> startTangent;
        public List<Vector3> endTangent;
        public bool isDrawingLine;
        public bool canDraw;
        public GameObject selectedGO;
        public GameObject firstSelectedGO;
        public GameObject secondSelectedGO;


        
        

        public void CreateSwitcher()
        {
            GameObject newSwitcher =  Instantiate(switcher,canvas.transform);
            Selection.activeGameObject = newSwitcher;
        }


        
        public void CreateLine()
        {
            for (int i = 0; i < startPoint.Count; i++)
            {
                GameObject newLine = Instantiate(line);

                newLine.GetComponent<ElectricalLinePlacement>().Initialize(firstSelectedGO, secondSelectedGO);
                newLine.GetComponent<LineRenderer>().SetPosition(0, startPoint[i]);
                newLine.GetComponent<LineRenderer>().SetPosition(1, startTangent[i]);
                newLine.GetComponent<LineRenderer>().SetPosition(2, endTangent[i]);
                newLine.GetComponent<LineRenderer>().SetPosition(3, endPoint[i]);
            }
            ClearLineLists();
            firstSelectedGO = null;
            secondSelectedGO = null;
            canDraw = false;

        }

        public void AddLine()
        {


            ClearLineLists();
            startPoint.Add(firstSelectedGO.transform.position);
            endPoint.Add(secondSelectedGO.transform.position);
            startTangent.Add(secondSelectedGO.transform.position);
            endTangent.Add(secondSelectedGO.transform.position);

            

            isSelectionning = false;
            canDraw = true;


        }

        public void ClearLineLists()
        {
            isDrawingLine = false;
            startPoint.Clear();
            endPoint.Clear();
            startTangent.Clear();
            endTangent.Clear();

        }

        public void ChangeTangentToALine()
        {
            int ID = startTangent.Count - 1;
            startTangent[ID] = endPoint[ID];
            endTangent[ID] = endPoint[ID];
        }

        public void ChangeTangentToUpLeft()
        {
            int ID = startTangent.Count - 1;
            startTangent[ID] = new Vector2(startPoint[ID].x, endPoint[ID].y); 
            endTangent[ID] = new Vector2(startPoint[ID].x, endPoint[ID].y);
        }
        public void ChangeTangentToDownLeft()
        {
            int ID = startTangent.Count - 1;
            startTangent[ID] = new Vector2(endPoint[ID].x, startPoint[ID].y); 
            endTangent[ID] = new Vector2(endPoint[ID].x, startPoint[ID].y);
        }

    }
}

