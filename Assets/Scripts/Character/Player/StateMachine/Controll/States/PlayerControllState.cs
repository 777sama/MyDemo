using GGG.Tool;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerControllState : IState
    {
        protected PlayerControllStateMachine stateMachine;

        protected PlayerGroundedData movementData;

        

        

        public PlayerControllState(PlayerControllStateMachine playerMovementStateMachine)
        {
            stateMachine = playerMovementStateMachine;


            movementData = stateMachine.Player.Data.GroundedData;

            InitializeData();
        }


        private void InitializeData()
        {
            stateMachine.ReusableData.TimeToReachTargetRotation = movementData.BaseRotationData.TargetRotationReachTime;
        }

        public virtual void Enter()
        {
            Debug.Log("玩家State:" + GetType().Name);
            stateMachine.currentState = GetType().Name;
            
            AddInputActionsCallbacks();
        }

        public virtual void Exit()
        {
            RemoveInputActionsCallbacks();
        }

        public virtual void HandleInput()
        {
            ReadMovementInput();
        }

        public virtual void PhysicsUpdate()
        {
            
            Move();
        }

        public virtual void Update()
        {

            OnEndAttact();
            if (EnemyManager.MainInstance.enemies.Count <= 0)
            {
                stateMachine.ChangeState(stateMachine.NotControllState);
            }
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




        private void ReadMovementInput()
        {
            stateMachine.ReusableData.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
        }

        private void Move()
        {
            if(stateMachine.ReusableData.MovementInput == Vector2.zero || stateMachine.ReusableData.Speed == 0f)
            {
                return;
            }
            Vector3 movementDirection = GetMovementInputDirection();

            float targetRotationYAngle = Rotate(movementDirection);

            Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

            float movementSpeed = GetMovementSpeed();

            Vector3 currentPlayerHorizontalVeleocity = GetPlayerHorizontalVelocity();

            //stateMachine.Player.Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVeleocity, ForceMode.VelocityChange);
            
        }

        private float Rotate(Vector3 direction)
        {
            float directionAngle = UpdateTargetRotation(direction);
            RotateTowardsTargetRotation();
            return directionAngle;
        }

        private void UPdateTargetRotationData(float targetAngle)
        {
            stateMachine.ReusableData.CurrentTargetRotation.y = targetAngle;
            stateMachine.ReusableData.DampedTargetRotationPassedTime.y = 0f;
        }

        private float AddCameraRotationToAngle(float angle)
        {
            angle += stateMachine.Player.MainCameraTransform.eulerAngles.y;
            if (angle > 360f)
            {
                angle -= 360f;
            }

            return angle;
        }

       

        private static float GetDirectionAngle(Vector3 angle)
        {
            float directionAngle = Mathf.Atan2(angle.x, angle.z) * Mathf.Rad2Deg;
            if (directionAngle < 0)
            {
                directionAngle += 360f;
            }

            return directionAngle;
        }

        private Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = stateMachine.Player.Rigidbody.velocity;
            playerHorizontalVelocity.y = 0f;
            return playerHorizontalVelocity;
        }

        private float GetMovementSpeed()
        {
            return movementData.BaseSpeed * stateMachine.ReusableData.MovementSpeedModifier;
            //return stateMachine.ReusableData.Speed;
        }




        protected Vector3 GetMovementInputDirection()
        {
            return new Vector3(stateMachine.ReusableData.MovementInput.x, 0f, stateMachine.ReusableData.MovementInput.y);
        }
        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;
            if (currentYAngle == stateMachine.ReusableData.CurrentTargetRotation.y)
            {
                return;
            }
            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, stateMachine.ReusableData.CurrentTargetRotation.y, ref stateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y, stateMachine.ReusableData.TimeToReachTargetRotation.y - stateMachine.ReusableData.DampedTargetRotationPassedTime.y);
            stateMachine.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;
            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);
            stateMachine.Player.Rigidbody.MoveRotation(targetRotation);
        }

        protected float UpdateTargetRotation(Vector3 direction,bool shouldConsiderCameraRotation = true)
        {
            float directionAngle = GetDirectionAngle(direction);
            if (shouldConsiderCameraRotation)
            {
                directionAngle = AddCameraRotationToAngle(directionAngle);
            }
            if (directionAngle != stateMachine.ReusableData.CurrentTargetRotation.y)
            {
                UPdateTargetRotationData(directionAngle);
            }
            
            return directionAngle;
        }

        protected Vector3 GetTargetRotationDirection(float targetAngle)
        {
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        protected void ResetVelocity()
        {
            stateMachine.Player.Rigidbody.velocity = Vector3.zero;
        }


        protected void UpdateCameraRecenteringState(Vector2 movementInput)
        {
            if (movementInput == Vector2.zero)
            {
                return;
            }

            if (movementInput == Vector2.up)
            {
                DisableCameraRecentering();
                return;
            }

            float cameraVerticalAngle = stateMachine.Player.MainCameraTransform.eulerAngles.x;
            if (cameraVerticalAngle >= 270f)
            {
                cameraVerticalAngle -= 360f;
            }
            cameraVerticalAngle = Mathf.Abs(cameraVerticalAngle);

            if (movementInput == Vector2.down)
            {
                SetCameraRecenteringState(cameraVerticalAngle, movementData.BackwardsCameraRecenteringData);
                return;
            }

            SetCameraRecenteringState(cameraVerticalAngle, movementData.SidewaysCameraRecenteringData);
        }

        protected void SetCameraRecenteringState(float cameraVerticalAngle, List<PlayerCameraRecenteringData> cameraRecenteringData)
        {
            foreach (PlayerCameraRecenteringData recenteringData in cameraRecenteringData)
            {
                if (!recenteringData.IsWithinRange(cameraVerticalAngle))
                {
                    continue;
                }
                EnableCameraRecentering(recenteringData.WaitTime, recenteringData.RecenteringTime);
                return;
            }
            DisableCameraRecentering();
        }

        protected void EnableCameraRecentering(float waitTIme = -1f,float recentering = -1f)
        {
            float movementSpeed = GetMovementSpeed();
            if (movementSpeed == 0f)
            {
                movementSpeed = movementData.BaseSpeed;
            }
            stateMachine.Player.CameraUtility.EnableRecentering(waitTIme, recentering,movementData.BaseSpeed,movementSpeed);
        }

        protected void DisableCameraRecentering()
        {
            stateMachine.Player.CameraUtility.DisableRecentering();
        }


        #region 攻击控制
        
        private bool CanBaseAttackInput()
        {
            if(!stateMachine.Player.canAttackInput) return false;
            if(stateMachine.Player.Animator.AnimationAtTag("Hit")) return false;
            if(stateMachine.Player.Animator.AnimationAtTag("Parry")) return false;
            if(stateMachine.Player.Animator.AnimationAtTag("Ultimate")) return false;
            return true;
        }

        protected void CharacterBaseAttackInput(bool NormalOrSpecialATK)//true:普遍攻击,false:特殊攻击
        {
            if (!CanBaseAttackInput()) return;
            if (NormalOrSpecialATK)
            {
                
                if (stateMachine.Player.currentCombo == null || stateMachine.Player.currentCombo != stateMachine.Player.baseCombo)
                {
                    stateMachine.Player.currentCombo = stateMachine.Player.baseCombo;
                    ResetComboInfo();
                }
                ExecuteComboAction();
                stateMachine.ChangeState(stateMachine.NormalATKState);
            }
            else
            {
                
                if (stateMachine.Player.currentCombo == null || stateMachine.Player.currentCombo != stateMachine.Player.specialCombo)
                {
                    stateMachine.Player.currentCombo = stateMachine.Player.specialCombo;
                    ResetComboInfo();
                }
                ExecuteSpecialComboAction();
                stateMachine.ChangeState(stateMachine.SpecialSkillState);
            }
            
            

        }

        private void ExecuteComboAction()
        {
            stateMachine.Player.hitIndex = 0;
            if(stateMachine.Player.currentComboIndex == stateMachine.Player.currentCombo.TryGetComboMaxCount())
            {
                stateMachine.Player.currentComboIndex = 0;
            }
            stateMachine.Player.maxColdTime = stateMachine.Player.currentCombo.TryGetColdTime(stateMachine.Player.currentComboIndex);
            stateMachine.Player.Animator.CrossFadeInFixedTime(stateMachine.Player.currentCombo.TryGetOneComboAction(stateMachine.Player.currentComboIndex), 0.1555f, 0, 0f);
            TimerManager.MainInstance.TryGetOneTimer(stateMachine.Player.maxColdTime, UpdateComboInfo);
            
            stateMachine.Player.canAttackInput = false;
        }

        private void ExecuteSpecialComboAction()//播放特殊攻击指定下标的动画
        {
            stateMachine.Player.hitIndex = 0;
            if(stateMachine.Player.currentComboIndex == stateMachine.Player.currentCombo.TryGetComboMaxCount())
            {
                stateMachine.Player.currentComboIndex = 0;
            }
            stateMachine.Player.maxColdTime = stateMachine.Player.currentCombo.TryGetColdTime(stateMachine.Player.currentComboIndex);
            stateMachine.Player.Animator.CrossFadeInFixedTime(stateMachine.Player.currentCombo.TryGetOneComboAction(stateMachine.Player.currentComboIndex), 0.1555f, 0, 0f);
            TimerManager.MainInstance.TryGetOneTimer(stateMachine.Player.maxColdTime, UpdateComboInfo);

            stateMachine.Player.canAttackInput = false;
        }

        private void UpdateComboInfo()
        {
            stateMachine.Player.currentComboIndex++;
            stateMachine.Player.maxColdTime = 0f;
            stateMachine.Player.canAttackInput = true;
           
        }

        private void UpdateSpecialComboInfo()//根据能量判断要执行的特殊攻击的动画下标
        {
            stateMachine.Player.currentComboIndex++;
            stateMachine.Player.maxColdTime = 0f;
            stateMachine.Player.canAttackInput = true;
        }

        protected void ResetComboInfo()
        {
            stateMachine.Player.currentComboIndex = 0;
            stateMachine.Player.currentSpecialConmboIndex = 0;
            stateMachine.Player.maxColdTime = 0f;
            stateMachine.Player.hitIndex = 0;
        }

        private void OnEndAttact()//攻击结束后重置攻击索引，回到第一个攻击动作
        {
            if(stateMachine.Player.Animator.AnimationAtTag("motion") && stateMachine.Player.canAttackInput)
            {
                ResetComboInfo();
            }
        }

        //private void ATK()
        //{
        //    TriggerDamager();
        //    GamePoolManager.MainInstance.TryGetPoolItem("ATKSound",stateMachine.Player.transform.position,Quaternion.identity);
        //}

        //private void DetectionTarget()
        //{
        //    if(Physics.SphereCast(stateMachine.Player.transform.position + (stateMachine.Player.transform.up*0.7f),stateMachine.Player.detectionRange,stateMachine.Player.detectionDirection,out var hit, stateMachine.Player.detectionDistance, 1 << 8, QueryTriggerInteraction.Ignore))
        //    {
        //        //检测到敌人
        //        stateMachine.Player.currentEnemy = hit.collider.transform;
        //    }
        //}



        //private void UpdateDetectionDirection()
        //{
        //    stateMachine.Player.detectionDirection = (stateMachine.Player.MainCameraTransform.forward * stateMachine.ReusableData.MovementInput.y) + (stateMachine.Player.MainCameraTransform.right * stateMachine.ReusableData.MovementInput.x);
        //    stateMachine.Player.detectionDirection.Set(stateMachine.Player.detectionDirection.x, 0f, stateMachine.Player.detectionDirection.z);
        //    stateMachine.Player.detectionDirection = stateMachine.Player.detectionDirection.normalized;
        //}

        //public void TriggerDamager()//伤害触发
        //{
        //    if (stateMachine.Player.currentEnemy == null) return;
        //    if(Vector3.Dot(stateMachine.Player.transform.forward,DevelopmentToos.DirectionForTarget(stateMachine.Player.transform,stateMachine.Player.currentEnemy))<0.85f) return;
        //    if(DevelopmentToos.DistanceForTarget(stateMachine.Player.currentEnemy,stateMachine.Player.transform)>1.3f) return;


        //    //参数: 伤害值，受伤动画名，攻击者，当前被攻击者
        //    GameEventManager.MainInstance.CallEvent("触发伤害", stateMachine.Player.currentCombo.TryGetComboDamage(stateMachine.Player.currentComboIndex), stateMachine.Player.currentCombo.TryGetOneHitName(stateMachine.Player.currentComboIndex, stateMachine.Player.hitIndex), stateMachine.Player.transform, stateMachine.Player.currentEnemy);
        //}

        ////看着目标
        //private void LookTargetOnAttack()
        //{
        //    if(stateMachine.Player.Animator.AnimationAtTag("Attack") && stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
        //    {
        //        stateMachine.Player.transform.Look(stateMachine.Player.currentEnemy.position, 50f);
        //    }
        //}

        #endregion

        #region 受击

        protected virtual void BeAttack()
        {
            stateMachine.ChangeState(stateMachine.HitState);
        }

        #endregion

        protected virtual void AddInputActionsCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
            stateMachine.Player.Input.PlayerActions.Look.started += OnMouseMovementStarted;
            stateMachine.Player.Input.PlayerActions.Movement.performed += OnMovementPerformed;
            stateMachine.Player.Input.PlayerActions.LATK.started += OnLATKStarted;
            stateMachine.Player.Input.PlayerActions.Skill.started += OnSkillStarted;
            stateMachine.Player.Input.PlayerActions.Switch.started += OnSwitchStarted;
            stateMachine.Player.Input.PlayerActions.Ultimate.started += OnUltimateStarted;
        }


        protected virtual void RemoveInputActionsCallbacks()
        {

            stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;
            stateMachine.Player.Input.PlayerActions.Look.started -= OnMouseMovementStarted;
            stateMachine.Player.Input.PlayerActions.Movement.performed -= OnMovementPerformed;
            stateMachine.Player.Input.PlayerActions.LATK.started -= OnLATKStarted;
            stateMachine.Player.Input.PlayerActions.Skill.started -= OnSkillStarted;
            stateMachine.Player.Input.PlayerActions.Switch.started -= OnSwitchStarted;
            stateMachine.Player.Input.PlayerActions.Ultimate.started -= OnUltimateStarted;
        }





        private void OnMovementCanceled(InputAction.CallbackContext context)
        {
            DisableCameraRecentering();
        }

        private void OnMouseMovementStarted(InputAction.CallbackContext context)
        {
            UpdateCameraRecenteringState(stateMachine.ReusableData.MovementInput);
        }

        protected virtual void OnMovementPerformed(InputAction.CallbackContext context)
        {
            UpdateCameraRecenteringState(context.ReadValue<Vector2>());
        }

        protected virtual void OnLATKStarted(InputAction.CallbackContext context)
        {
            CharacterBaseAttackInput(true);
            
            
        }


        protected virtual void OnSkillStarted(InputAction.CallbackContext context)
        {
            CharacterBaseAttackInput(false);
           
        }

        protected virtual void OnSwitchStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.SwitchOutState);
            
            SwitchCharacter.MainInstance.ChangeCharacter();
        }

        private void OnUltimateStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.UltimateSkillState);
        }

    }
}
