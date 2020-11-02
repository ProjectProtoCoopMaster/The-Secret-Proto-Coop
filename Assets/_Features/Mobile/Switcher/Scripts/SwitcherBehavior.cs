using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class SwitcherBehavior : MonoBehaviour, ISwitchable
    {
        [Header("---References---")]
        public CallableFunction _switch = default;
        public CallableFunction _sendSwitcherChange = default;
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        public List<Object> nodes = default;

        [Header("---IMPORTANT---")]
        [Range(0, 10)]
        public float ID = default;
        public enum SwitchTimer { None, Fixed}

        public SwitchTimer switchTimer = default;
        [HideInInspector]
        public float timer = 0;
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
            if (switchTimer == SwitchTimer.Fixed && button.enabled)
                StartCoroutine(DelaySwitchNode());
            
        }

        IEnumerator DelaySwitchNode()
        {
            button.enabled = false;
            image.color = Color.grey;
            yield return new WaitForSeconds(timer);
            SwitchNode();
            button.enabled = true;
            image.color = Color.white;
            yield break;
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
