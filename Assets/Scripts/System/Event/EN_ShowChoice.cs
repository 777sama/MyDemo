using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    [Serializable]
    public class ChoiceData
    {
        public string Content;
        public bool QuickLocate;
        //音效?
    }
    [CreateAssetMenu(fileName ="Node_",menuName ="Event/Message/Show Choices")]
    public class EN_ShowChoice : EventNodeBase
    {
        public ChoiceData[] datas;
        public SequenceEventExecutor[] executors;
        public int DefaultSelectIndex = 0;

        public override void Init(Action<bool> OnFinishedEvent)
        {
            base.Init(OnFinishedEvent);
            foreach (var item in executors)
            {
                if (item != null)
                {
                    item.Init(OnFinished);
                }
            }
        }

        public override void Execute()
        {
            base.Execute();
            //显示所有的选项
            UIManager.MainInstance.CreateDialogueChocies(datas, OnChoiceConfirm, DefaultSelectIndex);
        }

        private void OnChoiceConfirm(int index)
        {
            if (index < executors.Length && executors[index] != null)
            {
                executors[index].Execute();
            }
            else
            {
                OnFinished(true);
            }
        }
    }
}
