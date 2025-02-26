using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class BasePanel : MonoBehaviour
    {
        protected bool isRemove = false;
        protected new string name;

        protected virtual void Awake()
        {

        }

        public virtual void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public virtual void OpenPanel(string name)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            this.name = name;
            SetActive(true);
        }

        public virtual void ClosePanel()
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            isRemove = true;
            SetActive(false);
            Destroy(gameObject);
            
            if (UIManager.MainInstance.panelDict.ContainsKey(name))
            {
                UIManager.MainInstance.panelDict.Remove(name);
            }
        }
    }
}
