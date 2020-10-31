using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class SwitcherBehavior : MonoBehaviour
    {
       
        public int ID;
        

        [SerializeField] private List<Object> nodes;
        public void CallSwitchManager(CallableFunction _switch) => _switch.Raise(ID);

        public void Switch()
        {
            foreach (ISwitchable node in nodes)
            {
                node.SwitchNode();
            }
        }

        public void SearchReferences()
        {
            nodes.Clear();
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.GetComponent<ISwitchable>() != null)
                {
                    nodes.Add(transform.GetChild(i).gameObject.GetComponent<ISwitchable>() as Object);

                }

            }
        }


    }

}
