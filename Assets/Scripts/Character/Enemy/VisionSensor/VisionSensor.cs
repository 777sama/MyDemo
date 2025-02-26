using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class VisionSensor : MonoBehaviour
    {
        [SerializeField] Enemy enemy;
        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                enemy.player = player;
                EnemyManager.MainInstance.AddEnemyInRange(enemy);
            }
                

        }

        private void OnTriggerExit(Collider other)
        {
            var player = other.GetComponent<Player>();
            if (player != null)
                enemy.player = null;
            EnemyManager.MainInstance.RemoveEnemyInRange(enemy);
        }
    }
}
