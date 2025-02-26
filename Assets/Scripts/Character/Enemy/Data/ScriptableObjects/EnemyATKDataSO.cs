using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    [CreateAssetMenu(fileName = "ComboData", menuName = "Custom/Enemy/ATKData")]
    public class EnemyATKDataSO : ScriptableObject
    {
        [SerializeField] private string atkName;
        [SerializeField] private string[] atkHitName;
        [SerializeField] private float damage;

        public string ATKName => atkName;
        public string[] ATKHitName => atkHitName;
        public float Damage => damage;



        public int GetHitAndParryNameMaxCount() => atkHitName.Length;
    }
}
