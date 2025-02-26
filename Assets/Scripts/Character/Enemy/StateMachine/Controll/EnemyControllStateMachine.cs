using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class EnemyControllStateMachine : StateMachine
    {
        public Enemy Enemy { get; }
        public EnemyIdlingState IdlingState {  get; private set; }
        public EnemyChasingState ChasingState { get; private set; }
        public EnemyCirclingState CirclingState { get; private set; }
        public EnemyRetreatingState RetreatingState { get; private set; }
        public EnemyAttackingState AttackingState { get; private set; }
        public EnemyHitState HitState { get; private set; }
        public EnemyDieState DieState { get; private set; }

        public EnemyControllStateMachine(Enemy enemy)
        {
            Enemy = enemy;
            IdlingState = new EnemyIdlingState(this);
            ChasingState = new EnemyChasingState(this);
            CirclingState = new EnemyCirclingState(this);
            RetreatingState = new EnemyRetreatingState(this);
            AttackingState = new EnemyAttackingState(this);
            HitState = new EnemyHitState(this);
            DieState = new EnemyDieState(this);
        }
    }
}
