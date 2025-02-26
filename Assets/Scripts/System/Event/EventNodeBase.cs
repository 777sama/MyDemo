using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public enum NodeState
    {
        Waiting,
        Executing,
        Finished
    }
    public class EventNodeBase : ScriptableObject
    {
        protected Action<bool> OnFinished;
        [HideInInspector]public NodeState state;

        public virtual void Init(Action<bool> OnFinishedEvent)
        {
            OnFinished = OnFinishedEvent;
            state = NodeState.Waiting;
        }

        public virtual void Execute()
        {
            if (state != NodeState.Waiting)
            {
                return;
            }
            state = NodeState.Executing;
        }
    }
}
