using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    [Serializable]
    public class DialogueData
    {
        public string Speaker;
        [Multiline] public string Content;
        public bool AutoNext;
        public bool NeedTyping;
        public bool CanQuickShow;
    }

    [CreateAssetMenu(fileName ="Node_",menuName ="Event/Message/Show DIalogue")]
    public class EN_ShowDialogue : EventNodeBase
    {
        public DialogueData[] datas;
        public int boxStyle = 0;
        private int index;

        public override void Execute()
        {
            base.Execute();
            index = 0;
            UIManager.MainInstance.OpenDialogueBox(ShowNextDialogue);
        }

        private void ShowNextDialogue(bool forceDisplayDirectly)
        {
            if (index < datas.Length)
            {
                DialogueData data = new DialogueData()
                {
                    Speaker = datas[index].Speaker,
                    Content = datas[index].Content,
                    AutoNext = datas[index].AutoNext,
                    CanQuickShow = datas[index].CanQuickShow,
                    NeedTyping = !forceDisplayDirectly && datas[index].NeedTyping,
                };
                UIManager.MainInstance.PrintDialogue(data);

                index++;

            }
            else
            {
                state = NodeState.Finished;
                OnFinished(true);
            }
        }
    }
}
