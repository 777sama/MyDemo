using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerInCityWalkingState : PlayerInCityMovementState
    {
        private PlayerWalkData walkData;
        public PlayerInCityWalkingState(PlayerInCityMovementStateMachine playerInCitystateMachine) : base(playerInCitystateMachine)
        {
            walkData = movementData.WalkData;
        }

        public override void Enter()
        {
            stateMachine.ReusableData.Speed = walkData.Speed;
            
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            stateMachine.Player.Animator.SetFloat(AnimationID.SpeedID, stateMachine.ReusableData.Speed, 0.1f, Time.deltaTime);
        }


        protected override void OnWalkToggleStart(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStart(context);

            stateMachine.ChangeState(stateMachine.RunningState);
        }
    }
}
