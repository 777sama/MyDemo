using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using static zzz.UIManager;

namespace zzz
{
    public class GMCmd 
    {
        [MenuItem("CMCmd/��ȡ����")]
        public static void ReadTable()
        {
            PackageTable packageTable = Resources.Load<PackageTable>("TableData/PackageTable");
            foreach (PackageTableItem item in packageTable.DataList)
            {
                Debug.Log(string.Format("[id] : {0}, [name] : {1}", item.id, item.name));
            }
        }

        [MenuItem("CMCmd/����������������")]
        public static void CreateLocalPackageData()
        {
            //��������
            PackageLocalData.MainInstance.items = new List<PackageLocalItem>();
            for (int i = 1; i < 9; i++)
            {
                PackageLocalItem packageLovalItem = new()
                {
                    uid = Guid.NewGuid().ToString(),
                    id = i,
                    num = i,
                    level = i,
                    isNew = i % 2 == 1
                };
                PackageLocalData.MainInstance.items.Add(packageLovalItem);
            }
            PackageLocalData.MainInstance.SavePackage();


            
        }

        [MenuItem("CMCmd/��ȡ������������")]
        public static void ReadLocalRackageData()
        {
            //��ȡ����
            List<PackageLocalItem> readItems = PackageLocalData.MainInstance.LoadPackage();
            foreach (PackageLocalItem item in readItems)
            {
                Debug.Log(item.id+"  "+item.num);
            }
        }

        [MenuItem("CMCmd/�򿪱�������")]
        public static void OpenPackagePanel()
        {
            UIManager.MainInstance.OpenPanel(UIConst.PackagePanel);
        }

        [MenuItem("CMCmd/��ȡ��ʼ�����")]
        public static void ReadLocalTaskData()
        {
            List<TaskDataSO> tasks = TaskLocalData.MainInstance.LoadTask();
            Debug.Log(tasks.Count);

            
            foreach (var task in tasks)
            {
                Debug.Log(task);
                Debug.Log(task.uid);
                Debug.Log(task.name);
                Debug.Log(task.description);
                Debug.Log(task.currentState);
            }
        }

        [MenuItem("CMCmd/���������б�")]
        public static void ResetLocalTaskData()
        {
            List<TaskDataSO> tasks = TaskLocalData.MainInstance.LoadTask();
            
                
            
            TaskLocalData.MainInstance.tasks.Clear();
            TaskLocalData.MainInstance.SaveTask();
        }

    }
}
