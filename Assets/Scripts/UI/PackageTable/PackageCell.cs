using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;

namespace zzz
{
    public class PackageCell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private Transform UIIcon;
        private Transform UIHead;
        private Transform UINew;
        private Transform UISelete;
        private Transform UILevel;
        private Transform UIStars;
        private Transform UIDeleteSelect;
        private Transform UISelectAni;
        private Transform UIMouseOverAni;

        private PackageLocalItem packageLocalItem;
        private PackageTableItem packageTableItem;
        private PackagePanel uiParent;

        private void Awake()
        {
            InitUIName();
        }

        private void InitUIName()
        {
            UIIcon = transform.Find("Top/Icon");
            UINew = transform.Find("Top/New");
            UISelete = transform.Find("Select");
            UILevel = transform.Find("Bottom/LevelText");
            UIStars = transform.Find("Bottom/Stars");
            UIDeleteSelect = transform.Find("DeleteSelect");
            UISelectAni = transform.Find("SelectAni");
            UIMouseOverAni = transform.Find("MouseOverAni");

            UIDeleteSelect.gameObject.SetActive(false);
            UISelectAni.gameObject.SetActive(false);
            UIMouseOverAni.gameObject.SetActive(false);
        }

        public void Refresh(PackageLocalItem packageLocalItem,PackagePanel uiParent)
        {
            this.packageLocalItem = packageLocalItem;
            this.packageTableItem = UserAssetManager.MainInstance.GetPackageItemById(packageLocalItem.id);
            this.uiParent = uiParent;
            
            if(packageLocalItem.type == GameConst.PackageTypeMaterial)
            {
                //数量信息
                UILevel.GetComponent<TMP_Text>().text = this.packageLocalItem.num.ToString();
            }
            if (packageLocalItem.type == GameConst.PackageTypeWeapon)
            {
                //等级信息
                UILevel.GetComponent<TMP_Text>().text = "Lv." + this.packageLocalItem.level.ToString();
            }
            //是否是新获得
            UINew.gameObject.SetActive(this.packageLocalItem.isNew);
            //物品的图片
            Texture2D t = (Texture2D)Resources.Load(this.packageTableItem.imagePath);
            Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
            UIIcon.GetComponent<Image>().sprite = temp;
            //刷新星级
            RefreshStars();
        }

        public void RefreshStars()
        {
            for (int i = 0; i < UIStars.childCount; i++)
            {
                Transform star = UIStars.GetChild(i);
                if(this.packageTableItem.star > i)
                {
                    star.gameObject.SetActive(true);
                }
                else
                {
                    star.gameObject.SetActive(false);
                }
            }
        }


        public void RefreshDeleteState()
        {
            if (this.uiParent.deleteChooseUid.Contains(this.packageLocalItem.uid))
            {
                this.UIDeleteSelect.gameObject.SetActive(true);
            }
            else
            {
                this.UIDeleteSelect.gameObject.SetActive(false) ;
            }
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("OnPointerClick:" + eventData.ToString());
            if(this.uiParent.curMode == PackageMode.delete)
            {
                this.uiParent.AddChooseDeleteUid(this.packageLocalItem.uid);
            }

            if(this.uiParent.chooseUid == this.packageLocalItem.uid) return;

            this.uiParent.chooseUid = this.packageLocalItem.uid;
            UISelectAni.gameObject.SetActive(true);
            UISelectAni.GetComponent<Animator>().SetTrigger("in");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("OnPointerEnter:" + eventData.ToString());
            UIMouseOverAni.gameObject.SetActive(true);
            UIMouseOverAni.GetComponent<Animator>().SetTrigger("in");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("OnPointerExit:" + eventData.ToString());
            UIMouseOverAni.GetComponent<Animator>().SetTrigger("out");
        }
    }
}
