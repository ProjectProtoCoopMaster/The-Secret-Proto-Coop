using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class GuardMortalityBehavior : MonoBehaviour
    {
        public void Shot()
        {
            gameObject.name = "DEAD";
        }
    }
}
