using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class EnemyCirclingState : EnemyControllState
    {
        public EnemyCirclingState(EnemyControllStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        private float timer;
        private Vector2 timeRange = new Vector2(2, 4);
        private int horizontal;
        private int vertical;
        private bool needStop;

        public override void Enter()
        {
            base.Enter();
            timer = UnityEngine.Random.Range(timeRange.x, timeRange.y); 
            needStop = false;
            RandomMoveVelue();
            vertical = 0;
        }

        public override void Update()
        {
            base.Update();
            stateMachine.Enemy.transform.Look(stateMachine.Enemy.player.transform.position,50f);

            if(timer <= 0 && !needStop)
            {
                RandomMoveVelue();
                
                timer = UnityEngine.Random.Range(timeRange.x, timeRange.y);
            }else if(timer <= 0 && needStop)
            {
                ResetMoveVelue();

                timer = UnityEngine.Random.Range(timeRange.x, timeRange.y);
            }
            stateMachine.Enemy.animator.SetFloat(AnimationID.HorizontalID, horizontal, 0.1555f, Time.deltaTime);
            stateMachine.Enemy.animator.SetFloat(AnimationID.VerticalID, vertical, 0.1555f, Time.deltaTime);
            


            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
        }

        private void RandomMoveVelue()
        {
            
            horizontal = UnityEngine.Random.Range(-1, 2);
            needStop = !needStop;
        }
        private void ResetMoveVelue()
        {
            horizontal = 0;
            vertical = 0;
            needStop = !needStop;
        }

        protected override void ChangeState()
        {
            if (stateMachine.Enemy.player != null && distance > StandRange + 1f)
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
            }
            else if (stateMachine.Enemy.player != null && distance < ATKRange)
            {
                stateMachine.ChangeState(stateMachine.RetreatingState);
            }
        }

    }
}
