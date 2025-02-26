using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    [CreateAssetMenu(fileName = "Property_", menuName = "Custom/Enemy/PropertySO")]
    public class EnemyPropertySO : ScriptableObject
    {
        [SerializeField] private float hp;

        public float HP => hp;
    }
}
