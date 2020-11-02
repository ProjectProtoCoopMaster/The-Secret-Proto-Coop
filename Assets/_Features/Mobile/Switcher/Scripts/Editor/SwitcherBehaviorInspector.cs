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
        private void OnEnable()
        {
            switcher = target as SwitcherBehavior;
            
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

           

            if(switcher.switchTimer == SwitcherBehavior.SwitchTimer.Fixed)
            {
                switcher.timer = EditorGUILayout.FloatField("Timer",switcher.timer);
            }

            if (GUILayout.Button("Search Nodes References"))
            {
                switcher.SearchReferences();
            }

            EditorUtility.SetDirty(switcher);

        }
    }
}

