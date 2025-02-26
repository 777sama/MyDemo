using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace zzz
{
    public class AdvancedButtonA : AdvancedButton
    {
        Widget Select;
        Animator animator;
        

        protected override void Awake()
        {
            base.Awake();
            Select = transform.Find("Select").GetComponent<Widget>();
            animator = GetComponent<Animator>();
        }

        

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            Select.Fade(1, 0.1f, null);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            Select.Fade(0, 0.25f, null);
        }

        protected override void OnClickEvent()
        {
            base.OnClickEvent();
            animator.SetTrigger("Click");
        }

        public override void Init(string content, int index, Action<int> onConfirmEvent)
        {
            base.Init(content, index, onConfirmEvent);
            AdvancedText text = GetComponentInChildren<AdvancedText>();
            text.StartCoroutine(text.SetText(content, AdvancedText.DisplayeType.Default));
        }
    }
}
