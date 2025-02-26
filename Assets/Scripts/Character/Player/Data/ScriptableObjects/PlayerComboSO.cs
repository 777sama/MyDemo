using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    [CreateAssetMenu(fileName = "Combo", menuName = "Custom/Character/Combo")]
    public class PlayerComboSO : ScriptableObject
    {
        [SerializeField] private List<PlayerComboDataSO> allComboData = new List<PlayerComboDataSO>();



        //获得连招中的某一个招式动作
        public string TryGetOneComboAction(int index)
        {
            if(allComboData.Count == 0) return null;
            return allComboData[index].ComboName;
        }
        //获取对应的受伤动画
        public string TryGetOneHitName(int index,int hitIndex)
        {
            if (allComboData.Count == 0) return null;
            if (allComboData[index].GetHitAndParryNameMaxCount() == 0) return null;
            return allComboData[index].ComboHitName[hitIndex];
        }
        //获取对应的防御动画
        public string TryGetOneParrryName(int index, int parryIndex)
        {
            if (allComboData.Count == 0) return null;
            if (allComboData[index].GetHitAndParryNameMaxCount() == 0) return null;
            return allComboData[index].ComboParryName[parryIndex];
        }

        //获得对应攻击动画的攻击范围
        public Vector3 TryGetComboATKRangePosition(int index)
        {
            if (allComboData.Count == 0) return Vector3.zero;
            return allComboData[index].ATKRangePosition;
        }
        public Vector3 TryGetComboATKRangeSize(int index)
        {
            if (allComboData.Count == 0) return Vector3.zero;
            return allComboData[index].ATKRangeSize;
        }

        public float TryGetComboDamage(int index)
        {
            if (allComboData.Count == 0) return 0;
            
            return allComboData[index].Damage;
        }

        public float TryGetColdTime (int index)
        {
            if (allComboData.Count == 0) return 0;
            return allComboData[index].ColdTime;
        }

        public float TryGetComboPositionOffset(int index)
        {
            if (allComboData.Count == 0) return 0;
            return allComboData[index].ComboPositionOffset;
        }

        public int TryGetHitOrParryMaxCount(int index)
        {
            
            return allComboData[index].GetHitAndParryNameMaxCount();
        }
        public int TryGetComboMaxCount()=>allComboData.Count;
    }
}
    