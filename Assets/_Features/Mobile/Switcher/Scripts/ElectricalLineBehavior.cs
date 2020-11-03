using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Gameplay.Mobile
{
    public class ElectricalLineBehavior : MonoBehaviour, ISwitchable
    {
        [Range(0, 1), SerializeField] private int state;
        [SerializeField] private LineRenderer line;
        public int State { get { return state; } set { state = value; } }

        public void CheckState()
        {
            if (State == 0) TurnOff();
            else TurnOn();
        }

        public void SwitchNode()
        {
            if (State == 0) State = 1;
            else State = 0;

            CheckState();
        }

        public void TurnOff()
        {
            line.startColor = Color.black;
            line.endColor = Color.black;
        }

        public void TurnOn()
        {
            line.startColor = Color.white;
            line.endColor = Color.white;
        }
    }
}

