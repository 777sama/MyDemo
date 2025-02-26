using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerSwitchOutState : PlayerControllState
    {
        public PlayerSwitchOutState(PlayerControllStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateMachine.Player.Animator.CrossFadeInFixedTime("SwitchOut", 0.15f);
        }

        protected override void OnMovementPerformed(InputAction.CallbackContext context)
        {
            
        }
    }
}
