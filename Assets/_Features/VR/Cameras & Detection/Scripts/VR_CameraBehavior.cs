using UnityEngine;

namespace Gameplay.VR
{
    public class VR_CameraBehavior : MonoBehaviour, ISwitchable
    {
        [Range(0, 1), SerializeField] private int state;
        [Range(0, 1), SerializeField] private int power;

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

        public GameObject MyGameObject { get { return this.gameObject; } set { MyGameObject = value; } }

        public void TurnOff()
        {

        }

        public void TurnOn()
        {

        }
    }
}