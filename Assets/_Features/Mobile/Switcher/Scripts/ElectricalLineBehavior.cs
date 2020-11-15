using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Gameplay.Mobile
{
    public class ElectricalLineBehavior : MonoBehaviour, ISwitchable
    {
        [Range(0, 1), SerializeField] private int state;
        [Range(0, 1), SerializeField] private int power;
        [SerializeField] private LineRenderer line;
        public int State { get { return state; } set { state = value; } }
        public GameObject MyGameObject { get { return this.gameObject; } set { MyGameObject = value; } }
        public int Power
        {
            get { return power; }
            set
            {
                power = value;
                if (power == 1)
                {
                    if (state == 1)
                    {
                        TurnOn();
                    }
                    else
                    {
                        TurnOff();
                    }
                }
                else
                {
                    TurnOff();
                }
            }
        }
        private void OnEnable()
        {
            Power = power;
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

        public void SwitchNode(int changeNodes)
        {
            throw new System.NotImplementedException();
        }
    }
}

