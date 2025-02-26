using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class EnemyHitState : EnemyControllState
    {
        public EnemyHitState(EnemyControllStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }
}
