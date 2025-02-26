using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class EnemyAttackingState : EnemyControllState
    {
        public EnemyAttackingState(EnemyControllStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            if (SwitchCharacter.MainInstance.attacter == null)
                SwitchCharacter.MainInstance.attacter = stateMachine.Enemy.transform;
        }
        public override void Update()
        {
            base.Update();
            
            stateMachine.Enemy.transform.Look(stateMachine.Enemy.player.transform.position, 50f);
            if (stateMachine.Enemy.player != null && distance > ATKRange - 0.5f)
            {
                stateMachine.Enemy.animator.SetFloat(AnimationID.VerticalID, 1f, 0.1555f, Time.deltaTime);
            }
            else if (stateMachine.Enemy.player != null && distance <= ATKRange)
            {
                stateMachine.Enemy.animator.Play("Attack");
            }
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            
            stateMachine.ChangeState(stateMachine.IdlingState);
        }

        public override void Exit()
        {
            base.Exit();
            stateMachine.Enemy.applyATK = false;
            stateMachine.Enemy.SetATKCheckingActive(0);
            SwitchCharacter.MainInstance.attacter =null;
        }

        protected override void Attacking()
        {
           
        }
    }
}
