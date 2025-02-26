using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class EnemyIdlingState : EnemyControllState
    {
        

        public EnemyIdlingState(EnemyControllStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        private float maxDistance = 4f;


        public override void Update()
        {
            base.Update();
            
        }

        protected override void ChangeState()
        {
            if (stateMachine.Enemy.player != null && distance > StandRange)
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
            }
            else if (stateMachine.Enemy.player != null && distance <= StandRange - 0.04f && distance > ATKRange)
            {
                stateMachine.ChangeState(stateMachine.CirclingState);
            }
            else if (stateMachine.Enemy.player != null && distance < ATKRange)
            {
                stateMachine.ChangeState(stateMachine.RetreatingState);
            }
        }
    }
}
