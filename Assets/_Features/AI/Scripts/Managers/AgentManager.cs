using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.AI
{
    public enum StateType { Patrol, Distraction, Search, Chase, None, Standby }

    public abstract class AgentManager : MonoBehaviour
    {
        public Dictionary<StateType, AgentBehavior> agentBehaviors = new Dictionary<StateType, AgentBehavior>();

        public StateType currentState { get; set; }
        public StateType previousState { get; set; }

        void Awake()
        {
            InitializeAgent();
        }

        protected abstract void InitializeAgent();

        void Start()
        {
            currentState = StateType.Patrol;

            agentBehaviors[currentState].Begin();
        }

        public void SwitchState(StateType newState)
        {
            previousState = currentState;
            currentState = newState;

            foreach(AgentBehavior agentBehavior in agentBehaviors.Values)
            {
                agentBehavior.Stop();
            }

            agentBehaviors[currentState].previousBehavior = agentBehaviors[previousState];
            agentBehaviors[currentState].Begin();
        }
    }
}
