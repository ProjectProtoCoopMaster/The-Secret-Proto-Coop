using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR
{
    public class AwarenessManager : MonoBehaviour
    {
        float time;
        bool killedAlarmRaiser;
        public float timeToAlarm;
        public GameEvent gameOver;

        public void GE_GuardRaisingAlarm()
        {
            // TODO : Implement a progressive spotting mechanic, based on distance
            killedAlarmRaiser = false;
            StartCoroutine(AlarmRaising());
        }

        // interrupt the alarm
        public void GE_KillAlarmRaiser()
        {
            //killedAlarmRaiser = true;
        }

        // instant gameOver
        public void GE_CameraRaisedAlarm()
        {
            // TODO : Implement a progressive spotting mechanic, based on distance
            gameOver.Raise();
        }

        // countdown to raise the alarm, can be interrupted
        private IEnumerator AlarmRaising()
        {
            time = 0f;
            Debug.Log("Raising the Alarm !");

            while (time < timeToAlarm)
            {
                time += Time.deltaTime;
                if (killedAlarmRaiser)
                {
                    Debug.Log("Alarm was interrupted");
                    break;
                }

                // if enough time passes uninterrupted, the gameOver event is raised
                if (time >= timeToAlarm) gameOver.Raise();

                yield return null;
            }


            yield return null;
        }
    }
}