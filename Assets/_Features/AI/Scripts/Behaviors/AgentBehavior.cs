using System.Collections;
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

        [ShowIf("type", ActionType.Wait)]
        public float timeToWait;

        [ShowIf("type", ActionType.Watch)]
        public List<Orientation> orientations;
        public List<Vector3> watchDirections { get; set; }
    }

    /// Parent class for all Action Behaviors the entities can perform => Will likely be moved to an interface such as IActionable
    public class ActionBehavior : MonoBehaviour
    {
        public ActionType actionType;

        public virtual void StartActionBehavior(_Action action) { }
        public virtual void ResumeActionBehavior(_Action action) { StartActionBehavior(action); }

        public virtual void StopActionBehavior() { }

        public virtual bool Check(_Action action) { return true; }
    }

    public class AgentBehavior : MonoBehaviour
    {
        public StateType stateType;

        public bool loop;

        protected Dictionary<ActionType, ActionBehavior> actionBehaviors;

        protected List<_Action> actions;

        protected _Action currentAction;
        public ActionType currentActionType { get; set; }
        public ActionType savedActionType { get; set; }

        protected int actionIndex;

        public AgentBehavior previousBehavior { get; set; }

        #region Get
        void Awake()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            actionBehaviors = new Dictionary<ActionType, ActionBehavior>();

            actions = new List<_Action>();
        }
        #endregion

        #region Set
        public void Begin()
        {
            string msg = "There are no actions registered for this behavior ! Consider using Patrol Behavior instead to customize a list of actions.";
            if (Utility.SafeCheck(actions, msg))
            {
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
            if (actionBehaviors[currentActionType].Check(currentAction))
            {
                NextAction();
            }
        }
        #endregion

        #region Next
        protected void NextAction()
        {
            actionIndex++;

            if (actions.Count == actionIndex)
            {
                if (loop) Begin();
                else if (previousBehavior != null) { previousBehavior.Resume(); previousBehavior = null; actionIndex = 0; Stop(); }
            }

            else SetAction(actionIndex);
        }
        #endregion

        #region Pause
        public void Stop()
        {
            savedActionType = currentActionType;
            currentActionType = ActionType.None;

            foreach (ActionBehavior actionBehavior in actionBehaviors.Values)
            {
                actionBehavior.StopActionBehavior();
            }
        }

        public void Resume()
        {
            currentActionType = savedActionType;

            SetActionBehavior(currentActionType);
        }
        #endregion
    }
}
