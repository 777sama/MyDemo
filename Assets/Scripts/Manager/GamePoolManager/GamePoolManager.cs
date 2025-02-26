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
            poolItemParent = new GameObject("����صĸ�����");
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
                    
                    if(!poolCenter.ContainsKey(configPoolItem[i].itemName))//�жϳ������Ƿ������������key
                    {
                        //���û��һ����ATKSound�ĳ��ӣ��򴴽�һ��
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
            if (poolCenter.ContainsKey(name))//�ж���û�н�name�ĳ��Ӵ���
            {
                var item = poolCenter[name].Dequeue();
                item.transform.position = position;
                item.transform.rotation = rotation;
                item.SetActive(true);
                poolCenter[name].Enqueue(item);
            }
            else
            {
                DevelopmentToos.WTF("��ǰ����ĳ��Ӳ�����,����ĳ�������" + name);
            }
        }


        public GameObject TryGetPoolItem(string name)
        {
            if (poolCenter.ContainsKey(name))
            {
                //�ж��Ƿ��������name�ĳ���
                var item = poolCenter[name].Dequeue();
                item.SetActive(true);
                poolCenter[name].Enqueue(item);
                return item;

            }
            DevelopmentToos.WTF($"��Ϊ{name}�ĳ��Ӳ�����");
            return null;

        }

    }

    
}
