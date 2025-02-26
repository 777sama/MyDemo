using GGG.Tool.Singleton;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace zzz
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        public bool isAttacking = true;
        private int index = 0;
       
        [SerializeField]public List<Enemy> enemies = new List<Enemy>();

        public float notAttackingTimer = 5f;

        public void AddEnemyInRange(Enemy enemy)
        {
            if(!enemies.Contains(enemy))
            enemies.Add(enemy);
        }

        public void RemoveEnemyInRange(Enemy enemy) 
        {
            enemies.Remove(enemy); 
        }

        protected override void Awake()
        {
            base.Awake();
            
        }

        private void Update()
        {
            if (FightingEventManager.MainInstance.gameOver) return;

            if (enemies.Count <= 0 && !FightingEventManager.MainInstance.gameOver)
            {
                StartCoroutine(GameOver());
                return;
            }
            if (!ifAnyOneAttacking())
            {
                
                if(notAttackingTimer > 0)
                {
                    notAttackingTimer -= Time.deltaTime;    
                }
                if(notAttackingTimer <= 0)
                {
                    index++;
                    if (index >= enemies.Count)
                        index = 0;
                    enemies[index].applyATK = true;
                    
                    notAttackingTimer = 5f;
                    
                }
            }

           
        }

        public void DestroyObject(GameObject gameObject,float time = 0)
        {
            Destroy(gameObject, time);
        }



        IEnumerator GameOver()
        {
            FightingEventManager.MainInstance.gameOver = true;
            yield return new WaitForSecondsRealtime(3f);
            FightingEventManager.MainInstance.GameOver();
        }

        private bool ifAnyOneAttacking()
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.applyATK)
                { return true; }
            }
            return false;
        }
    }
}
