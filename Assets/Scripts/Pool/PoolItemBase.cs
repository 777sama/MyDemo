using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public interface IPoolItem
    {
        void Spawn();
        void Recycl();
    }
    public abstract class PoolItemBase : MonoBehaviour, IPoolItem
    {
        private void OnEnable()
        {
            Spawn();
        }

        private void OnDisable()
        {
           Recycl();
        }

        public virtual void Spawn()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Recycl()
        {
            throw new System.NotImplementedException();
        }
    }
}
