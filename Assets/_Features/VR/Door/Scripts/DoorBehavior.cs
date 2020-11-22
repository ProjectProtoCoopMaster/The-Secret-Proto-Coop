using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace Gameplay.VR
{
    public class DoorBehavior : MonoBehaviour, ISwitchable
    {
        public enum LockState { Locked, Unlocked }
        public LockState lockState;
        [Range(0, 1), SerializeField] private int state;
        [Range(0, 1), SerializeField] private int power;

        [SerializeField] private Collider collider;
        [SerializeField] private Material red, orange, blue;
        [SerializeField] private Renderer keyPassRenderer;
        public int State
        {
            get { return state; }
            set { state = value; }

        }

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

        public GameObject MyGameObject { get { return this.gameObject; } set { MyGameObject = value; } }


        private void Start() => Power = power;

        public void TurnOn()
        {
            if (lockState == LockState.Locked) { keyPassRenderer.material = orange; collider.enabled = false; }
            else Unlock();
        }

        public void TurnOff()
        {
            if (lockState == LockState.Locked) { keyPassRenderer.material = red; collider.enabled = false; }
        }

        [Button("Unlock")]
        public void Unlock()
        {

            collider.enabled = true;
            keyPassRenderer.material = blue;
            lockState = LockState.Unlocked;
        }
    }

}

