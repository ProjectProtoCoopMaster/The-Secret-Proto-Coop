using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class PlayerBehavior : MonoBehaviour, IKillable
    {
        [SerializeField] private CallableFunction _GameOver;
        public void GE_Die()
        {
            _GameOver.Raise();
        }

    }
}

