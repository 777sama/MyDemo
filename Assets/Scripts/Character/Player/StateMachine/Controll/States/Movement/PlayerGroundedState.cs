using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerGroundedState : PlayerControllState
    {
        public PlayerGroundedState(PlayerControllStateMachine playerMovementStateMachine) :  base(playerMovementStateMachine)
        {
        }


        public override void Enter()
        {
            base.Enter();

            UpdateCameraRecenteringState(stateMachine.ReusableData.MovementInput);
        }


        protected virtual void OnMove()
        {
            stateMachine.ChangeState(stateMachine.RunningState);
        }






        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            stateMachine.Player.Input.PlayerActions.Movement.started += OnMovementStart;
            stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCancele;
            stateMachine.Player.Input.PlayerActions.Dash.started += OnDashStart;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            stateMachine.Player.Input.PlayerActions.Movement.started -= OnMovementStart;
            stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCancele;
            stateMachine.Player.Input.PlayerActions.Dash.started -= OnDashStart;
        }


        protected virtual void OnMovementCancele(InputAction.CallbackContext context)
        {
            stateMachine.Player.Animator.SetBool(AnimationID.HasInputID, false);
            
            stateMachine.ChangeState(stateMachine.IdlingState);
        }

        protected virtual void OnDashStart(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.DashingState);
        }

        private void OnMovementStart(InputAction.CallbackContext context)
        { 
            stateMachine.Player.Animator.SetBool(AnimationID.HasInputID, true);
        }
    }
}
