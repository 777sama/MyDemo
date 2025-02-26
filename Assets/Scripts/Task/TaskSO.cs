using GGG.Tool.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{


    [CreateAssetMenu(fileName ="TaskMenu",menuName = "Task/TaskMenu")]
    public class TaskSO : ScriptableObject
    {
        public List<TaskDataSO> tasks;


       
    }

}
