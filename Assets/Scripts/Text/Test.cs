using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class Test : MonoBehaviour
    {
        public PlayerInput input;
        
        public SequenceEventExecutor executor;
        public SequenceEventExecutor executor2;
        public SequenceEventExecutor executorTaskWorking;
        public SequenceEventExecutor executorTaskFinished;
        private bool canInteract;

        public TaskDataSO task;

        public TaskDataSO task0;

        private void OnTriggerEnter(Collider other)
        {
            canInteract = true;
        }
        private void OnTriggerExit(Collider other)
        {
            canInteract = false;
        }

        private void Start()
        {
            //TaskLocalData.MainInstance.LoadTask().Add(task0);
            //TaskLocalData.MainInstance.SaveTask();
            //TaskLocalData.MainInstance.RemoveTask(task);
            executor.Init(OnFinished1);
            executorTaskWorking.Init(OnFinished1);
            executorTaskFinished.Init(OnFinished1);
            executor2.Init(OnFInish2);
            
        }



        // Update is called once per frame
        void Update()
        {
            if (input.PlayerActions.Switch.triggered && canInteract)
            {
                Cursor.lockState = CursorLockMode.None;
                if (TaskLocalData.MainInstance.GetTaskByUid(task.uid)!=null && TaskLocalData.MainInstance.GetTaskByUid(task.uid).currentState == TaskState.Working)
                {
                    executorTaskWorking.Execute();
                }
                else if(TaskLocalData.MainInstance.GetTaskByUid(task.uid) != null && TaskLocalData.MainInstance.GetTaskByUid(task.uid).currentState == TaskState.Finished)
                {
                    executorTaskFinished.Execute();
                }
                else
                {
                    executor.Execute();
                }
                
            }
        }


        void OnFinished1(bool success)
        {

        }
        
        private void OnFInish2(bool success)
        {
            task.currentState = TaskState.Working;
            if(TaskLocalData.MainInstance.tasks.Contains(task))return;
            TaskLocalData.MainInstance.AddTask(task);

        }
    }
}
