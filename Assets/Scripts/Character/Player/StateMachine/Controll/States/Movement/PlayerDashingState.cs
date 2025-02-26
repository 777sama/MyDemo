using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

namespace zzz
{
    public class PlayerDashingState : PlayerGroundedState
    {
        private PlayerDashData dashData;
        private float startTime;
        private int consecutiveDashesUsed;
        public PlayerDashingState(PlayerControllStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            dashData = movementData.DashData;
        }

        public override void Enter()
        {
            
            FightingEventManager.MainInstance.player = stateMachine.Player;
            if (stateMachine.Player.beAttacking)
            {   
                FightingEventManager.MainInstance.TriggerFreezeOnDash();
                //stateMachine.Player.MeshHighlighter.HighlightMesh(true);
            }
            ResetComboInfo();
            stateMachine.ReusableData.MovementSpeedModifier = dashData.SpeedModifier;

            if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.Player.Animator.Play("DashBack");
            }
            else
            {
                stateMachine.Player.Animator.Play("DashFront");
            }
            
            base.Enter();
            


            UpdateConsecutiveDashes();

            startTime = Time.time;
        }

        public override void Exit()
        {
            base.Exit();
            stateMachine.Player.beAttacking = false;
            //stateMachine.Player.MeshHighlighter.HighlightMesh(false);
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

        private void UpdateConsecutiveDashes()
        {
            if (!IsConsecutive())
            {
                consecutiveDashesUsed = 0;
            }
            ++consecutiveDashesUsed;

            if (consecutiveDashesUsed == dashData.ConsecutiveDashedLimitAmount)
            {
                consecutiveDashesUsed = 0;

                stateMachine.Player.Input.DisableActionFor(stateMachine.Player.Input.PlayerActions.Dash,dashData.DashLimitReachedCooldown);
            }
        }



        private bool IsConsecutive()
        {
            return Time.time < startTime + dashData.TimeToBeConsideredConsecutive;
        }

        protected override void OnDashStart(InputAction.CallbackContext context)
        {
            
        }

        protected override void OnMovementCancele(InputAction.CallbackContext context)
        {
            

        }

        protected override void OnLATKStarted(InputAction.CallbackContext context)
        {
            if (stateMachine.Player.beAttacking) 
                stateMachine.ChangeState(stateMachine.SpecialATKState);
            else
                base.OnLATKStarted(context);
        }
    }
}
