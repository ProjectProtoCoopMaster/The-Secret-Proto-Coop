using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Gameplay
{
    [CustomEditor(typeof(SwitcherBehavior))]
    public class SwitcherBehaviorInspector : Editor
    {
        SwitcherBehavior switcher;
        int ID;
        private void OnEnable()
        {
            switcher = target as SwitcherBehavior;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Search Nodes References"))
            {
                switcher.SearchReferences();
            }

        }
    }
}

