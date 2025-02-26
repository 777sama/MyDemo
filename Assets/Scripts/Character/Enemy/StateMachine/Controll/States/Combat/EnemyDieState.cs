using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class EnemyDieState : EnemyControllState
    {
        public EnemyDieState(EnemyControllStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            stateMachine.Enemy.life = false;
            stateMachine.Enemy.animator.Play("Die");
            stateMachine.Enemy.collider.excludeLayers = LayerMask.NameToLayer("Player");
            EnemyManager.MainInstance.RemoveEnemyInRange(stateMachine.Enemy);
            EnemyManager.MainInstance.DestroyObject(stateMachine.Enemy.gameObject, 5f);
        }


        
    }
}
