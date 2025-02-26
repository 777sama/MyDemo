using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class EnemyHealthControl : CharacterHealthBase
    {


        protected override void CharacterHitActiong(float damage, string hitName)
        {
            if ((!animator.AnimationAtTag("EMATK") || currentAttacker.GetComponent<Player>().Animator.AnimationAtTag("spATK")) && transform.GetComponent<Enemy>().beAttacking && transform.GetComponent<Enemy>().HP>0)
            {
                
                FightingEventManager.MainInstance.TriggerShakeCamera(0.3f);
                animator.Play(hitName, 0, 0);
                TakeDamage(damage);
            }
                
            //GamePoolManager.MainInstance.TryGetPoolItem("HitSound", transform.position, Quaternion.identity);
        }

        protected override void OnCharacterHitEventHandler(float damage, string hitName, Transform attack, Transform self)
        {


            if (attack.tag == "Player")
            {
                base.OnCharacterHitEventHandler(damage, hitName, attack, self);
            }
             

        }

        protected override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            transform.GetComponent<Enemy>().HP -= damage;
            Debug.Log(transform.GetComponent<Enemy>().HP);
        }
    }
}
