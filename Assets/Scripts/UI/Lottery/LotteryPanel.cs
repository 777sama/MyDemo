using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace zzz
{
    public class LotteryPanel : BasePanel
    {
        private Transform UIClose;
        private Transform UICenter;
        private Transform UILottery10;
        private Transform UILottery1;
        private Transform UIModeChangeBtn;
        private Transform UIModeText;
        private GameObject LotteryCellPrefab;
        private int currentMode;

        protected override void Awake()
        {
            base.Awake();
            currentMode = GameConst.PackageTypeWeapon;
            InitUI();
            InitPrefab();
            UIModeText.GetComponent<TMP_Text>().text = "武器";
        }

        private void InitUI()
        {
            UIClose = transform.Find("TopRight/CloseBtn");
            UICenter = transform.Find("Center");
            UILottery10 = transform.Find("Bottom/Lottery10");
            UILottery1 = transform.Find("Bottom/Lottery1");
            UIModeChangeBtn = transform.Find("ModeChangeBtn");
            UIModeText = transform.Find("ModeChangeBtn/Text");

            UILottery10.GetComponent<Button>().onClick.AddListener(OnLotery10Btn);
            UILottery1.GetComponent<Button>().onClick.AddListener(OnLottery1Btn);
            UIClose.GetComponent<Button>().onClick.AddListener(OnClose);
            UIModeChangeBtn.GetComponent<Button>().onClick.AddListener(OnModeChange);
        }

        private void InitPrefab()
        {
            LotteryCellPrefab = Resources.Load("Prefab/UI/Lottery/LotteryItem") as GameObject;
        }

        private void OnLottery1Btn()
        {
            print(">>>> OnLottery1Bt");
            //销毁原本的卡片
            for (int i = 0; i < UICenter.childCount; i++)
            {
                Destroy(UICenter.GetChild(i).gameObject);
            }
            PackageLocalItem item = UserAssetManager.MainInstance.GetLotteryRandom1(currentMode);

            Transform LotteryCellTran = Instantiate(LotteryCellPrefab.transform, UICenter) as Transform;
            //对卡片做信息展示刷新
            LotteryCell LotteryCell = LotteryCellTran.GetComponent<LotteryCell>();
            LotteryCell.Refresh(item, this);
        }

        private void OnLotery10Btn()
        {
            print(">>>> OnLotery10Btn");
            //销毁原本的卡片
            for (int i = 0; i < UICenter.childCount; i++)
            {
                Destroy(UICenter.GetChild(i).gameObject);
            }
            List<PackageLocalItem> items = UserAssetManager.MainInstance.GetLotteryRandom10(currentMode);

            foreach (PackageLocalItem item in items)
            {
                Transform LotteryCellTran = Instantiate(LotteryCellPrefab.transform, UICenter) as Transform;
                //对卡片做信息展示刷新
                LotteryCell LotteryCell = LotteryCellTran.GetComponent<LotteryCell>();
                LotteryCell.Refresh(item,this);
            }
        }

        private void OnClose()
        {
            print(">>>> OnClose"); 
            ClosePanel();
            UIManager.MainInstance.OpenPanel(UIManager.UIConst.MainPanel);
            
        }


        private void OnModeChange()
        {
            if(currentMode == GameConst.PackageTypeWeapon)
            {
                currentMode = GameConst.PackageTypeMaterial;
                UIModeText.GetComponent<TMP_Text>().text = "材料";
            }
            else
            {
                currentMode = GameConst.PackageTypeWeapon;
                UIModeText.GetComponent<TMP_Text>().text = "武器";
            }
        }
    }
}
