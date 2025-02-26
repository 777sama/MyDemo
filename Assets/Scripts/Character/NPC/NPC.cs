using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class NPC : MonoBehaviour
    {
        public TaskDataSO newTask;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
