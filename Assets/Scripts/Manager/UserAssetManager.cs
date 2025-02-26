using GGG.Tool.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static zzz.UIManager;

namespace zzz
{
    public class UserAssetManager : Singleton<UserAssetManager>
    {
        private PackageTable packageTable;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            UIManager.MainInstance.OpenPanel(UIManager.UIConst.MainPanel);
        }


        public void DeletePackageItems(List<string> uids)
        {
            foreach (string uid in uids)
            {
                DeletePackageItem(uid, false);
            }
            PackageLocalData.MainInstance.SavePackage();
        }

        private void DeletePackageItem(string uid, bool needSave = true)
        {
            PackageLocalItem packageLocalItem = GetPackageLocalItemByUId(uid);
            if (packageLocalItem == null) 
            {
                return;
            }
            PackageLocalData.MainInstance.items.Remove(packageLocalItem);
            if(needSave)
            {
                PackageLocalData.MainInstance.SavePackage();

            }
        }

        public PackageTable GetPackageTable()
        {
            if (packageTable == null)
            {
                packageTable = Resources.Load<PackageTable>("TableData/PackageTable");
            }
            return packageTable;
        }

        public List<PackageTableItem> GetPackageTableByType(int type)
        {
            List<PackageTableItem> packageItems = new List<PackageTableItem>();
            foreach (PackageTableItem packageItem in GetPackageTable().DataList)
            {
                if(packageItem.type == type)
                {
                    packageItems.Add(packageItem);
                }
            }

            return packageItems;
        }

        public PackageLocalItem GetLotteryRandom1(int type)
        {
            List<PackageTableItem> packageItems = GetPackageTableByType(type);
            int index = UnityEngine.Random.Range(0, packageItems.Count);
            PackageTableItem packageItem = packageItems[index];
            PackageLocalItem packageLocalItem = new()
            {
                uid = System.Guid.NewGuid().ToString(),
                id = packageItem.id,
                num = 1,
                type = type,
                isNew = false,
            };
            PackageLocalData.MainInstance.items.Add(packageLocalItem);
            PackageLocalData.MainInstance.SavePackage();
            return packageLocalItem;
        }

        public List<PackageLocalItem> GetLotteryRandom10(int type ,bool sort = false)
        {
            //随机抽卡
            List<PackageLocalItem> packageLocalItems = new();
            for(int i = 0; i < 10; i++)
            {
                PackageLocalItem packageLocalItem = GetLotteryRandom1(type);
                packageLocalItems.Add(packageLocalItem);
            }
            //武器排序
            if (sort)
            {
                packageLocalItems.Sort(new PackageItemComparer());
            }
            return packageLocalItems;
        }

        public List<PackageLocalItem> GetPackageLocalData()
        {
            return PackageLocalData.MainInstance.LoadPackage();
        }

        public List<PackageLocalItem> GetSortPackageLocalData(int type)
        {
            List<PackageLocalItem> allLocalItems = PackageLocalData.MainInstance.LoadPackage();
            List<PackageLocalItem> localItems = new List<PackageLocalItem>();
            foreach (PackageLocalItem item in allLocalItems)
            {
                if (item.type == type)
                {                     
                    localItems.Add(item);
                }
            }
            localItems.Sort(new PackageItemComparer());
            return localItems;
        }



        public class PackageItemComparer : IComparer<PackageLocalItem>
        {
            public int Compare(PackageLocalItem a,PackageLocalItem b)
            {
                PackageTableItem x = UserAssetManager.MainInstance.GetPackageItemById(a.id);
                PackageTableItem y = UserAssetManager.MainInstance.GetPackageItemById(b.id);

                //首先按照Star从大到小排序
                int starComparison = y.star.CompareTo(x.star);

                //如果star相同,则从id从大到小排序
                if(starComparison == 0)
                {
                    int idComparison = y.id.CompareTo(x.id);
                    if (idComparison == 0)
                    {
                        return b.level.CompareTo(a.level);
                    }
                    return idComparison;
                }

                return starComparison;
            }
        }








        public PackageTableItem GetPackageItemById(int id)
        {
            List<PackageTableItem> packageDataList = GetPackageTable().DataList;
            foreach (PackageTableItem item in packageDataList)
            {
                if(item.id == id)
                {
                    return item;
                }
            }
            return null;
        }
        
        public PackageLocalItem GetPackageLocalItemByUId(string uid)
        {
            List<PackageLocalItem> packageDataList = GetPackageLocalData();
            foreach (PackageLocalItem item in packageDataList)
            {
                if(item.uid == uid)
                {
                    return item;
                }
            }
            return null;
        }

        public PackageLocalItem GetPackageLocalItemById(int id)
        {
            List<PackageLocalItem> packageDataList = GetPackageLocalData();
            foreach (PackageLocalItem item in packageDataList)
            {
                if (item.id == id)
                {
                    return item;
                }
            }
            return null;
        }
    }

    public class GameConst
    {
        //武器类型
        public const int PackageTypeWeapon = 1;
        //材料
        public const int PackageTypeMaterial = 2;
    }
}
