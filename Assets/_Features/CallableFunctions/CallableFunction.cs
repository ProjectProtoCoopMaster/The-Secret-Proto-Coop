using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class CallableFunction : ScriptableObject
    {
        [SerializeField] private CallableFunctionListener listener = new CallableFunctionListener();
        public void Raise()
        {
            listener.OnEventRaised();
        }
        public void Raise<T>(T ID) => listener.component.SendMessage(listener.methodName, ID);

        public void RegisterListener(CallableFunctionListener otherListener)
        {
            listener = otherListener;
        }

        public void UnRegisterListener()
        {
            listener = null;
        }

    }
}

