using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace zzz
{
    public class GameOverPanel : BasePanel
    {
        private Transform UIBack;

        protected override void Awake()
        {
            base.Awake();
            InitUI();
        }

        private void InitUI()
        {
            UIBack = transform.Find("BackBtn");

            UIBack.GetComponent<Button>().onClick.AddListener(OnBtnBack);
        }

        private void OnBtnBack()
        {
            ClosePanel();
            UIManager.MainInstance.OpenPanel(UIManager.UIConst.ScenesChangePanel);
            
            GameManager.MainInstance.LoadSceneAsync(SceneConst.CityScene);
        }
    }
}
