using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerQTEState : PlayerControllState
    {
        public PlayerQTEState(PlayerControllStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            FightingEventManager.MainInstance.QTE = true;
            
            Time.timeScale = 0.001f;
            FightingEventManager.MainInstance.ppController.SetSpeedAndMaxValue(0.4f, 0.4f);
            TimerManager.MainInstance.TryGetOneTimer(5f,TimeScaleBacktrack);
        }

        public override void Exit()
        {
            base.Exit();
            Time.timeScale = 1f;
        }

        protected override void AddInputActionsCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.Switch.started += OnSwitchStarted;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.Switch.started -= OnSwitchStarted;
        }

        protected override void OnSwitchStarted(InputAction.CallbackContext context)
        {
            base.OnSwitchStarted(context);
            FightingEventManager.MainInstance.QTE = false;
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();

            if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
                return;
            }
            stateMachine.ChangeState(stateMachine.SprintingState);
        }

        private void TimeScaleBacktrack()
        {
            Time.timeScale = 1f;
            FightingEventManager.MainInstance.QTE = false;
        }
    }
}
