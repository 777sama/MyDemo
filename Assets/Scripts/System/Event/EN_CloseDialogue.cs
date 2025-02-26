using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    [CreateAssetMenu(fileName ="Node_",menuName =("Event/Message/Close Dialogue"))]
    public class EN_CloseDialogue : EventNodeBase
    {
        public override void Execute()
        {
            base.Execute();

            UIManager.MainInstance.CloseDialogueBox();
            Cursor.lockState = CursorLockMode.Locked;
            state = NodeState.Finished;
            OnFinished(true);
        }
    }
}
