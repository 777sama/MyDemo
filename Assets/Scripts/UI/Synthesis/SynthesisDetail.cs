using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;


namespace zzz
{
    public class SynthesisDetail : MonoBehaviour
    {
        private Transform UITitle;
        private Transform UISyntheticsIcon;
        private Transform UIMaterialIcon;
        private Transform UIMaterialNum;
        private Transform UIPlusBtn;
        private Transform UIDecreaseBtn;
        private Transform UISynthesisNum;
        private Transform UISynthesisBtn;

        private int synthesisNum = 0;
        private int needMaterialNumByOne = 4;

        private PackageTableItem packageTableItem;
        private PackageLocalItem materialLocalData;
        private PackageTableItem materialItem;
        

        private void Awake()
        {
            InitUIName();
            InitUIClick();
        }

        private void InitUIName()
        {
            UITitle = transform.Find("Top/Name");
            UISyntheticsIcon = transform.Find("Center/Top/SyntheticsIcon");
            UIMaterialIcon = transform.Find("Center/Top/MaterialIcon");
            UIMaterialNum = transform.Find("Center/Top/Num");
            UIPlusBtn = transform.Find("Center/Bottom/PlusBtn");
            UIDecreaseBtn = transform.Find("Center/Bottom/DecreaseBtn");
            UISynthesisNum = transform.Find("Center/Bottom/Image/Num");
            UISynthesisBtn = transform.Find("Bottom/Button");
        }

        private void InitUIClick()
        {
            UIPlusBtn.GetComponent<Button>().onClick.AddListener(OnClickPlus);
            UIDecreaseBtn.GetComponent<Button>().onClick.AddListener(OnClickDecreaseBtn);
            UISynthesisBtn.GetComponent<Button>().onClick.AddListener(OnClickSynthesis);
        }

        public void Refresh(PackageTableItem packageTableItem)
        {
            synthesisNum = 0;
            this.packageTableItem = packageTableItem;
            this.materialLocalData = UserAssetManager.MainInstance.GetPackageLocalItemById(packageTableItem.id+1);
            this.materialItem = UserAssetManager.MainInstance.GetPackageItemById(packageTableItem.id+1);

            UITitle.GetComponent<TMP_Text>().text = packageTableItem.name;

            Texture2D t = (Texture2D)Resources.Load(this.packageTableItem.imagePath);
            Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
            UISyntheticsIcon.GetComponent<Image>().sprite = temp;

            Texture2D t2 = (Texture2D)Resources.Load(this.materialItem.imagePath);
            Sprite temp2 = Sprite.Create(t2, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
            UIMaterialIcon.GetComponent<Image>().sprite = temp2;


            NumUpdate();
        }

        private void NumUpdate()
        {
            UIMaterialNum.GetComponent<TMP_Text>().text = string.Format("{0}/{1}", synthesisNum * needMaterialNumByOne, this.materialLocalData.num);
            UISynthesisNum.GetComponent<TMP_Text>().text = synthesisNum.ToString();
        }

        private void OnClickSynthesis()
        {
            
            PackageLocalData.MainInstance.MaterialNumChange(packageTableItem.id, synthesisNum);
            PackageLocalData.MainInstance.MaterialNumChange(packageTableItem.id+1, -synthesisNum * needMaterialNumByOne);
            Refresh(UserAssetManager.MainInstance.GetPackageItemById(packageTableItem.id));
            NumUpdate();
        }

        private void OnClickDecreaseBtn()
        {
            if (synthesisNum == 0) return;
            synthesisNum--;
            NumUpdate();
        }

        private void OnClickPlus()
        {
            if ((synthesisNum+1)* needMaterialNumByOne > this.materialLocalData.num) return;
            synthesisNum++;
            NumUpdate();
        }
    }
}
