using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace zzz
{
    public class TaskDetail : MonoBehaviour
    {
        private Transform UITaskName;
        private Transform UITaskDescription;
        private Transform UIGoBtn;

        private TaskDataSO task;

        private void Awake()
        {
            InitUI();
        }

        private void InitUI()
        {
            UITaskName = transform.Find("TaskName");
            UITaskDescription = transform.Find("TaskDescription");
            UIGoBtn = transform.Find("GoBtn");


            UIGoBtn.GetComponent<Button>().onClick.AddListener(OnClickGoBtn);
        }


        public void Refresh(TaskDataSO task)
        {
            this.task = task;
            UITaskName.GetComponent<TMP_Text>().text = task.name;
            UITaskDescription.GetComponent<TMP_Text>().text = task.description;
            TaskFiniedOrNot(task);
        }

        private void TaskFiniedOrNot(TaskDataSO task)
        {
            if(task.currentState == TaskState.Working)
            {
                UIGoBtn.gameObject.SetActive(true);
            }
            else
            {
                UIGoBtn.gameObject.SetActive(false);
            }
        }

        private void OnClickGoBtn()
        {
            GameManager.MainInstance.currentTask = task;
            UIManager.MainInstance.ClosePanel(UIManager.UIConst.TaskPanel);
            UIManager.MainInstance.OpenPanel(UIManager.UIConst.ScenesChangePanel);
           
            GameManager.MainInstance.LoadSceneAsync(SceneConst.FightScene);
        }
    }
}
