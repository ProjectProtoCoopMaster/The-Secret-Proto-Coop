using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ElectricalDrawingView))]
public class ElectricalDrawer : Editor
{

    private void OnSceneGUI()
    {
        ElectricalDrawingView t = target as ElectricalDrawingView;
         Event cur = Event.current;

        //t.pos = SceneView.currentDrawingSceneView.camera.ScreenToWorldPoint(cur.mousePosition);
        t.pos = t.transform.position;
        t.pos = new Vector3(t.pos.x, t.pos.y, 0);
        Rect rect = new Rect(t.pos.x, t.pos.y, 1, 1);
        Handles.DrawSolidRectangleWithOutline(rect, Color.cyan, Color.black);

        if (cur.type == EventType.KeyDown)
        {
            t.isDrawing = true;
        }


    }
}
