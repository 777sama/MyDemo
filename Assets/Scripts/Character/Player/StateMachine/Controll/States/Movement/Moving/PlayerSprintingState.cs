using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerSprintingState : PlayerMovingState
    {
        private PlayerSprintData sprintData;
        public PlayerSprintingState(PlayerControllStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            sprintData = movementData.SprintData;
        }

        public override void Enter()
        {
            stateMachine.ReusableData.MovementSpeedModifier = sprintData.SpeedModifier;

            base.Enter();

            stateMachine.ReusableData.Speed = sprintData.Speed;
           
        }

        public override void Update()
        {
            base.Update();
            stateMachine.Player.Animator.SetFloat(AnimationID.SpeedID, stateMachine.ReusableData.Speed, 0.1f, Time.deltaTime);
        }



        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            stateMachine.Player.Input.PlayerActions.Movement.performed += OnSprintPerformed;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            stateMachine.Player.Input.PlayerActions.Movement.performed -= OnSprintPerformed;
        }



        private void OnSprintPerformed(InputAction.CallbackContext context)
        {
            
        }

        protected override void OnDashStart(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.DashingState);
        }

    }
}
