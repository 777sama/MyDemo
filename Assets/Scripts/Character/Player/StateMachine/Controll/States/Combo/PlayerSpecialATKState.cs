using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerSpecialATKState : PlayerControllState
    {
        public PlayerSpecialATKState(PlayerControllStateMachine playerControllStateMachine) : base(playerControllStateMachine)
        {
        }


        public override void Enter()
        {
            stateMachine.Player.ATKRange.center = stateMachine.Player.spATK.ATKRangePosition;
            stateMachine.Player.ATKRange.size = stateMachine.Player.spATK.ATKRangeSize;
            base.Enter();
            stateMachine.Player.Animator.CrossFadeInFixedTime("spATK", 0.1555f,0,0f);
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
