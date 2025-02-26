using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace zzz
{
    public class ScenesChangePanel : BasePanel
    {
        private Slider UISlider;
        private Transform UIText;
        private Transform UIButton;


        protected override void Awake()
        {
            base.Awake();
            InitUI();
        }

        private void InitUI()
        {
            UISlider = transform.Find("Slider").GetComponent<Slider>();
            UIText = transform.Find("Text");
            UIButton = transform.Find("Button");

            UIButton.GetComponent<Button>().onClick.AddListener(OnContinuClick);
            
            UIButton.gameObject.SetActive(false);
            UIText.gameObject.SetActive(false);
        }

        private void Update()
        {
            UISlider.value = GameManager.MainInstance.loadValue / 0.9f;
            if(UISlider.value >= 0.9f)
            {
                UIText.gameObject.SetActive(true);
                UIButton.gameObject.SetActive(true);
            }
        }

        private void OnContinuClick()
        {
            GameManager.MainInstance.applyChangeScene = true;
            Time.timeScale = 1f;    

        }
    }
}
