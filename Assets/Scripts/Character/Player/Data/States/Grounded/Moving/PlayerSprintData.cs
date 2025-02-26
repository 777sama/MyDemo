using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    [Serializable]
    public class PlayerSprintData 
    {
        [field: SerializeField][field: Range(1f, 3f)] public float SpeedModifier { get; private set; } = 1.7f;
        [field: SerializeField][field: Range(1f, 3f)] public float Speed { get; private set; } = 1f;
    }
}
