using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    [Serializable]
    public class PlayerRunData 
    {
        [field: SerializeField][field: Range(1f, 2f)] public float SpeedModifier { get; private set; } = 1f;
        [field: SerializeField][field: Range(0f, 1f)] public float Speed { get; private set; } = 0.6f;
    }
}
