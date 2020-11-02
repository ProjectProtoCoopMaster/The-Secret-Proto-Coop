using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Mobile
{
    public class ElectricalDrawingView : MonoBehaviour
    {
        public bool isSelectionning;
        public bool isMouse0Pressed;
        public int numberOfSelectionRect;
        public GameObject switcher;
        public Canvas canvas;
        public GUIStyle selectionNameStyle;

        public void CreateSwitcher()
        {
            Instantiate(switcher,canvas.transform);
        }
        

    }
}

