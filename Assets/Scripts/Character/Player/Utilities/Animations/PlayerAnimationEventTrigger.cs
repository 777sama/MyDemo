using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class PlayerAnimationEventTrigger : MonoBehaviour
    {
        private Player player;

        private void Awake()
        {
            player = GetComponent<Player>();
        }

        public void TriggerOnControllStateAnimationEnterEvent()
        {
            player.OnControllStateAnimationEnterEvent();
        }

        public void TriggerOnControllStateAnimationExitEvent()
        {
            player.OnControllStateAnimationExitEvent();
        }

        public void TriggerOnControllStateAnimationTransitionEvent()
        {
            player.OnControllStateAnimationTransitionEvent();
        }
    }
}
