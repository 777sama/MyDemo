using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace zzz
{
    public class TaskPanel : BasePanel
    {
        private Transform UICloseBtn;
        private Transform UIScrollview;
        private Transform UITaskDetail;

        public GameObject TaskUIItemPrefab;
        private string _chooseUid;
        public string chooseUid
        {
            get
            {
                return _chooseUid;
            }
            set
            {
                _chooseUid = value;
                RefreshDetail();
            }
        }
        protected override void Awake()
        {
            base.Awake();
            InitUI();
        }

        private void Start()
        {
            RefreshScroll();
        }

        private void InitUI()
        {
            UICloseBtn = transform.Find("CloseBtn");
            UIScrollview = transform.Find("TaskMenu");
            UITaskDetail = transform.Find("TaskDetail");

            UICloseBtn.GetComponent<Button>().onClick.AddListener(OnClickClose);
        }

        private void RefreshScroll()
        {
            RectTransform scrollContent = UIScrollview.GetComponent<ScrollRect>().content;
            if (scrollContent.childCount != 0) return;

            foreach(TaskDataSO task in TaskLocalData.MainInstance.LoadTask())
            {
                Transform TaskUIItem = Instantiate(TaskUIItemPrefab.transform,scrollContent) as Transform; 
                TaskCell taskCell = TaskUIItem.GetComponent<TaskCell>();
                taskCell.Refresh(task.uid, this);
            }
            
        }

        private void RefreshDetail()
        {
            TaskDataSO task = TaskLocalData.MainInstance.GetTaskByUid(chooseUid);
            UITaskDetail.GetComponent<TaskDetail>().Refresh(task);
        }

        private void OnClickClose()
        {
            ClosePanel();
            UIManager.MainInstance.OpenPanel(UIManager.UIConst.MainPanel);
            
        }
    }
}
