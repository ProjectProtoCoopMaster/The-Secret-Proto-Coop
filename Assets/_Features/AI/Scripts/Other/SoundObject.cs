using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.AI;

public class SoundObject : MonoBehaviour
{
    public LayerMask layerMask;
    public float radius;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            _Distraction();
        }
    }

    public void _Distraction()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

        foreach(Collider col in colliders)
        {
            if (col.transform.parent.tag == "Enemy")
            {
                AgentManager agent = col.transform.parent.GetComponent<AgentManager>();

                DistractionBehavior distractionBehavior = agent.agentBehaviors[StateType.Distraction] as DistractionBehavior;
                distractionBehavior.SetDistraction(transform.position);

                agent.SwitchState(StateType.Distraction);
            }
        }
    }
}
