﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Mobile
{
    public class DoorBehavior : MonoBehaviour, ISwitchable
    {
        [Range(0, 1), SerializeField] private int state;
        [Range(0, 1), SerializeField] private int power;
        public GameObject MyGameObject { get { return this.gameObject; } set { MyGameObject = value; } }
        public int State { get { return state; } set { state = value; } }
        public int Power
        {
            get { return power; }
            set
            {
                power = value;
                if (power == 1) if (state == 1) TurnOn(); else TurnOff();
                else TurnOff();
            }
        }

        private void Start() => Power = power;

        public void TurnOff() { GetComponent<Image>().color = Color.black; }
        public void TurnOn() { GetComponent<Image>().color = Color.white; }
    }
}

