using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace zzz
{
    public class MainPanel : BasePanel
    {
        private Transform UILottery;
        private Transform UIPackage;
        private Transform UISynthesis;
        private Transform UITask;


        protected override void Awake()
        {
            base.Awake();
            InitUI();
        }

        public override void OpenPanel(string name)
        {
            base.OpenPanel(name);
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void InitUI()
        {
            UILottery = transform.Find("TopRight/LotteryBtn");
            UIPackage = transform.Find("TopRight/PackageBtn");
            UISynthesis = transform.Find("TopRight/SynthesisBtn");
            UITask = transform.Find("TopRight/SceneChangeBtn");

            UILottery.GetComponent<Button>().onClick.AddListener(OnBtnLottery);
            UIPackage.GetComponent<Button>().onClick.AddListener(OnBtnPackage);
            UISynthesis.GetComponent<Button>().onClick.AddListener(OnBtnSynthesis);
            UITask.GetComponent<Button>().onClick.AddListener (OnBtnTask);
        }

        private void OnBtnTask()
        {
            ClosePanel();
            UIManager.MainInstance.OpenPanel(UIManager.UIConst.TaskPanel);
            
        }

        private void OnBtnPackage()
        {
            ClosePanel();
            print(">>>> OnBtnPackage");
            UIManager.MainInstance.OpenPanel(UIManager.UIConst.PackagePanel);
            
        }

        private void OnBtnLottery()
        {
            ClosePanel();
            print(">>>> OnBtnLottery");
            UIManager.MainInstance.OpenPanel(UIManager.UIConst.LotteryPanel);
            
        }
        
        private void OnBtnSynthesis()
        {
            ClosePanel();
            print(">>>> OnBtnSynthesis");
            UIManager.MainInstance.OpenPanel(UIManager.UIConst.SynthesisPanel);
            
        }
    }
}
