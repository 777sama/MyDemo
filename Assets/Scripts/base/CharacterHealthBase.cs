using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public abstract class CharacterHealthBase : MonoBehaviour
    {
        //共同的受伤函数
        //共同的生命值信息

        protected Transform currentAttacker;//当前的攻击者

        protected Animator animator;

        

        private void Awake()
        {
            animator = GetComponent<Animator>();
            
        }

        protected virtual void Update()
        {
            OnHitLookAttacker();
        }

        private void OnEnable()
        {
            GameEventManager.MainInstance.AddEventListening<float,string,Transform,Transform>("触发伤害", OnCharacterHitEventHandler);
        }

        private void OnDisable()
        {
            GameEventManager.MainInstance.RemoveEvent<float, string, Transform, Transform>("触发伤害", OnCharacterHitEventHandler);
        }

        protected virtual void CharacterHitActiong(float damage,string hitName)
        {

        }


        protected virtual void TakeDamage(float damage)
        {
            
        }

        //设置当前的攻击者
        private void SetAttacker(Transform attacker)
        {
            if(attacker == null|| currentAttacker != attacker)
            {
                currentAttacker = attacker;
            }
            
        }
        
    

        private void OnHitLookAttacker()
        {
            if (currentAttacker == null) return;
            if (animator.AnimationAtTag("Hit") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
            transform.Look(currentAttacker.position, 50f);
        }



        //事件
        protected virtual void OnCharacterHitEventHandler(float damage, string hitName,  Transform attack, Transform self)
        {
            
         //if(self != transform) return;

            
            SetAttacker(attack);
            CharacterHitActiong(damage, hitName);
            //TakeDamage(damage);
        }
    }
}
