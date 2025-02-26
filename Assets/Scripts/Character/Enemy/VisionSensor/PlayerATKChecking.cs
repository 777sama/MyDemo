using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class PlayerATKChecking : MonoBehaviour
    {
        private List<Enemy> enemies = new List<Enemy>();
        private void OnTriggerEnter(Collider other)
        {
            enemies.Add(other.GetComponent<Enemy>());
            other.GetComponent<Enemy>().beAttacking = true;
        }

        private void OnDisable()
        {
            if(enemies.Count == 0) return;

            foreach (Enemy enemy in enemies)
            {
                enemy.beAttacking = false;
            }

            enemies.Clear();
        }
    }
}
