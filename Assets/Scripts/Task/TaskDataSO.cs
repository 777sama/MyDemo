using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public enum TaskState
    {
        Waiting,
        Working,
        Finished
    }
    
    [CreateAssetMenu(fileName = "Task_", menuName = "Task/New Task")]
    public class TaskDataSO : ScriptableObject
    {
        public string uid = System.Guid.NewGuid().ToString();
        public string name;
        [Multiline]public string description;
        public TaskState currentState;
    }
}
