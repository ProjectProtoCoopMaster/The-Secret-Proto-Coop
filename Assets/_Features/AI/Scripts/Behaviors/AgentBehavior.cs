﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

#region Utility
public class Utility
{
    public static bool SafeCheck<T>(T element, string message)
    {
        if (element == null)
        {
            Debug.Log(message);
            return false;
        }
        else return true;
    }

    public static bool SafeCheck<T>(List<T> list, string message)
    {
        if (list.Count == 0)
        {
            Debug.Log(message);
            return false;
        }
        else return true;
    }
}
#endregion

namespace Gameplay.AI
{
    public enum ActionType { Move, Wait, Watch, None, Search }

    [System.Serializable]
    public class _Action
    {
        public ActionType actionType;

        [ShowIf("type", ActionType.Move)]
        public Vector3 destination;
        public float area;

        [ShowIf("type", ActionType.Wait)]
        public float timeToWait;

        [ShowIf("type", ActionType.Watch)]
        public Vector3 watchDirection;

        [ShowIf("type", ActionType.Search)]
        public Vector3 watchRotation;
    }

    public abstract class ActionBehavior : MonoBehaviour
    {
        public ActionType actionType;

        public abstract void StartActionBehavior(_Action action);
        public virtual void ResumeActionBehavior(_Action action) { StartActionBehavior(action); }

        public abstract void StopActionBehavior();

        public abstract bool Check();
    }

    public abstract class AgentBehavior : MonoBehaviour
    {
        public StateType stateType;

        public bool loop;

        private bool active;

        protected Dictionary<ActionType, ActionBehavior> actionBehaviors = new Dictionary<ActionType, ActionBehavior>();

        protected List<_Action> actions = new List<_Action>();

        protected _Action currentAction;
        public ActionType currentActionType { get; set; }
        public ActionType savedActionType { get; set; }

        protected int actionIndex;

        public AgentBehavior previousBehavior { get; set; }

        #region Get
        void Awake()
        {
            InitializeBehavior();
        }

        protected abstract void InitializeBehavior();
        #endregion

        #region Set
        public void Begin()
        {
            string msg = "There are no actions registered for " + this + ", likely, the behavior did not initialize correctly";
            if (Utility.SafeCheck(actions, msg))
            {
                active = true;
                actionIndex = 0;
                SetAction(actionIndex);
            }
        }

        protected void SetAction(int index)
        {
            currentAction = actions[index];
            currentActionType = currentAction.actionType;

            SetActionBehavior(currentActionType);
        }

        protected void SetActionBehavior(ActionType actionType)
        {
            if (Utility.SafeCheck(actions, "There is no Action Behavior associated to the Action Type :" + actionType))
            {
                actionBehaviors[actionType].StartActionBehavior(currentAction);
            }
        }
        #endregion

        #region Loop
        void Update()
        {
            if (active)
            {
                if (actionBehaviors[currentActionType].Check())
                {
                    NextAction();
                }
            }
        }
        #endregion

        #region Next
        protected void NextAction()
        {
            actionIndex++;

            Debug.Log(actionIndex + " " + actions.Count + " " + this);
            if (actions.Count == actionIndex)
            {
                if (loop) Begin();
                else if (previousBehavior != null) { previousBehavior.Resume(); previousBehavior = null; actionIndex = 0; active = false; }
                else { actionIndex = 0; active = false; }
            }

            else SetAction(actionIndex);
        }
        #endregion

        #region Pause
        public void Stop()
        {
            active = false;
            savedActionType = currentActionType;

            foreach (ActionBehavior actionBehavior in actionBehaviors.Values)
            {
                actionBehavior.StopActionBehavior();
            }
        }

        public void Resume()
        {
            active = true;
            currentActionType = savedActionType;

            SetActionBehavior(currentActionType);
        }
        #endregion
    }
}
