using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class RotationBehavior : RotationData
    {
        bool movingRight;

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
            StartCoroutine(Rotate());
        }

        private IEnumerator Rotate()
        {
            // reset the clock
            timePassed = 0f;

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

            // multiply the incrementor by -1 to switch its sign for the next pass
            rotationIncrement *= -1;

            // call the function recursively
            StartCoroutine(Rotate());
        }
    }
}