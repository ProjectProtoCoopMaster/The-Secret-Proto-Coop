using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.AI
{
    public enum StateType { Patrol, Distraction, Search, Chase, None, Standby }

    public class AgentManager : MonoBehaviour
    {
        public PatrolBehavior patrolBehavior;
        public DistractionBehavior distractionBehavior;

        public Dictionary<StateType, AgentBehavior> agentBehaviors;

        public StateType currentState { get; set; }
        public StateType previousState { get; set; }

        void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            agentBehaviors = new Dictionary<StateType, AgentBehavior>()
            {
                { StateType.Patrol, patrolBehavior },
                { StateType.Distraction, distractionBehavior }
            };
        }

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
