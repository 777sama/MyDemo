using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace zzz
{
    public class PackageDetail : MonoBehaviour
    {
        private Transform UIStars;
        private Transform UIDescription;
        private Transform UIIcon;
        private Transform UITitle;
        private Transform UILeveleText;
        private Transform UISkillDescription;


        private PackageLocalItem packageLocalData;
        private PackageTableItem packageTableItem;
        private PackagePanel uiParent;


        private void Awake()
        {
            InitUIName();
        }


        private void InitUIName()
        {
            UIStars = transform.Find("Center/Stars");
            UIDescription = transform.Find("Center/Description");
            UIIcon = transform.Find("Center/icon");
            UITitle = transform.Find("Top/Title");
            UILeveleText = transform.Find("Bottom/Level/Level");
            UISkillDescription = transform.Find("Bottom/Description");
        }

        public void Refresh(PackageLocalItem packageLocalData,PackagePanel uiParent)
        {
            this.packageLocalData = packageLocalData;
            this.packageTableItem = UserAssetManager.MainInstance.GetPackageItemById(packageLocalData.id);
            this.uiParent = uiParent;
            if (packageLocalData.type == GameConst.PackageTypeWeapon)
            {
                UILeveleText.GetComponent<TMP_Text>().text = string.Format("Lv.{0}/40", this.packageLocalData.level.ToString());
            }
            else
            {
                UILeveleText.GetComponent<TMP_Text>().text = string.Format("Num:{0}", this.packageLocalData.num.ToString());
            }
            
            UIDescription.GetComponent<TMP_Text>().text = this.packageTableItem.description;
            UISkillDescription.GetComponent<TMP_Text>().text = this.packageTableItem.skillDescription;
            UITitle.GetComponent<TMP_Text>().text = this.packageTableItem.name;

            Texture2D t = (Texture2D)Resources.Load(this.packageTableItem.imagePath);
            Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
            UIIcon.GetComponent<Image>().sprite = temp;

            RefreshStars();
        }

        public void RefreshStars()
        {
            for (int i = 0; i < UIStars.childCount; i++)
            {
                Transform star = UIStars.GetChild(i);
                if (this.packageTableItem.star > i)
                {
                    star.gameObject.SetActive(true);
                }
                else
                {
                    star.gameObject.SetActive(false);
                }
            }
        }
    }
}
