using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace zzz
{
    public class DialogueBox : MonoBehaviour
    {
        public PlayerInput input;

        [Header("Components")]
        [SerializeField] private Image background;
        [SerializeField] private Widget widget;
        [SerializeField] private TextMeshProUGUI speaker;
        [SerializeField] private AdvancedText content;
        [SerializeField] private Widget nextCursorWidget;
        [SerializeField] private Animator nextCursorAnimator;
        private static readonly int click = Animator.StringToHash("Click");


        [Header("Configs")]
        [SerializeField] private Sprite[] backgroundStyles;

        private bool interactable;
        private bool printFinished;
        private bool canQuickShow;
        private bool autoNext;

        private bool CanQuickShow => !printFinished && canQuickShow;
        private bool CanNext => printFinished;

        public Action<bool> OnNext;//bool类型代表下一句是否强制直接显示


        private void Awake()
        {
            content.OnFinished = PrintFinished;
        }

        private void PrintFinished()
        {
            if (autoNext)
            {
                interactable = false;
                OnNext(false);
            }
            else
            {
                interactable = true;
                printFinished = true;
                nextCursorWidget.Fade(1, 0.2f, null);
            }
        }

        // Update is called once per frame
        void Update()
        {
           if(interactable) UpdateInput();
        }

        private void UpdateInput()
        {
            if (input.InputActions.Player.LATK.triggered)
            {
                
                if (canQuickShow)
                {
                    content.QuickShowRemaining();
                    PrintFinished();
                }
                else if (CanNext)
                {
                    interactable = false;
                    nextCursorAnimator.SetTrigger(click);
                    nextCursorWidget.Fade(0, 0.5f, null);
                    OnNext(true);
                }
            }
            else if (input.InputActions.Player.Skill.triggered)
            {
                if (CanNext)
                {
                    interactable = false;
                    nextCursorAnimator.SetTrigger(click);
                    nextCursorWidget.Fade(0, 0.5f, null);
                    OnNext(false);
                }
            }
        }

        public void Open(Action<bool> nextEvent,int boxStyle = 0)
        {
            OnNext = nextEvent;
            background.sprite = backgroundStyles[boxStyle];
            

            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                
                widget.Fade(1, 0.2f, null);
                speaker.SetText("");
                content.Initialise();
            }
            nextCursorWidget.RenderOpacity = 0;
            
            OnNext(false);
        }

        public void Close(Action onClosed)
        {
            widget.Fade(0, 0.4f, () =>
            {
                gameObject.SetActive(false);
                onClosed?.Invoke();
            });
        }

        public IEnumerator PrintDialogue(string content, string speaker, bool canQuickShow, bool needTyping = true, bool autoNext = false) 
        {
            interactable = false;
            printFinished = false;
            
            if(this.content.text != "")
            {
                this.content.Disappear();
                yield return new WaitForSecondsRealtime(0.3f);
            }

            this.canQuickShow = canQuickShow;
            this.autoNext = autoNext;
            this.speaker.SetText(speaker);

            if (needTyping)
            {
                this.interactable = true;
                this.content.StartCoroutine(this.content.SetText(content, AdvancedText.DisplayeType.Typing));
            }
            else
            {
                this.content.StartCoroutine(this.content.SetText(content, AdvancedText.DisplayeType.Fading));
            }
        }
    }
}
