using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace zzz
{
    public class GamePoolManager : GGG.Tool.Singleton.Singleton<GamePoolManager>
    {
        [System.Serializable]
        private class PoolItem
        {
            public string itemName;
            public GameObject item;
            public int initMaxCount;
        }

        [SerializeField]private List<PoolItem> configPoolItem = new List<PoolItem>();
        private Dictionary<string,Queue<GameObject>> poolCenter = new Dictionary<string,Queue<GameObject>>();
        private GameObject poolItemParent;

        private void Start()
        {
            poolItemParent = new GameObject("对象池的父对象");
            poolItemParent.transform.SetParent(this.transform);
            InitPool();
        }

        private void InitPool()
        {
            if(configPoolItem.Count == 0) return;
             for(var i = 0; i < configPoolItem.Count; i++)
            {
                for(int j = 0; j< configPoolItem[i].initMaxCount; j++)
                {
                    var item = Instantiate(configPoolItem[i].item);
                    item.SetActive(false);
                    item.transform.SetParent(poolItemParent.transform);
                    
                    if(!poolCenter.ContainsKey(configPoolItem[i].itemName))//判断池子中是否存在这个对象的key
                    {
                        //如果没有一个叫ATKSound的池子，则创建一个
                        poolCenter.Add(configPoolItem[i].itemName,new Queue<GameObject>());
                        poolCenter[configPoolItem[i].itemName].Enqueue(item);
                    }
                    else
                    {
                        poolCenter[configPoolItem[i].itemName].Enqueue(item);
                    }
                }
            }
           
        }

        public void TryGetPoolItem(string name,Vector3 position,Quaternion rotation)
        {
            if (poolCenter.ContainsKey(name))//判断有没有叫name的池子存在
            {
                var item = poolCenter[name].Dequeue();
                item.transform.position = position;
                item.transform.rotation = rotation;
                item.SetActive(true);
                poolCenter[name].Enqueue(item);
            }
            else
            {
                DevelopmentToos.WTF("当前申请的池子不存在,申请的池子名：" + name);
            }
        }


        public GameObject TryGetPoolItem(string name)
        {
            if (poolCenter.ContainsKey(name))
            {
                //判断是否存在名叫name的池子
                var item = poolCenter[name].Dequeue();
                item.SetActive(true);
                poolCenter[name].Enqueue(item);
                return item;

            }
            DevelopmentToos.WTF($"名为{name}的池子不存在");
            return null;

        }

    }

    
}
