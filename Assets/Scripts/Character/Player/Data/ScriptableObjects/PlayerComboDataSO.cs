using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    [CreateAssetMenu(fileName = "ComboData", menuName = "Custom/Character/ComboData")]
    public class PlayerComboDataSO : ScriptableObject
    {
        //��ʽ����(ĳ������Ƭ�ε�����)
        [SerializeField] private string comboName;
        [SerializeField] private string[] comboHitName;
        [SerializeField] private string[] comboParryName;
        [SerializeField] private float damage;
        [SerializeField] private float coldTime;
        [SerializeField] private float comboPositionOffset;
        [SerializeField] private Vector3 aTKRangePosition;
        [SerializeField] private Vector3 aTKRangeSize;

        public string ComboName => comboName;
        public string[] ComboHitName => comboHitName;
        public string[] ComboParryName => comboParryName;
        public float Damage => damage;
        public float ColdTime => coldTime;
        public float ComboPositionOffset => comboPositionOffset;
        public Vector3 ATKRangePosition => aTKRangePosition;
        public Vector3 ATKRangeSize => aTKRangeSize;

        //��ȡ��ǰ�������������� 
        public int GetHitAndParryNameMaxCount() => comboHitName.Length;
    }
}
