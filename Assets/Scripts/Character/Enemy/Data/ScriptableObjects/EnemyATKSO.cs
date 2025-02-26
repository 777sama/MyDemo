using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    [CreateAssetMenu(fileName = "Combo", menuName = "Custom/Enemy/ATK")]
    public class EnemyATKSO : ScriptableObject
    {
        [SerializeField] private List<EnemyATKDataSO> allATKData = new List<EnemyATKDataSO>();

        //��������е�ĳһ����ʽ����
        public string TryGetOneComboAction(int index)
        {
            if (allATKData.Count == 0) return null;
            return allATKData[index].ATKName;
        }
        //��ȡ��Ӧ�����˶���
        public string TryGetOneHitName(int index, int hitIndex)
        {
            if (allATKData.Count == 0) return null;
            if (allATKData[index].GetHitAndParryNameMaxCount() == 0) return null;
            return allATKData[index].ATKHitName[hitIndex];
        }

        public float TryGetComboDamage(int index)
        {
            if (allATKData.Count == 0) return 0;
            return allATKData[index].Damage;
        }

        public int TryGetHitOrParryMaxCount(int index)
        {

            return allATKData[index].GetHitAndParryNameMaxCount();
        }
    }
}
