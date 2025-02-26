using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerCombatControl : MonoBehaviour
    {
        private PlayerInput input;
        private Transform cameraGameObject;

        //检测方向
        private Vector3 detectionDirection;
        [SerializeField, Header("攻击检测")] private float detectionRang;

        private void Awake()
        {
            if(Camera.main != null) cameraGameObject = Camera.main.transform;
        }

        //动画事件触发的攻击事件
        private void ATK()
        {

        }

        private void DetectionTarget()
        {

        }

        private void UpdateDetectionDirection()
        {
            
        }
    }
}
