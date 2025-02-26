using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace zzz
{
    public class AdvancedButton : Button
    {

        protected override void Awake()
        {
            base.Awake();
            onClick.AddListener(OnClickEvent);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            Select();
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            UIManager.MainInstance.SetCurrentSelectable(this);
        }

        protected virtual void OnClickEvent()
        {

        }

        protected int index;

        public Action<int> OnConfirm;//int参数代表自身的下标序号
        public virtual void Init(string content,int index,Action<int> onConfirmEvent)
        {
            this.index = index;
            OnConfirm += onConfirmEvent;
        }

        public void Confirm()
        {
            OnConfirm(index);
        }

    }
}
