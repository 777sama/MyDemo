using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG;
using GGG.Tool.Singleton;
using System;
using JetBrains.Annotations;

namespace zzz
{
    public class PackageLocalData : SingletonNonMono<PackageLocalData>
    {
        public List<PackageLocalItem> items;
        public List<PackageLocalItem> materialItem = new List<PackageLocalItem>();  



        public void SavePackage()
        {
            string inventroyJson = JsonUtility.ToJson(this);
            PlayerPrefs.SetString("PackageLocalData",inventroyJson);
            PlayerPrefs.Save();
        }

        public List<PackageLocalItem> LoadPackage()
        {
            if (items != null)
            {
                SearchSameMaterialItem();
                MaterialItemMerge();
                return items;
            }
            if (PlayerPrefs.HasKey("PackageLocalData"))
            {
                string inventroyJson = PlayerPrefs.GetString("PackageLocalData");
                PackageLocalData packageLocalData = JsonUtility.FromJson<PackageLocalData>(inventroyJson);
                items = packageLocalData.items;

                SearchSameMaterialItem();
                MaterialItemMerge();
                return items;
            }
            else
            {
                items = new List<PackageLocalItem>();
                return items;
            }
        }

        public void MaterialItemMerge()
        {
            RemoveMaterialItem();
            foreach (PackageLocalItem item in materialItem)
            {
                items.Add(item);
            }
        }

        public void SearchSameMaterialItem()
        {
            materialItem.Clear();
            foreach(PackageLocalItem item in items)
            {
                if (item.type == GameConst.PackageTypeMaterial)
                {
                    if(!Shifoucunzai(item))
                    {
                        materialItem.Add(item); 
                    } 
                }
            }
          
        }

        public bool Shifoucunzai(PackageLocalItem sameitem)
        {

            foreach (PackageLocalItem item2 in materialItem)
            {
                if (sameitem.id == item2.id)
                {
                    item2.num += sameitem.num;
                    return true;
                }
            }

            return false;
        }

        public void RemoveMaterialItem()
        {
            foreach(var item in items)
            {
                if(item.type == GameConst.PackageTypeMaterial)
                {
                    items.Remove(item);
                    RemoveMaterialItem();
                    return;
                }
            }
        }

        public void MaterialNumChange(int id,int num)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].id == id)
                {
                    items[i].num += num;
                }
            }
            SavePackage();
        }

    }

    [Serializable]
    public class PackageLocalItem
    {
        public string uid;
        public int id;
        public int type;
        public int num;
        public int level;
        public bool isNew;
    }
}
