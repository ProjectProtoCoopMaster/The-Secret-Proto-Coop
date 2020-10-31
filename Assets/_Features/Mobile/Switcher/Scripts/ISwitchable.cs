using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public interface ISwitchable
    {
        int State { get; set; }
        void SwitchNode();

        void CheckState();
    }
}

