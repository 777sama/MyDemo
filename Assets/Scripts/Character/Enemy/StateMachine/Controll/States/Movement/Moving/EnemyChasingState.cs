using GGG.Tool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace zzz
{
    public class EnemyChasingState : EnemyControllState
    {

        public EnemyChasingState(EnemyControllStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        private float distanceToStand = 4f;
        private float timer;
        private float horizontal;

        public override void Enter()
        {
            base.Enter();
            timer = UnityEngine.Random.Range(1, 3);
        }

        public override void Update()
        {
            base.Update();
            stateMachine.Enemy.transform.Look(stateMachine.Enemy.player.transform.position, 50f);
            stateMachine.Enemy.animator.SetFloat(AnimationID.VerticalID, 1f,0.1555f,Time.deltaTime);

            if (timer <= 0)
            {
                RandomDirection();
                timer = UnityEngine.Random.Range(1, 3);
            }
            
            stateMachine.Enemy.animator.SetFloat(AnimationID.HorizontalID, horizontal, 0.1555f, Time.deltaTime);

            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
        }

        protected override void ChangeState()
        {
            if (stateMachine.Enemy.player != null && distance <= StandRange - 0.04f && distance > ATKRange)
            {
                stateMachine.ChangeState(stateMachine.CirclingState);
            }
            else if (stateMachine.Enemy.player != null && distance < ATKRange)
            {
                stateMachine.ChangeState(stateMachine.RetreatingState);
            }
        }

        private void RandomDirection()
        {
            horizontal = UnityEngine.Random.Range(-1, 2);
        }

    }
}
