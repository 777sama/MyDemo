using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class PlayerUltimateSkillState : PlayerControllState
    {
        public PlayerUltimateSkillState(PlayerControllStateMachine playerControllStateMachine) : base(playerControllStateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.Player.ATKRange.center = stateMachine.Player.ultimateSKill.ATKRangePosition;
            stateMachine.Player.ATKRange.size = stateMachine.Player.ultimateSKill.ATKRangeSize;
            base.Enter();
            stateMachine.Player.ExCameraTimeLine.SetActive(true);
            stateMachine.Player.Animator.Play("Ex_Start");
            stateMachine.Player.ATKRange.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            base.Exit();
            stateMachine.Player.ATKRange.gameObject.SetActive(false);
        }

        public override void Update()
        {
            
        }

        public override void PhysicsUpdate()
        {
            
        }

        protected override void AddInputActionsCallbacks()
        {
            
        }

        protected override void RemoveInputActionsCallbacks()
        {
            
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

        
    }
}
