using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.AI
{
    public class WatchAction : ActionBehavior
    {
        public float rotateSpeed;

        private bool watch = false;

        private List<Vector3> directions;
        private Vector3 currentDirection;
        private int index;

        private Quaternion currentRotation;
        private float[] angles = new float[2];
        private float angleOffset = 1f;

        private float time;

        public override void StartActionBehavior(_Action action)
        {
            if (action.watchDirections == null) return;

            directions = action.watchDirections;
            index = 0;
            SetDirection(index);
            watch = true;
        }

        public override void StopActionBehavior()
        {
            watch = false;
        }

        public override bool Check(_Action action)
        {
            return !watch;
        }

        #region Set
        public void SetDirection(int index)
        {
            currentDirection = directions[index];

            Vector3 target = transform.position + currentDirection;
            Vector3 position = target - transform.position;
            position.y = 0.0f;

            currentRotation = Quaternion.LookRotation(position, Vector3.up);
            angles[0] = currentRotation.eulerAngles.y - angleOffset;
            angles[1] = currentRotation.eulerAngles.y + angleOffset;

            time = 0.0f;
        }
        #endregion

        #region Loop
        void Update()
        {
            if (watch)
            {
                WatchDirection();

                if (transform.rotation.eulerAngles.y >= angles[0] && transform.rotation.eulerAngles.y <= angles[1])
                {
                    NextDirection();
                }
            }
        }

        public void WatchDirection()
        {
            time += (Time.deltaTime * rotateSpeed);

            transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, time);

            //Debug.Log(currentRotation.eulerAngles + " for " + gameObject.name);
            //Debug.Log(transform.rotation.eulerAngles + " for " + gameObject.name);
        }
        #endregion

        #region Next
        private void NextDirection()
        {
            index++;

            if (directions.Count == index) watch = false;

            else SetDirection(index);
        }
        #endregion
    } 
}
