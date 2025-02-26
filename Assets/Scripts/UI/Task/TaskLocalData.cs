using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool.Singleton;

namespace zzz
{
    public class TaskLocalData : SingletonNonMono<TaskLocalData>
    {
        public List<TaskDataSO> tasks;

        

        public void SaveTask()
        {
            string inventeroyJson = JsonUtility.ToJson(this);
            PlayerPrefs.SetString("TaskLocalData",inventeroyJson);
            PlayerPrefs.Save();
        }

        public List<TaskDataSO> LoadTask()
        {
            
            if (tasks != null)
            {
                
                return tasks;
            }
            if (PlayerPrefs.HasKey("TaskLocalData"))
            {
                
                string inventroyJson = PlayerPrefs.GetString("TaskLocalData");
                TaskLocalData taskLocalData = JsonUtility.FromJson<TaskLocalData>(inventroyJson);
                tasks = taskLocalData.tasks;
                
                return tasks;
            }
            else
            {
                tasks = new List<TaskDataSO>();
                return tasks;
            }
        }

        public void AddTask(TaskDataSO task)
        {
            tasks.Add(task);
            SaveTask();
        }

        public void RemoveTask(TaskDataSO task)
        {
            tasks.Remove(task);
            SaveTask();
        }

        public TaskDataSO GetTaskByUid(string uid)
        {
            foreach (TaskDataSO task in tasks)
            {
                if(task.uid == uid) return task;
            }
            return null;
        }

        public void ChangeTaskState(string uid,TaskState state)
        {
            GetTaskByUid(uid).currentState = state;
            SaveTask();
        }

    }
}
