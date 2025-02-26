using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace zzz
{
    public class TaskCell : MonoBehaviour, IPointerClickHandler
    {
        private Transform UIName;
        private Transform UIFinished;

        private TaskPanel uiParent;
        private TaskDataSO task;


        private void Awake()
        {
            InitUIName();
        }

        private void InitUIName()
        {
            UIName = transform.Find("Name");
            UIFinished = transform.Find("Finished");
        }

        public void Refresh(string uid,TaskPanel uiPanel)
        {
            this.task = TaskLocalData.MainInstance.GetTaskByUid(uid);
            this.uiParent = uiPanel;

            UIName.GetComponent<TMP_Text>().text = this.task.name;
            if(TaskLocalData.MainInstance.GetTaskByUid(uid).currentState == TaskState.Finished)
            {
                UIFinished.gameObject.SetActive(true);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (this.uiParent.chooseUid == this.task.uid) return;
            this.uiParent.chooseUid = this.task.uid;
        }
    }
}
