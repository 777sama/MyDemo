using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class PlayerNotControllState : PlayerControllState
    {
        public PlayerNotControllState(PlayerControllStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

        }

        public override void Update()
        {
            
        }

        public override void PhysicsUpdate()
        {
            
        }

        public override void HandleInput()
        {
            
        }

        protected override void AddInputActionsCallbacks()
        {
           
        }

        protected override void RemoveInputActionsCallbacks()
        {
            
        }
    }
}
