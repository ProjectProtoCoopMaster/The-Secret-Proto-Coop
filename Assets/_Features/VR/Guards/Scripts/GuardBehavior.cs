using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class GuardBehavior : MonoBehaviour, IKillable
    {
        public void Die()
        {
            GetComponent<RagdollBehavior>().ActivateRagdoll();
        }

    }

}

