using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace zzz
{
    public class SynthesisCell : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
    {

        private Transform UIIcon;
        private Transform UIName;

        private SynthesisPanel uiParent;
        private PackageTableItem packageTableItem;

        private void Awake()
        {
            InitUIName();
        }

        private void InitUIName()
        {
            UIIcon = transform.Find("Icon");
            UIName = transform.Find("Name");
        }

        public void Refresh(int id , SynthesisPanel uiParent)
        {
            this.packageTableItem = UserAssetManager.MainInstance.GetPackageItemById(id);
            this.uiParent = uiParent;

            UIName.GetComponent<TMP_Text>().text = this.packageTableItem.name;
            Texture2D t = (Texture2D)Resources.Load(this.packageTableItem.imagePath);
            Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
            UIIcon.GetComponent<Image>().sprite = temp;

        }

        

        public void OnPointerClick(PointerEventData eventData)
        {
            if (this.uiParent.chooseId == this.packageTableItem.id) return;
            this.uiParent.chooseId = this.packageTableItem.id;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }
        
        
    }
}
