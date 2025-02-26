using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.U2D.ScriptablePacker;

namespace zzz
{
    public class PlayerIdlingState : PlayerGroundedState
    {
        public PlayerIdlingState(PlayerControllStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.ReusableData.MovementSpeedModifier = 0f;
            
            
            base.Enter();
            stateMachine.Player.Animator.SetFloat(AnimationID.SpeedID, 0f);

            ResetVelocity();
        }

        public override void Update()
        {
            base.Update();
            if (stateMachine.ReusableData.MovementInput == Vector2.zero) return;
            OnMove();
        }

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            stateMachine.Player.Input.PlayerActions.Dash.started += OnDashStart;
        }

        protected override void RemoveInputActionsCallbacks() 
        { 
            base.RemoveInputActionsCallbacks();
            stateMachine.Player.Input.PlayerActions.Dash.started -= OnDashStart;
        }

    }
}
