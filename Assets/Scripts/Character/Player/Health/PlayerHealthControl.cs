using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class PlayerHealthControl : CharacterHealthBase
    {
        protected override void CharacterHitActiong(float damage, string hitName)
        {
            if(animator.AnimationAtTag("Dash")) return;
            if (animator.AnimationAtTag("Defense"))
            {
                FightingEventManager.MainInstance.TriggerShakeCamera(1.3f);
                currentAttacker.GetComponent<Enemy>().animator.Play("FHit");
                GamePoolManager.MainInstance.TryGetPoolItem("HitVFX", transform.position + new Vector3(transform.forward.normalized.x,0.8f,transform.forward.normalized.z), Quaternion.identity);
                GamePoolManager.MainInstance.TryGetPoolItem("FootSound", transform.position, Quaternion.identity); return; 
            }

            animator.Play(hitName, 0, 0);
            
        }

        protected override void OnCharacterHitEventHandler(float damage, string hitName, Transform attack, Transform self)
        {

            if (self != transform)
            {
                return;
            }
            base.OnCharacterHitEventHandler(damage, hitName, attack, self);
        }
    }
}
