using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerInCityRunningState : PlayerInCityMovementState
    {
        private PlayerRunData runData;
        public PlayerInCityRunningState(PlayerInCityMovementStateMachine playerInCitystateMachine) : base(playerInCitystateMachine)
        {
            runData = movementData.RunData;
        }

        public override void Enter()
        {
            stateMachine.ReusableData.Speed = runData.Speed;
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            RotateTowardsTargetRotation();
            stateMachine.Player.Animator.SetFloat(AnimationID.SpeedID, stateMachine.ReusableData.Speed,0.1f,Time.deltaTime);
        }

        protected override void OnWalkToggleStart(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStart(context);
            stateMachine.Player.Animator.CrossFadeInFixedTime("RunToWalk", 0.1555f, 0, Time.deltaTime);
            stateMachine.ChangeState(stateMachine.WalkingState);
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {  
            base.OnMovementCanceled(context);
        }
    }
}
