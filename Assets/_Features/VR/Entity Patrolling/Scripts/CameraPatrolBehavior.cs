using System.Collections;
using UnityEngine;

namespace Gameplay.VR
{
    public class CameraPatrolBehavior : EntityPatrolData
    {
        private void Start()
        {
            StartCoroutine(MoveToAltPosition());
        }

        // move the camera to its next patrol point location
        IEnumerator MoveToAltPosition()
        {
            foreach (GameObject points in gameObjects) queue.Enqueue(points);

            while (true)
            {
                if (queue.Count > 0)
                {
                    transform.position = queue.Dequeue().transform.position;
                    yield return new WaitForSeconds(.25f);
                }
                // move in a looping manner
                //else foreach (GameObject points in gameObjects) queue.Enqueue(points);

                else for (int i = gameObjects.Count - 1; i >= 0; i--) queue.Enqueue(gameObjects[i]);
            }
        }
    }
}
