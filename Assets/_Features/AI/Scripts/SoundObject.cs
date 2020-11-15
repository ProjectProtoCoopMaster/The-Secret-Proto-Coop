using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    public float radius;

    public void GE_MakeSound()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider col in colliders)
        {
            if (col.tag == "Enemy")
            {
                DistractionBehavior distraction = col.GetComponent<DistractionBehavior>();
                distraction.DistractTo(transform.position);
            }
        }
    }
}
