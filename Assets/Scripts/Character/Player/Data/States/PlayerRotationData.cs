using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    [Serializable]
    public class PlayerRotationData 
    {
        [field:SerializeField] public Vector3 TargetRotationReachTime { get;private set; }
    }
}
