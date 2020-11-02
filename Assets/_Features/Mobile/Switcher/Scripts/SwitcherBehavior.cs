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
        [Range(0, 1), SerializeField] private int state;
        [Range(0, 10)]
        public float ID = default;
        public enum SwitchTimer { None, Fixed}

        public SwitchTimer switchTimer = default;
        [HideInInspector]
        public float timer = 0;

        public int State { get { return state; } set { state = value; } }


        private void Start() => CheckState();

        public void CallSwitchManager()
        { 
            _switch.Raise(ID);
            _sendSwitcherChange.Raise(ID);
        }

        public void SwitchNode()
        {
            if (State == 0) State = 1;
            else State = 0;

            CheckState();

        }

        public void TriggerSwitch()
        {
            foreach (ISwitchable node in nodes)
            {
                node.SwitchNode();
            }
            if (switchTimer == SwitchTimer.Fixed && button.interactable)
                StartCoroutine(DelaySwitchNode());
        }

        IEnumerator DelaySwitchNode()
        {
            TurnOff();
            yield return new WaitForSeconds(timer);
            TriggerSwitch();
            TurnOn();
            yield break;
        }

        public void CheckState()
        {
            if (State == 0) TurnOff();
            else TurnOn();
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

        public void TurnOn()
        {
            ChangeSwitch(true, Color.white);
        }

        public void TurnOff()
        {
            ChangeSwitch(false, Color.black);
        }

        void ChangeSwitch(bool buttonState, Color buttonColor)
        {
            button.interactable = buttonState;
        }
    }

}
