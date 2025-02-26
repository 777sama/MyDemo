using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class PlayerControllStateMachine : StateMachine
    {
        public Player Player { get; }
        public string currentState;
        public PlayerStateReusableData ReusableData { get; set; }
        public PlayerIdlingState IdlingState {  get; private set; }
        public PlayerRunningState RunningState { get; private set; }
        public PlayerSprintingState SprintingState { get; private set; }
        public PlayerDashingState DashingState { get; private set; }
        public PlayerNormalATKState NormalATKState { get; private set; }
        public PlayerSpecialATKState NormalSkillState { get; private set; }
        public PlayerSpecialSkillState SpecialSkillState { get; private set; }
        public PlayerUltimateSkillState UltimateSkillState { get; private set; }
        public PlayerSwitchInState SwitchInState { get; private set; }
        public PlayerSwitchOutState SwitchOutState { get; private set; }
        public PlayerSpecialATKState SpecialATKState { get; private set; }
        public PlayerHitState HitState { get; private set; }
        public PlayerSwitchDefenseState DefenseState { get; private set; }
        public PlayerQTEState QTEState { get; private set; }
        public PlayerNotControllState NotControllState { get; private set; }

        public PlayerControllStateMachine(Player player)
        {
            Player = player;
            ReusableData = new PlayerStateReusableData();
            IdlingState = new PlayerIdlingState(this);
            RunningState = new PlayerRunningState(this);
            SprintingState = new PlayerSprintingState(this);
            DashingState = new PlayerDashingState(this);   
            NormalATKState = new PlayerNormalATKState(this);
            NormalSkillState = new PlayerSpecialATKState(this);
            SpecialSkillState = new PlayerSpecialSkillState(this);
            UltimateSkillState = new PlayerUltimateSkillState(this);
            SwitchInState = new PlayerSwitchInState(this);
            SwitchOutState = new PlayerSwitchOutState(this);
            SpecialATKState = new PlayerSpecialATKState(this);
            HitState = new PlayerHitState(this);
            DefenseState = new PlayerSwitchDefenseState(this);
            QTEState = new PlayerQTEState(this);
            NotControllState = new PlayerNotControllState(this);
        }
    }
}
