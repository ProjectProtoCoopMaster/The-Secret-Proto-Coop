using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class SwitcherManager : MonoBehaviour
    {
        [SerializeField] private SwitcherBehavior[] switchers;
        private void OnEnable() => SearchSwitchersInScene();

        public void RaiseSwitch(float ID)
        {
            for (int i = 0; i < switchers.Length; i++)
            {
                if (switchers[i].ID == ID)
                {
                    switchers[i].SwitchNode();
                }
            }
        }

        public void SearchSwitchersInScene() => switchers = FindObjectsOfType<SwitcherBehavior>();
    }
}

