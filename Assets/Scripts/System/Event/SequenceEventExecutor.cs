using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

namespace zzz
{
    [CreateAssetMenu(fileName ="Executor_",menuName =("Event/Sequence Executor"))]
    public class SequenceEventExecutor : ScriptableObject
    {
        public Action<bool> OnFinished;

        private int index;
        public EventNodeBase[] nodes;

        public void Init(Action<bool> onFinishedEvent)
        {
            index = 0;

            foreach(EventNodeBase item in nodes)
            {
                if (item != null)
                {
                    item.Init(OnNodeFinished);
                }
            }

            OnFinished = onFinishedEvent;
        }

        private void OnNodeFinished(bool success)
        {
            if (success)
            {
                ExecuteNextNode();
            } else 
            {
                OnFinished(false);
            }
        }

        private void ExecuteNextNode()
        {
            if (index < nodes.Length)
            {
                if (nodes[index].state == NodeState.Waiting)
                {
                    index++;
                    nodes[index-1].Execute();
                    
                }
            }
            else
            {
                OnFinished(true);
            }
        }

        public void Execute()
        {
            
            index = 0;
            ResetTalk();
            ExecuteNextNode();
        }

        public void ResetTalk()
        {
            foreach (var node in nodes)
            {
                node.state = NodeState.Waiting;
            }
        }
    }
}
