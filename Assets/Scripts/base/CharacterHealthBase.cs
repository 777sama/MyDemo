using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public abstract class CharacterHealthBase : MonoBehaviour
    {
        //��ͬ�����˺���
        //��ͬ������ֵ��Ϣ

        protected Transform currentAttacker;//��ǰ�Ĺ�����

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
            GameEventManager.MainInstance.AddEventListening<float,string,Transform,Transform>("�����˺�", OnCharacterHitEventHandler);
        }

        private void OnDisable()
        {
            GameEventManager.MainInstance.RemoveEvent<float, string, Transform, Transform>("�����˺�", OnCharacterHitEventHandler);
        }

        protected virtual void CharacterHitActiong(float damage,string hitName)
        {

        }


        protected virtual void TakeDamage(float damage)
        {
            
        }

        //���õ�ǰ�Ĺ�����
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



        //�¼�
        protected virtual void OnCharacterHitEventHandler(float damage, string hitName,  Transform attack, Transform self)
        {
            
         //if(self != transform) return;

            
            SetAttacker(attack);
            CharacterHitActiong(damage, hitName);
            //TakeDamage(damage);
        }
    }
}
