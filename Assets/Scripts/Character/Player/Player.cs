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

        public Collider collider;//���ڿ���ĳЩ��������ѵ��˶���

        public bool beAttacking;

        



        public SkinnedMeshHighlighter MeshHighlighter { get; private set; }
        


        [SerializeField, Header("��ɫ��ͨ����")] public PlayerComboSO baseCombo;
        public PlayerComboSO currentCombo;
        public int currentComboIndex;
        public int hitIndex;
        public float maxColdTime;
        public bool canAttackInput;

        [SerializeField, Header("���⹥��")] public PlayerComboSO specialCombo;
        public PlayerComboSO currentSpecialCombo;
        public int currentSpecialConmboIndex;


        //����ʱ��ⷽ��
        public Vector3 detectionDirection;
        [SerializeField, Header("�������")] public float detectionRange;
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

        public void BeAttack()//�ܻ����
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
                //��⵽����
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

        public void TriggerDamager()//�˺�����
        {
            if (currentEnemy == null) return;
            //if (Vector3.Dot(transform.forward, DevelopmentToos.DirectionForTarget(transform, currentEnemy)) < 2f) return;
            if (controllStateMachine.currentState == "PlayerSpecialATKState")
            {
                GameEventManager.MainInstance.CallEvent("�����˺�", spATK.Damage, spATK.ComboHitName[hitIndex], transform, currentEnemy);return;
            }
            //if (DevelopmentToos.DistanceForTarget(currentEnemy, transform) >  2f) return;


            //����: �˺�ֵ�����˶������������ߣ���ǰ��������
            GameEventManager.MainInstance.CallEvent("�����˺�", currentCombo.TryGetComboDamage(currentComboIndex), currentCombo.TryGetOneHitName(currentComboIndex, hitIndex), transform, currentEnemy);
            
    
        }

        //����Ŀ��,Ϊ��ֹһЩ��̹���һֱճ�ŵ��ˣ�����������animation�¼��д���
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
