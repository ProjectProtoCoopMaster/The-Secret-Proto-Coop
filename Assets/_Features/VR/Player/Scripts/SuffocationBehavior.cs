using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR.Player
{
    public class SuffocationBehavior : AgentStateData
    {
        // suffocation behavior
        [SerializeField] protected float oxygenLevel;
        [SerializeField] protected float oxygenDepletionRate;

        void GE_DepleteOxygen()
        {
            StartCoroutine(DepleteOxygen());
        }

        IEnumerator DepleteOxygen()
        {
            oxygenLevel -= oxygenDepletionRate * Time.deltaTime;
            yield return null;

            if (oxygenLevel <= 0) gameOver.Raise();
        }
    }
}