using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class EnemyRetreatingState : EnemyControllState
    {
        public EnemyRetreatingState(EnemyControllStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        
        private float horizontal;


        public override void Enter()
        {
            base.Enter();
            RandomDirection();
        }

        public override void Update()
        {
            base.Update();
            stateMachine.Enemy.transform.Look(stateMachine.Enemy.player.transform.position, 50f);

            stateMachine.Enemy.animator.SetFloat(AnimationID.VerticalID,-1f,0.1555f,Time.deltaTime);
            stateMachine.Enemy.animator.SetFloat(AnimationID.HorizontalID,horizontal,0.1555f,Time.deltaTime);

        }

        protected override void ChangeState()
        {
            if (stateMachine.Enemy.player != null&& distance > ATKRange+1f)
            {
                stateMachine.ChangeState(stateMachine.CirclingState);
            }
        }

        private void RandomDirection()
        {
            horizontal = UnityEngine.Random.Range(-1, 2);
        }
    }
}
