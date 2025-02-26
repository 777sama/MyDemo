using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class PlayerInCityMovementStateMachine : StateMachine
    {
        public PlayerInCity Player { get; }
        public PlayerStateReusableData ReusableData { get; set; }
        public PlayerInCityIdlingState IdlingState { get; private set; }
        public PlayerInCityWalkingState WalkingState { get; private set; }
        public PlayerInCityRunningState RunningState { get; private set; }
        public PlayerInCitySprintingState SprintingState { get; private set; }

        public PlayerDoNothingState DoNothingState { get; private set; }

        public PlayerInCityMovementStateMachine(PlayerInCity player)
        {
            Player = player;
            ReusableData = new PlayerStateReusableData();
            IdlingState = new PlayerInCityIdlingState(this);
            RunningState = new PlayerInCityRunningState(this);
            WalkingState = new PlayerInCityWalkingState(this);
            SprintingState = new PlayerInCitySprintingState(this);
            DoNothingState = new PlayerDoNothingState(this);
        }

    }
}
