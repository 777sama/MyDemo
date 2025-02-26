using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerSwitchDefenseState : PlayerControllState
    {
        public PlayerSwitchDefenseState(PlayerControllStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //stateMachine.Player.MeshHighlighter.HighlightMesh(true);
            stateMachine.Player.Animator.Play("SwitchDefense");
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

        public override void OnAnimationEnterEvent()
        {
            base.OnAnimationEnterEvent();
            FightingEventManager.MainInstance.TriggerFreezeOnDefense();
        }

        public override void Exit()
        {
            stateMachine.Player.beAttacking = false;
            base.Exit();
            //stateMachine.Player.MeshHighlighter.HighlightMesh(false);
        }

        protected override void OnLATKStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.SpecialATKState);
        }
    }
}
