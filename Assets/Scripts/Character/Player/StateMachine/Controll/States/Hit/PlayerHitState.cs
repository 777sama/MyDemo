using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerHitState : PlayerControllState
    {
        public PlayerHitState(PlayerControllStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            stateMachine.Player.beAttacking = false;
        }


        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();
            
            stateMachine.ChangeState(stateMachine.IdlingState);

        }

        protected override void OnLATKStarted(InputAction.CallbackContext context)
        {
           
        }
        protected override void OnSkillStarted(InputAction.CallbackContext context)
        {
            
        }

        protected override void BeAttack()
        {
           
        }

    }
}
