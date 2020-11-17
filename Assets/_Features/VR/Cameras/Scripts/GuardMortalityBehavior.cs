using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class GuardMortalityBehavior : MonoBehaviour
    {
        public GameEvent killed;

        public void Shot()
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            gameObject.name = "DEAD";
            killed.Raise();
        }
    }
}
