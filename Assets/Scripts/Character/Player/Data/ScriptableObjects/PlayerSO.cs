using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    [CreateAssetMenu(fileName="Player",menuName = "Custom/Character/Player")]
    public class PlayerSO : ScriptableObject
    {
        [field:SerializeField] public PlayerGroundedData GroundedData {  get; private set; }
    }
}
