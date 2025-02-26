using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class EnemyATKChecking : MonoBehaviour
    {
        private Collider clo;
        private void OnTriggerEnter(Collider other)
        {
            clo = other;
            other.GetComponent<Player>().beAttacking = true;
            //Debug.Log(other.name);
        }

    }
}
