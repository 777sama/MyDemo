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

        //��ⷽ��
        private Vector3 detectionDirection;
        [SerializeField, Header("�������")] private float detectionRang;

        private void Awake()
        {
            if(Camera.main != null) cameraGameObject = Camera.main.transform;
        }

        //�����¼������Ĺ����¼�
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
