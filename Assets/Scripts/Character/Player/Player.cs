using Cinemachine;
using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        [field: Header("References")]
        [field: SerializeField] public PlayerSO Data { get; private set; }


        [field: Header("Cameras")]
        [field: SerializeField] public PlayerCameraUtility CameraUtility { get; private set; }
        [SerializeField] public GameObject ExCameraTimeLine;
        

        public Rigidbody Rigidbody { get; private set; }

        public Animator Animator { get; private set; }

        public Transform MainCameraTransform { get; private set; }
        public PlayerInput Input { get; private set; }

        public  PlayerControllStateMachine controllStateMachine;

        public Collider collider;//用于控制某些攻击不会把敌人顶走

        public bool beAttacking;

        



        public SkinnedMeshHighlighter MeshHighlighter { get; private set; }
        


        [SerializeField, Header("角色普通攻击")] public PlayerComboSO baseCombo;
        public PlayerComboSO currentCombo;
        public int currentComboIndex;
        public int hitIndex;
        public float maxColdTime;
        public bool canAttackInput;

        [SerializeField, Header("特殊攻击")] public PlayerComboSO specialCombo;
        public PlayerComboSO currentSpecialCombo;
        public int currentSpecialConmboIndex;


        //攻击时检测方向
        public Vector3 detectionDirection;
        [SerializeField, Header("攻击检测")] public float detectionRange;
        [SerializeField] public float detectionDistance;
        [SerializeField] public BoxCollider ATKRange;
        [SerializeField] public PlayerComboDataSO spATK;
        [SerializeField] public PlayerComboDataSO ultimateSKill;

        public Transform currentEnemy;


        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Input = GetComponent<PlayerInput>();
            CameraUtility.Initialize();
            MainCameraTransform = Camera.main.transform;
            Animator = GetComponent<Animator>();
            collider = GetComponent<Collider>();
            MeshHighlighter = GetComponent<SkinnedMeshHighlighter>();
            controllStateMachine = new PlayerControllStateMachine(this);

        }

        private void Start()
        {
            canAttackInput = true;
            currentCombo = baseCombo;

            if(!SwitchCharacter.MainInstance.isSwitched)
                controllStateMachine.ChangeState(controllStateMachine.IdlingState);
            
            
        }

        private void Update()
        {
            controllStateMachine.HandleInput();
            controllStateMachine.Update();
            UpdateDetectionDirection();
            //LookTargetOnAttack();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position + (transform.up * 0.7f) + (detectionDirection * detectionDistance), detectionRange);
        }


        private void FixedUpdate()
        {
            controllStateMachine.PhysicsUpdate();
            DetectionTarget();
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

        public void SetSwitchedState()
        {
            if(SwitchCharacter.MainInstance.lastPlayer.beAttacking)
            {
                SwitchCharacter.MainInstance.lastPlayer.beAttacking = false;
                controllStateMachine.ChangeState(controllStateMachine.DefenseState);
            }else if (FightingEventManager.MainInstance.QTE)
            {
                FightingEventManager.MainInstance.QTEEvent();
                controllStateMachine.ChangeState(controllStateMachine.SpecialATKState);
            }
            else
            {
                controllStateMachine.ChangeState(controllStateMachine.SwitchInState);
            }
            
        }

        public void BeAttack()//受击检测
        {
            controllStateMachine.ChangeState(controllStateMachine.HitState);
        }

        private void ATK(int checkingQTE)
        {
            
            TriggerDamager();
            UpdateHitIndex();
            GamePoolManager.MainInstance.TryGetPoolItem("ATKSound", transform.position, Quaternion.identity);
            if(checkingQTE == 1)
            {
                controllStateMachine.ChangeState(controllStateMachine.QTEState);
            }
        }

        private void DetectionTarget()
        {
            if (Physics.SphereCast(transform.position + (transform.up * 0.7f), detectionRange, detectionDirection, out var hit, detectionDistance, 1 << 8, QueryTriggerInteraction.Ignore))
            {
                //检测到敌人
                currentEnemy = hit.collider.transform;
            }
           
        }


        private void UpdateHitIndex()
        {
            //hitIndex++;

            //if(hitIndex >= currentCombo.TryGetHitOrParryMaxCount(currentComboIndex)) 
             hitIndex = 0;
        }


        private void UpdateDetectionDirection()
        {
            detectionDirection = (MainCameraTransform.forward * controllStateMachine.ReusableData.MovementInput.y) + (controllStateMachine.Player.MainCameraTransform.right * controllStateMachine.ReusableData.MovementInput.x);
            detectionDirection.Set(detectionDirection.x, 0f, detectionDirection.z);
            detectionDirection = detectionDirection.normalized;
        }

        public void TriggerDamager()//伤害触发
        {
            if (currentEnemy == null) return;
            //if (Vector3.Dot(transform.forward, DevelopmentToos.DirectionForTarget(transform, currentEnemy)) < 2f) return;
            if (controllStateMachine.currentState == "PlayerSpecialATKState")
            {
                GameEventManager.MainInstance.CallEvent("触发伤害", spATK.Damage, spATK.ComboHitName[hitIndex], transform, currentEnemy);return;
            }
            //if (DevelopmentToos.DistanceForTarget(currentEnemy, transform) >  2f) return;


            //参数: 伤害值，受伤动画名，攻击者，当前被攻击者
            GameEventManager.MainInstance.CallEvent("触发伤害", currentCombo.TryGetComboDamage(currentComboIndex), currentCombo.TryGetOneHitName(currentComboIndex, hitIndex), transform, currentEnemy);
            
    
        }

        //看着目标,为防止一些冲刺攻击一直粘着敌人，将函数放在animation事件中触发
        public void LookTargetOnAttack()
        {
            if(currentEnemy == null) return;
            //if(DevelopmentToos.DistanceForTarget(currentEnemy,transform) >5f) return;   
                
                transform.Look(currentEnemy.position,10000f);
            

        }

        private void OnDisableCollider()
        {
           Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("Enemy"),true);
        }
        private void OnEnableCollider()
        {
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        }
    }
}
