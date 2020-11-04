using System.Collections;
using UnityEngine;

namespace Gameplay.VR
{
    public class CameraPatrolBehavior : EntityPatrolData
    {
        // move the camera to its next patrol point location
        IEnumerator MoveToAltPosition()
        {
            yield return null;
        }
    }
}
