using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class PlayerMovingState : PlayerGroundedState
    {
        public PlayerMovingState(PlayerControllStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
    }
}
