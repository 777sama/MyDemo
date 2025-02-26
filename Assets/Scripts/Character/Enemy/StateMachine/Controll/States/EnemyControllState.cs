using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class EnemyControllState : IState
    {
        protected EnemyControllStateMachine stateMachine;

        protected float ATKRange;
        protected float StandRange;
        protected float distance;

        public EnemyControllState(EnemyControllStateMachine enemyStateMachine)
        {
            stateMachine = enemyStateMachine;
        }


        public virtual void Enter()
        {
            
            Debug.Log("µ–»ÀState:" + GetType().Name);
            
            ATKRange = 2f;
            StandRange = UnityEngine.Random.Range(4f, 6f);
        }

        public virtual void Exit()
        {
            
        }

        public virtual void HandleInput()
        {
            
        }

        public virtual void OnAnimationEnterEvent()
        {
            
        }

        public virtual void OnAnimationExitEvent()
        {
           
        }

        public virtual void OnAnimationTransitionEvent()
        {
           
        }

        public virtual void PhysicsUpdate()
        {
            
        }

        public virtual void Update()
        {
            if(stateMachine.Enemy.HP <= 0 && stateMachine.Enemy.life)
            {
                stateMachine.ChangeState(stateMachine.DieState);
            }
            distance = Vector3.Distance(stateMachine.Enemy.player.transform.position, stateMachine.Enemy.transform.position);
            ChangeState();
            Attacking();

        }

        protected virtual void ChangeState()
        {
           
        }
        protected virtual void Attacking()
        {
            if (stateMachine.Enemy.applyATK)
            {
                stateMachine.ChangeState(stateMachine.AttackingState);
            }
          
        }
        
    }
}
