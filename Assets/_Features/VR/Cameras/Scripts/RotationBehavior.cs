using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class RotationBehavior : RotationData
    {
        private void Awake()
        {
            // assign the camera's base rotation
            baseRotation = transform.eulerAngles;

            // assign the camera's target location
            targetRotation = new Vector3(baseRotation.x, baseRotation.y + rotationAngle, baseRotation.z);

            // calculate the increments based on the angle to travel and the time to take
            rotationIncrement = rotationAngle / rotationDuration;
        }

        private void Start()
        {
            StartCoroutine(Rotate(targetRotation));
        }

        private IEnumerator Rotate(Vector3 target)
        {
            // reset the clock
            timePassed = 0f;

            // make the rotationIncrement positive or negative depending on the direction
            rotationIncrement *= Mathf.Sign(target.y - transform.eulerAngles.y);

            // apply the rotation over time
            while (timePassed <= rotationDuration)
            {
                currentRotation = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(new Vector3(currentRotation.x,
                                                                  currentRotation.y + rotationIncrement * Time.deltaTime,
                                                                  currentRotation.z));
                timePassed += Time.deltaTime;
                yield return null;
            }

            // wait for a set amount of time
            yield return new WaitForSeconds(holdTime);

            // flip the values for the next rotation
            Vector3 temp = baseRotation;
            baseRotation = -target;
            targetRotation = temp;

            // call the function recursively
            StartCoroutine(Rotate(targetRotation));
        }
    }
}