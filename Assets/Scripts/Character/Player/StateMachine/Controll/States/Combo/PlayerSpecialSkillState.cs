using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerSpecialSkillState : PlayerControllState
    {
        public PlayerSpecialSkillState(PlayerControllStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
           
            base.Enter();
            stateMachine.Player.ATKRange.center = stateMachine.Player.currentCombo.TryGetComboATKRangePosition(stateMachine.Player.currentComboIndex);
            stateMachine.Player.ATKRange.size = stateMachine.Player.currentCombo.TryGetComboATKRangeSize(stateMachine.Player.currentComboIndex);
            stateMachine.Player.ATKRange.gameObject.SetActive(true);
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();

            if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
                return;
            }
            stateMachine.ChangeState(stateMachine.SprintingState);
        }
        public override void Exit()
        {
            base.Exit();
            stateMachine.Player.ATKRange.gameObject.SetActive(false);
        }

        protected override void OnLATKStarted(InputAction.CallbackContext context)
        {
           
        }
    }
}
