using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerDoNothingState : PlayerInCityMovementState
    {
        public PlayerDoNothingState(PlayerInCityMovementStateMachine playerInCitystateMachine) : base(playerInCitystateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.Player.CameraUtility.VirtualCamera.enabled = false;
            base.Enter();
        }
        public override void Update()
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
            }
        }

        public override void Exit()
        {
            
            base.Exit();
            stateMachine.Player.CameraUtility.VirtualCamera.enabled = true;
        }
        public override void PhysicsUpdate()
        {
            
        }

        protected override void AddInputActionSCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.ALT.started += OnALTStart;
        }

        protected override void RemoveInputActionSCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.ALT.started -= OnALTStart;
        }

        public override void HandleInput()
        {
            
        }

        protected override void OnALTStart(InputAction.CallbackContext context)
        {
            Cursor.lockState = CursorLockMode.Locked;

        }

    }
}
