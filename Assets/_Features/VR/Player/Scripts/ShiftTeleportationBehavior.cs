using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Valve.VR;

namespace Gameplay.VR.Player
{
    public class ShiftTeleportationBehavior : AgentStateData
    {
        // shift teleportation behavior
        [SerializeField] protected float movementSpeed;

        [SerializeField] float minDashRange = 0.5f, maxDashRange = 40f, dashTime = 0.2f;

        [SerializeField] Animator maskAnimator;

        SteamVR_Action teleportAction;
        Transform cameraRigRoot;


        // a reference to the action
        public SteamVR_Action_Boolean teleport;
        // a reference to the hand
        public SteamVR_Input_Sources handType;

        float time;
        Vector3 change;
        Vector3 startValue;
        Vector3 targetValue;
        float tweenDuration;

        Vector3 movingPosition;


        private void Start()
        {
            teleport.AddOnStateDownListener(TriggerDown, handType);
            teleport.AddOnStateUpListener(TriggerUp, handType);
        }

        private void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {

        }

        private void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {

        }

        //example function
        IEnumerator MoveValue(Vector3 targetPos)
        {
            startValue = transform.position;
            change = targetPos - startValue;

            while (time <= tweenDuration)
            {
                time += Time.deltaTime;
                movingPosition.x = TweenManagerLibrary.LinearTween(time, startValue.x, change.x, tweenDuration);
                movingPosition.x = TweenManagerLibrary.LinearTween(time, startValue.z, change.z, tweenDuration);

                transform.position = movingPosition;
                yield return null;
            }
        }
    }
}