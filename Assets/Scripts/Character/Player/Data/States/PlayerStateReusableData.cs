using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class PlayerStateReusableData 
    {
        public Vector2 MovementInput {  get; set; }
        public float MovementSpeedModifier { get; set; } = 1f;
        public bool ShouldWalk {  get; set; }

        public float Speed {  get; set; }

        public Vector3 currentTargetRotation;
        public Vector3 timeToReachTargetRotation;
        public Vector3 dampedTargetRotationCurrentVelocity;
        public Vector3 dampedTargetRotationPassedTime;

        public ref Vector3 CurrentTargetRotation
        {
            get
            {
                return ref currentTargetRotation;
            }
        }
        public ref Vector3 TimeToReachTargetRotation
        {
            get
            {
                return ref timeToReachTargetRotation;
            }
        }
        public ref Vector3 DampedTargetRotationCurrentVelocity
        {
            get
            {
                return ref dampedTargetRotationCurrentVelocity;
            }
        }
        public ref Vector3 DampedTargetRotationPassedTime
        {
            get
            {
                return ref dampedTargetRotationPassedTime;
            }
        }
    }
}
