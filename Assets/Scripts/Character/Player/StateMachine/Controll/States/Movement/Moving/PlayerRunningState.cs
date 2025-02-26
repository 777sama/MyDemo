using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerRunningState : PlayerMovingState
    {
        public PlayerRunningState(PlayerControllStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;

            base.Enter();

            stateMachine.ReusableData.Speed = movementData.RunData.Speed;
            
            
        }

        public override void Update()
        {
            base.Update();
            RotateTowardsTargetRotation();
            stateMachine.Player.Animator.SetFloat(AnimationID.SpeedID, stateMachine.ReusableData.Speed, 0.1f, Time.deltaTime);
            
        }

    }
}
