using GGG.Tool.Singleton;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Rendering.ReloadAttribute;

namespace zzz
{
    public class UIManager : Singleton<UIManager>
    {

        private Transform _uiRoot;

        //·�������ֵ�
        private Dictionary<string, string> pathDict;
        //Ԥ�Ƽ������ֵ�
        private Dictionary<string, GameObject> prefabDict;
        //�Ѵ򿪽���Ļ����ֵ�
        public Dictionary<string ,BasePanel> panelDict;

        private GameObject pfbButtonA;
        public Transform UIRoot
        {
            get
            {
                if (_uiRoot == null)
                {
                    if (GameObject.Find("Canvas"))
                    {
                        _uiRoot = GameObject.Find("Canvas").transform;
                    } else
                    {
                        _uiRoot = new GameObject("Canvas").transform;
                    }
                };
                return _uiRoot;
            }
        }


        protected override void Awake()
        {
            base.Awake();
            //DontDestroyOnLoad(gameObject);
            pfbButtonA = Resources.Load<GameObject>("Prefab/UI/TalkPanel/ButtonA") as GameObject;
        }

        private UIManager()
        {
            InitDicts();
        }

        private void InitDicts()
        {
            prefabDict = new Dictionary<string, GameObject>();
            panelDict = new Dictionary<string, BasePanel>();

            pathDict = new Dictionary<string, string>()
            {
                {UIConst.PackagePanel,"Package/PackagePanel"},
                {UIConst.LotteryPanel,"Lottery/LotteryPanel"},
                {UIConst.MainPanel,"Main/MainPanel"},
                {UIConst.SynthesisPanel,"Synthesis/SynthesisPanel" },
                {UIConst.ScenesChangePanel,"ScenesChange/ScenesChangePanel" },
                {UIConst.GameOverPanel,"GameOver/GameOverPanel" },
                {UIConst.TaskPanel,"Task/TaskPanel" }
            };
        }



        public BasePanel GetPanel(string name)
        {
            BasePanel panel = null;
            //����Ƿ��Ѵ�
            if(panelDict.TryGetValue(name,out panel))
            {
                return panel;
            }
            return null;
        }

        public BasePanel OpenPanel(string name)
        {
            BasePanel panel = null;
            //����Ƿ��Ѵ�
            if(panelDict.TryGetValue(name,out panel))
            {
                Debug.Log("�����Ѵ򿪣�"+ name);
                return null;
            }

            //���·���Ƿ���ȷ
            string path = "";
            if(!pathDict.TryGetValue(name,out path))
            {
                Debug.Log("�������ƴ��󣬻�δ����·��" + name);
                return null;
            }

            //ʹ�û���Ԥ�Ƽ�
            GameObject panelPrefab = null;
            if(!prefabDict.TryGetValue(name,out panelPrefab))
            {
                string realPath = "Prefab/UI/" + path;
                
                panelPrefab = Resources.Load<GameObject>(realPath) as GameObject;
                prefabDict.Add(name, panelPrefab);
            }
            
            //�򿪽���
            GameObject panelObject = GameObject.Instantiate(panelPrefab, UIRoot, false);
            panel = panelObject.GetComponent<BasePanel>();
            panelDict.Add(name,panel);
            Debug.Log(name);
            panel.OpenPanel(name);
            return panel;
        }

        public bool ClosePanel(string name)
        {
            BasePanel panel = null;

            if(!panelDict.TryGetValue(name,out panel))
            {
                Debug.Log("����δ��" + name);
                return false;
            }
            panel.ClosePanel();
            return true;
        }

        public class UIConst
        {
            public const string PackagePanel = "PackagePanel";
            public const string LotteryPanel = "LotteryPanel";
            public const string MainPanel = "MainPanel";
            public const string SynthesisPanel = "SynthesisPanel";
            public const string ScenesChangePanel = "ScenesChangePanel";
            public const string GameOverPanel = "GameOverPanel";
            public const string TaskPanel = "TaskPanel";
        }



        [SerializeField] private DialogueBox dialogueBox;
        private Selectable currentSelectable;
        public void SetCurrentSelectable(Selectable obj)
        {
            currentSelectable = obj;

        }

        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                if (currentSelectable != null)
                {
                    currentSelectable.Select();
                }
            }
        }
        public void OpenDialogueBox(Action<bool> onNextEvent,int boxStyle = 0)
        {
            dialogueBox.Open(onNextEvent, boxStyle);
        }
        public void PrintDialogue(DialogueData data)
        {
            dialogueBox.StartCoroutine(dialogueBox.PrintDialogue(data.Content, data.Speaker, data.CanQuickShow, data.NeedTyping, data.AutoNext));
        }
        public void CloseDialogueBox()
        {
            dialogueBox.Close(null);
        }

        
        [SerializeField] private ChoicePanel choicePanel;
        public void CreateDialogueChocies(ChoiceData[] datas,Action<int> onConfirmEvent,int defaultSelectIndex)
        {
            for (int i = 0; i < datas.Length; i++)
            {
                AdvancedButtonA button = Instantiate(pfbButtonA).GetComponent<AdvancedButtonA>();
                button.gameObject.name = "ButtonA" + i;
                button.Init(datas[i].Content,i,onConfirmEvent);
                choicePanel.AddButton(button);
            }
            choicePanel.Open(defaultSelectIndex);
        }
    }
}
