using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class SwitcherBehavior : MonoBehaviour, ISwitchable
    {
        [Header("---IMPORTANT---")]
        [Range(0,10)]
        public float ID;

        [Header("---References---")]
        [SerializeField] private CallableFunction _switch;
        [SerializeField] private CallableFunction _sendSwitcherChange;
        [SerializeField] private List<Object> nodes;

        public int State { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void CallSwitchManager()
        { 
            _switch.Raise(ID);
            _sendSwitcherChange.Raise(ID);
        }

        public void SwitchNode()
        {
            foreach (ISwitchable node in nodes)
            {
                node.SwitchNode();
            }
        }

        public void CheckState()
        {
            throw new System.NotImplementedException();
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
