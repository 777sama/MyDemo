using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace zzz
{
    public class FollowTargetText : MonoBehaviour
    {
        public NavMeshAgent agent;
        public Transform target;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            this.agent.SetDestination(this.target.position);
        }
    }
}
