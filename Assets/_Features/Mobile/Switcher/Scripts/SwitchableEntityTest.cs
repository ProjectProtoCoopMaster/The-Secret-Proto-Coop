using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class SwitchableEntityTest : MonoBehaviour, ISwitchable
    {

        [Range(0,1),SerializeField] private int state;
        [SerializeField] private Color offColor;
        [SerializeField] private Color onColor;
        

        private void Start() => CheckState();

        public int State { get { return state; } set { state = value; } }
        public void SwitchNode() 
        {
            if (State == 0) State = 1;
            else State = 0;

            CheckState();
        }

        public void CheckState()
        {
            if (State == 0) TurnOn();
            else TurnOff();
        }

        private void TurnOff() { GetComponent<Image>().color = offColor; }
        private void TurnOn() { GetComponent<Image>().color = onColor; }
        
    }
}

