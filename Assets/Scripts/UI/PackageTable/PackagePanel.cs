using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace zzz
{

    public enum PackageMode
    {
        normal,
        delete,
        sort
    }

    public enum PackageKind
    {
        Weapon,
        Material
    }
    public class PackagePanel : BasePanel 
    {
        private Transform UIMenu;
        private Transform UIMenuWeapon;
        private Transform UIMenuMaterial;
        private Transform UIMenuWeaponIcon;
        private Transform UIMenuMaterialIcon;
        private Transform UIMenuWeaponSelect;
        private Transform UIMenuMaterialSelect;
        private Transform UICloseBtn;
        private Transform UICenter;
        private Transform UIScrollview;
        private Transform UIDetailPanel;
        private Transform UILeftBtn;
        private Transform UIRightBtn;
        private Transform UIDeletePanel;
        private Transform UIDeleteBackBtn;
        private Transform UIDeleteConfirmBtn;
        private Transform UIBottomMenus;
        private Transform UIDeleteBtn;
        //当前处于什么状态
        public PackageMode curMode = PackageMode.normal;
        public PackageKind curKind = PackageKind.Weapon;

        public List<PackageKind> kinds = new List<PackageKind>() { PackageKind.Weapon, PackageKind.Material };
        public int curIndex = 0;

        public GameObject PackageUIItemPrefab;

        public List<string> deleteChooseUid;

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
                UIDetailPanel.gameObject.SetActive(true);
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
            RefreshUI();
        }

        private void RefreshUI()
        {
            RefreshScroll(curKind);
            RefreshTopMenu(curKind);
        }

        private void RefreshDetail()
        {
            PackageLocalItem localItem = UserAssetManager.MainInstance.GetPackageLocalItemByUId(chooseUid);
            UIDetailPanel.GetComponent<PackageDetail>().Refresh(localItem, this);
        }

        private void RefreshTopMenu(PackageKind currentKind)
        {
            if (currentKind == PackageKind.Weapon)
            {
                UIMenuMaterialIcon.gameObject.SetActive(false);
                UIMenuMaterialSelect.gameObject.SetActive(false);
                UIMenuWeaponIcon.gameObject.SetActive(true);
                UIMenuWeaponSelect.gameObject.SetActive(true);
            }
            if (currentKind == PackageKind.Material)
            {
                UIMenuMaterialIcon.gameObject.SetActive(true);
                UIMenuMaterialSelect.gameObject.SetActive(true);
                UIMenuWeaponIcon.gameObject.SetActive(false);
                UIMenuWeaponSelect.gameObject.SetActive(false);
            }
        }

        private void RefreshScroll(PackageKind currentKind)
        {
            if (currentKind == PackageKind.Weapon)
            {
                RectTransform scrollContent = UIScrollview.GetComponent<ScrollRect>().content;
                for (int i = 0; i < scrollContent.childCount; i++)
                {
                    Destroy(scrollContent.GetChild(i).gameObject);
                }

                foreach (PackageLocalItem localData in UserAssetManager.MainInstance.GetSortPackageLocalData(GameConst.PackageTypeWeapon))
                {
                    Transform PackageUIItem = Instantiate(PackageUIItemPrefab.transform, scrollContent) as Transform;
                    PackageCell packageCell = PackageUIItem.GetComponent<PackageCell>();
                    packageCell.Refresh(localData, this);
                }
            }
            if (currentKind == PackageKind.Material)
            {
                RectTransform scrollContent = UIScrollview.GetComponent<ScrollRect>().content;
                for (int i = 0; i < scrollContent.childCount; i++)
                {
                    Destroy(scrollContent.GetChild(i).gameObject);
                }

                foreach (PackageLocalItem localData in UserAssetManager.MainInstance.GetSortPackageLocalData(GameConst.PackageTypeMaterial))
                {
                    Transform PackageUIItem = Instantiate(PackageUIItemPrefab.transform, scrollContent) as Transform;
                    PackageCell packageCell = PackageUIItem.GetComponent<PackageCell>();
                    packageCell.Refresh(localData, this);
                }
            }
        }

        private void InitUI()
        {
            InitUIName();
            InitClick();
        }

        private void InitUIName()
        {
            UIMenu = transform.Find("TopCenter/Menus");
            UIMenuWeapon = transform.Find("TopCenter/Menus/Weapon");
            UIMenuMaterial = transform.Find("TopCenter/Menus/Materials");
            UIMenuWeaponIcon = transform.Find("TopCenter/Menus/Weapon/Icon1");
            UIMenuMaterialIcon = transform.Find("TopCenter/Menus/Materials/Icon1");
            UIMenuWeaponSelect = transform.Find("TopCenter/Menus/Weapon/Select");
            UIMenuMaterialSelect = transform.Find("TopCenter/Menus/Materials/Select");
            UICloseBtn = transform.Find("RightTop/CloseBtn");
            UICenter = transform.Find("Center");
            UIScrollview = transform.Find("Center/Scroll View");
            UIDetailPanel = transform.Find("Center/DetailPanel");
            UILeftBtn = transform.Find("Center/LastBtn");
            UIRightBtn = transform.Find("Center/NextBtn");
            UIDetailPanel = transform.Find("Center/DetailPanel");
            UIDeleteBackBtn = transform.Find("Bottom/DeletePanel/BackBtn");
            UIDeleteConfirmBtn = transform.Find("Bottom/DeletePanel/DeleteBtn");
            UIBottomMenus = transform.Find("Bottom/BottomMenus");
            UIDeleteBtn = transform.Find("Bottom/BottomMenus/DeleteBtn");
            UIDeletePanel = transform.Find("Bottom/DeletePanel");

            UIDeletePanel.gameObject.SetActive(false);
            UIBottomMenus.gameObject.SetActive(true);
        }
        
        private void InitClick()
        {
            UIMenuWeapon.GetComponent<Button>().onClick.AddListener(OnClickWeapon);
            UIMenuMaterial.GetComponent<Button>().onClick.AddListener(OnClickMaterial);
            UICloseBtn.GetComponent<Button>().onClick.AddListener(OnClickClose);
            UILeftBtn.GetComponent<Button>().onClick.AddListener(OnClickLeft);
            UIRightBtn.GetComponent<Button>().onClick.AddListener(OnClickRight);

            UIDeleteBackBtn.GetComponent<Button>().onClick.AddListener(OnDeleteBack);
            UIDeleteConfirmBtn.GetComponent<Button>().onClick.AddListener(OnDeleteConfirm);
            UIDeleteBtn.GetComponent<Button>().onClick.AddListener(OnDelete);
            
        }

        //添加删除选中项
        public void AddChooseDeleteUid(string uid)
        {
            this.deleteChooseUid ??= new List<string>();
            if (!this.deleteChooseUid.Contains(uid))
            {
                this.deleteChooseUid.Add(uid);
            }
            else
            {
                this.deleteChooseUid.Remove(uid);
            }
            RefreshDeletePanel();
        }

        private void RefreshDeletePanel()
        {
            RectTransform scrollContent = UIScrollview.GetComponent<ScrollRect>().content;
            foreach (Transform cell in scrollContent)
            {
                PackageCell packageCell = cell.GetComponent<PackageCell>();
                //物品刷新选中状态
                packageCell.RefreshDeleteState();
            }
        }

        private void OnDelete()
        {
            print(">>>> OnDelete");
            curMode = PackageMode.delete;
            UIDeletePanel.gameObject.SetActive(true);
        }

        private void OnDeleteConfirm()
        {
            print(">>>> OnDeleteConfirm");
            if(this.deleteChooseUid == null)
            {
                return;
            }
            if(this.deleteChooseUid.Count == 0)
            {
                return;
            }
            UserAssetManager.MainInstance.DeletePackageItems(this.deleteChooseUid);
            //删完刷新
            RefreshUI();
        }

        private void OnDeleteBack()
        {
            print(">>>> OnDeleteBack");
            curMode = PackageMode.normal;
            UIDeletePanel.gameObject.SetActive(false);
            //重置选中的删除列表
            deleteChooseUid = new List<string>();
            //刷新选中状态
            RefreshDeletePanel();
        }

        private void OnClickRight()
        {
            
            if(curIndex < kinds.Count-1)
            {
                curIndex++;
                UIDetailPanel.gameObject.SetActive(false);
                curKind = kinds[curIndex];
                RefreshUI();
            }
            print(">>>> OnClickRight");
        }

        private void OnClickLeft()
        {
            if (curIndex > 0)
            {
                curIndex--;
                UIDetailPanel.gameObject.SetActive(false);
                curKind = kinds[curIndex];
                RefreshUI();
            }
            print(">>>> OnClickLeft");
        }

        private void OnClickClose()
        {
            print(">>>> OnClickClose");
            ClosePanel();
            UIManager.MainInstance.OpenPanel(UIManager.UIConst.MainPanel);
            
        }

        private void OnClickMaterial()
        {
            print(">>>> OnClickMaterial");
            curIndex = 1;
            UIDetailPanel.gameObject.SetActive(false);
            curKind = PackageKind.Material;
            RefreshUI();
        }

        private void OnClickWeapon()
        {
            print(">>>> OnClickWeapon");
            curIndex = 0;
            UIDetailPanel.gameObject.SetActive(false);
            curKind = PackageKind.Weapon;
            RefreshUI();
        }
    }
}
