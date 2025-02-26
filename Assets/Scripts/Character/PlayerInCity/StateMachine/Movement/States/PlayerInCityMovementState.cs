using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerInCityMovementState : IState
    {
        protected PlayerInCityMovementStateMachine stateMachine;
        protected PlayerGroundedData movementData;


        public PlayerInCityMovementState(PlayerInCityMovementStateMachine playerInCitystateMachine)
        {
            stateMachine = playerInCitystateMachine;
            movementData = stateMachine.Player.Data.GroundedData;
            InitializeData();
        }

        private void InitializeData()
        {
            stateMachine.ReusableData.TimeToReachTargetRotation = movementData.BaseRotationData.TargetRotationReachTime;
        }

        public virtual void Enter()
        {
            Debug.Log("Íæ¼ÒState:" + GetType().Name);

            AddInputActionSCallbacks();
            UpdateCameraRecenteringState(stateMachine.ReusableData.MovementInput);
        }

        public virtual void Exit()
        {
            RemoveInputActionSCallbacks();
        }

        public virtual void HandleInput()
        {
            ReadMovementInput();
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
            Move();
        }

        public virtual void Update()
        {
            if(Cursor.lockState == CursorLockMode.None)
            {
                stateMachine.ChangeState(stateMachine.DoNothingState);
            }
        }


        private void ReadMovementInput()
        {
            stateMachine.ReusableData.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
        }

        private void Move()
        {
            if (stateMachine.ReusableData.MovementInput == Vector2.zero || stateMachine.ReusableData.Speed == 0f)
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

        protected virtual void OnMove()
        {
            if (stateMachine.ReusableData.ShouldWalk)
            {
                
                stateMachine.ChangeState(stateMachine.WalkingState);
                return;
            }
            stateMachine.Player.Animator.CrossFadeInFixedTime("RunStart", 0.1555f, 0, Time.deltaTime);
            stateMachine.ChangeState(stateMachine.RunningState);
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

        protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
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

        protected void EnableCameraRecentering(float waitTIme = -1f, float recentering = -1f)
        {
            float movementSpeed = GetMovementSpeed();
            if (movementSpeed == 0f)
            {
                movementSpeed = movementData.BaseSpeed;
            }
            stateMachine.Player.CameraUtility.EnableRecentering(waitTIme, recentering, movementData.BaseSpeed, movementSpeed);
        }

        protected void DisableCameraRecentering()
        {
            stateMachine.Player.CameraUtility.DisableRecentering();
        }

        protected virtual void AddInputActionSCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.WalkToggle.started += OnWalkToggleStart;
            stateMachine.Player.Input.PlayerActions.Movement.started += OnMovementStarted;
            stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
            stateMachine.Player.Input.PlayerActions.Look.started += OnMouseMovementStarted;
            stateMachine.Player.Input.PlayerActions.Movement.performed += OnMovementPerformed;
            stateMachine.Player.Input.PlayerActions.Dash.started += OnDashStarted;
            stateMachine.Player.Input.PlayerActions.ALT.started += OnALTStart;

        }

        protected virtual void RemoveInputActionSCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.WalkToggle.started -= OnWalkToggleStart;
            stateMachine.Player.Input.PlayerActions.Movement.started -= OnMovementStarted;
            stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;
            stateMachine.Player.Input.PlayerActions.Look.started -= OnMouseMovementStarted;
            stateMachine.Player.Input.PlayerActions.Movement.performed -= OnMovementPerformed;
            stateMachine.Player.Input.PlayerActions.ALT.started -= OnALTStart;
        }

        protected virtual void OnWalkToggleStart(InputAction.CallbackContext context)
        {
            stateMachine.ReusableData.ShouldWalk = !stateMachine.ReusableData.ShouldWalk;
        }

        protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
        {
            DisableCameraRecentering();
            stateMachine.Player.Animator.SetBool(AnimationID.HasInputID, false);
            stateMachine.ChangeState(stateMachine.IdlingState);
        }

        protected virtual void OnMouseMovementStarted(InputAction.CallbackContext context)
        {
            UpdateCameraRecenteringState(stateMachine.ReusableData.MovementInput);
        }

        protected virtual void OnMovementPerformed(InputAction.CallbackContext context)
        {
            UpdateCameraRecenteringState(context.ReadValue<Vector2>());
        }

        protected virtual void OnDashStarted(InputAction.CallbackContext context)
        {
           
        }

        protected virtual void OnMovementStarted(InputAction.CallbackContext context)
        {
            stateMachine.Player.Animator.SetBool(AnimationID.HasInputID, true);
        }

        protected virtual void OnALTStart(InputAction.CallbackContext context)
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
