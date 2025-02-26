using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class ChoicePanel : Widget
    {
        private List<AdvancedButton> buttons = new List<AdvancedButton>();

        public void AddButton(AdvancedButton button)
        {
            buttons.Add(button);
            button.transform.SetParent(transform);
            button.transform.localScale = Vector3.one;
            button.onClick.AddListener(DisableAllButton);
            button.OnConfirm += (_) => { Close(); };
        }

        private void DisableAllButton()
        {
            foreach (var item in buttons)
            {
                item.enabled = false;
            }
        }

        public void Open(int defaultSelectIndex,float duration = 0.2f)
        {
            RenderOpacity = 0f;
            Fade(1f, duration, () => 
            {
                if (defaultSelectIndex < buttons.Count)
                {
                    buttons[defaultSelectIndex].Select();
                }
                else
                {
                    buttons[0].Select();
                }
            });
        }

        public void Close(float duration = 0.2f)
        {
            UIManager.MainInstance.SetCurrentSelectable(null);
            Fade(0f, duration, () =>
            {
                foreach (var item in buttons)
                {
                    Destroy(item.gameObject);
                }
                buttons.Clear();
            });
        }
    }
}
