using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace zzz
{
    public class SynthesisPanel : BasePanel
    {
        private Transform UICloseBtn;
        private Transform UIScrollview;
        private Transform UISynthesisDetailPanel;

        public GameObject SynthesisUIItemPrefab;
       

        public int _chooseId;
        public int chooseId
        {
            get
            {
                return _chooseId;
            }
            set
            {
                _chooseId = value;
                UISynthesisDetailPanel.gameObject.SetActive(true);
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
            InitUIName();
            InitClick();
        }

        private void InitUIName()
        {
            UICloseBtn = transform.Find("TopRight/CloseBtn");
            UIScrollview = transform.Find("Kinds/Scroll View");
            UISynthesisDetailPanel = transform.Find("DetailPanel");
            
        }
        private void InitClick()
        {
            UICloseBtn.GetComponent<Button>().onClick.AddListener(OnClickClose);
        }

        private void RefreshScroll()
        {
            RectTransform scrollContent = UIScrollview.GetComponent<ScrollRect>().content;
            
            if (scrollContent.childCount!=0) return;
            
            foreach (PackageTableItem item in UserAssetManager.MainInstance.GetPackageTableByType(GameConst.PackageTypeMaterial))
            { 
                if(item.id == 12||item.id == 15||item.id == 18) continue;
                Transform SynthesisUIItem = Instantiate(SynthesisUIItemPrefab.transform,scrollContent) as Transform;
                SynthesisCell synthesisCell = SynthesisUIItem.GetComponent<SynthesisCell>();
                synthesisCell.Refresh(item.id, this);
            }
        }

        private void RefreshDetail()
        {
            PackageTableItem tableItem = UserAssetManager.MainInstance.GetPackageItemById(chooseId);
            UISynthesisDetailPanel.GetComponent<SynthesisDetail>().Refresh(tableItem);
        }


        private void OnClickClose()
        {
            print(">>>> OnClickClose");
            ClosePanel();
            UIManager.MainInstance.OpenPanel(UIManager.UIConst.MainPanel);
            
        }
    }
}
