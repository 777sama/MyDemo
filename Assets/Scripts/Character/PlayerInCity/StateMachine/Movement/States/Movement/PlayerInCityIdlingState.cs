using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerInCityIdlingState : PlayerInCityMovementState
    {
        public PlayerInCityIdlingState(PlayerInCityMovementStateMachine playerInCitystateMachine) : base(playerInCitystateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
        }

        public override void Update()
        {
            base.Update();
            if(stateMachine.ReusableData.MovementInput == Vector2.zero) return;
            OnMove();
        }

        protected override void OnDashStarted(InputAction.CallbackContext context)
        {
            
        }
    }
}
