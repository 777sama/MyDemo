using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace zzz
{
    public class Enemy : MonoBehaviour
    {
        private EnemyControllStateMachine controllStateMachine;

       
        public Player player;
        public Animator animator { get; private set; }

        public NavMeshAgent agent { get; private set; }

        public bool applyATK;

        public bool beAttacking;

        public Collider collider;

        [SerializeField, Header("µ–»Àπ•ª˜")] public EnemyATKSO ATK;
        public int currentATKIndex;
        public int hitIndex;
        public GameObject ATKChecking;

        [SerializeField, Header("µ–»À Ù–‘")] private EnemyPropertySO propertySO;
        public float HP;
        public bool life;

        private void Awake()
        {
            life = true;
            HP = propertySO.HP;
            applyATK = false;
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            collider = GetComponent<Collider>();
            
            controllStateMachine = new EnemyControllStateMachine(this);
        }

        private void Start()
        {
            
            controllStateMachine.ChangeState(controllStateMachine.IdlingState);
        }

        private void Update()
        {
            controllStateMachine.Update();
        }

        private void FixedUpdate()
        {
            controllStateMachine.PhysicsUpdate();   
        }
        public void OnControllStateAnimationEnterEvent()
        {
            controllStateMachine.OnAnimationEnterEvent();
        }

        public void OnControllStateAnimationExitEvent()
        {
            controllStateMachine.OnAnimationExitEvent();
        }

        public void OnControllStateAnimationTransitionEvent()
        {
            controllStateMachine.OnAnimationTransitionEvent();
        }


        private void EnemyATK()
        {
            TriggerDamager();
            UpdateHitIndex();
            GamePoolManager.MainInstance.TryGetPoolItem("ATKSound",transform.position,Quaternion.identity); 
        }

        private void UpdateHitIndex()
        {
            hitIndex++;
            if (hitIndex >= ATK.TryGetHitOrParryMaxCount(currentATKIndex)) hitIndex = 0;
        }

        public void TriggerDamager()
        {
            if (player == null) return;
            if (player.Animator.AnimationAtTag("Defense"))
            {
                GameEventManager.MainInstance.CallEvent("¥•∑¢…À∫¶", ATK.TryGetComboDamage(currentATKIndex), ATK.TryGetOneHitName(currentATKIndex, hitIndex), transform, player.transform);
                return;
            }
            if (DevelopmentToos.DistanceForTarget(player.transform, transform) > 2f) return;

            
            GameEventManager.MainInstance.CallEvent("¥•∑¢…À∫¶", ATK.TryGetComboDamage(currentATKIndex), ATK.TryGetOneHitName(currentATKIndex, hitIndex), transform, player.transform);
        }

        public void SetATKCheckingActive(int trueOrFalse)
        {
            if(trueOrFalse == 1)
            {
                ATKChecking.SetActive(true);
            }
            else
            {
                ATKChecking.SetActive(false);
            }
            
        }

        public void OnHit()
        {
            controllStateMachine.ChangeState(controllStateMachine.HitState);
        }
    }
}
