using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.AI
{
    public class MoveAction : ActionBehavior
    {
        public Transform target;

        public override void StartActionBehavior(_Action action)
        {
            SetMove(action.destination, true);
        }

        public override void StopActionBehavior()
        {
            Debug.Log(this);
            SetMove(target.position, false);
        }

        public override bool Check(_Action action)
        {
            if (IsInArea(target.position, action.destination, 0.5f))
            {
                SetMove(target.position, false);

                return true;
            }
            else return false;
        }

        public virtual void SetMove(Vector3 direction, bool move) { }

        public bool IsInArea(Vector3 objectPos, Vector3 destPos, float area)
        {
            if (objectPos.x <= (destPos.x + area) && objectPos.x >= (destPos.x - area))
            {
                if (objectPos.z <= (destPos.z + area) && objectPos.z >= (destPos.z - area))
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
    } 
}
