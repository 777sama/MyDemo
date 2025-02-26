using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class AnimatorID : MonoBehaviour
    {
        public static readonly int MovementID = Animator.StringToHash("Movement");
        public static readonly int HasInputID = Animator.StringToHash("HasInput");

    }
}
