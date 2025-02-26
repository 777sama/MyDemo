using GGG.Tool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerNormalATKState : PlayerControllState
    {

        
        public PlayerNormalATKState(PlayerControllStateMachine playerControllStateMachine) : base(playerControllStateMachine)
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
