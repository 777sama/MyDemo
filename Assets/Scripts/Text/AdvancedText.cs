using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace zzz
{
    public class RubyData
    {
        public RubyData(int startIndex,string content)
        {
            StartIndex = startIndex;
            RubyContent = content;
            EndIndex = StartIndex;
        }
        public int StartIndex { get; }
        public int EndIndex { get; set; }
        public string RubyContent { get;}
    }

    public class AdvancedTextPreprocessor : ITextPreprocessor
    {
        public Dictionary<int, float> IntervalDictionary;
        public List<RubyData> RubyList;

        public AdvancedTextPreprocessor() 
        {
            IntervalDictionary = new Dictionary<int, float>();
            RubyList = new List<RubyData>();
        }

        public bool TryGetRubyStartFrom(int index,out RubyData data)
        {
            data = new RubyData(0, "");
            foreach(var item in RubyList)
            {
                if(item.StartIndex == index)
                {
                    data = item;
                    return true;
                }
            }
            return false;
        }

        public string PreprocessText(string text)
        {
            IntervalDictionary.Clear();

            string processingText = text;

            string pattern = "<.*?>";
            Match match = Regex.Match(processingText, pattern);
            while (match.Success)
            {
                string label = match.Value.Substring(1,match.Length - 2);
                if (float.TryParse(label, out float result))
                {
                    IntervalDictionary[match.Index - 1] = result;
                }
                else if (Regex.IsMatch(label, "^r=.+"))
                {
                    RubyList.Add(new RubyData(match.Index, label.Substring(2)));
                }
                else if (label == "/r")
                {
                    if (RubyList.Count > 0)
                    {
                        RubyList[RubyList.Count - 1].EndIndex = match.Index - 1;
                    }
                }
                processingText = processingText.Remove(match.Index, match.Length);

                if (Regex.IsMatch(label, "^sprite=.+"))
                {
                    processingText = processingText.Insert(match.Index, "*");
                }
               
             
                

                match = Regex.Match(processingText, pattern);
            }

            // *    前一个字符出现零次或多次
            // +    前一个字符出现一次或多次
            // ?    前一个字符出现零次或一次
            // .    代表任意字符
            processingText = text;
            pattern = @"(<(\d+)(\.\d+)?>)|(</r>)|(<r=.*?>)";

            processingText = Regex.Replace(processingText, pattern, "");
            return processingText;
        }
    }
    public class AdvancedText : TextMeshProUGUI
    {
        public enum DisplayeType
        {
            Default,
            Fading,
            Typing
        }

        private Widget widget;
        private int typingIndex;
        private float defaultInterval = 0.06f;

        private Coroutine typingCoroutine;
        public Action OnFinished;



        protected override void Awake()
        {
            base.Awake();
            widget = GetComponent<Widget>();
        }


        public void Initialise()
        {
            SetText("");
            ClearRubyText();
        }

        public void Disappear(float duration = 0.2f) 
        {
            widget.Fade(0, duration, null);
        }

        public AdvancedText() 
        {
            textPreprocessor = new AdvancedTextPreprocessor();
        }

        private AdvancedTextPreprocessor SelfPreprocessor => (AdvancedTextPreprocessor) textPreprocessor;

        private void SetRubyText(RubyData data)
        {
            GameObject pfb = Resources.Load<GameObject>("Prefab/UI/Text/RubyText");
            GameObject ruby = Instantiate(pfb, transform);
            ruby.GetComponent<TextMeshProUGUI>().SetText(data.RubyContent);
            ruby.GetComponent<TextMeshProUGUI>().color = textInfo.characterInfo[data.StartIndex].color;
            ruby.transform.localPosition = (textInfo.characterInfo[data.StartIndex].topLeft + textInfo.characterInfo[data.EndIndex].topRight) / 2;
        }

        private void SetAllRubyText()
        {
            foreach (var item in SelfPreprocessor.RubyList)
            {
                SetRubyText(item);
            }
        }

        private void ClearRubyText()
        {
            foreach(var item in GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (item != this)
                {
                    
                    Destroy(item.gameObject);

                }
            }
            SelfPreprocessor.RubyList.Clear();
        }

        public void QuickShowRemaining()
        {
            if(typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                for(;typingIndex < m_characterCount; typingIndex++)
                {
                    StartCoroutine(FadeInCharacter(typingIndex));
                }
            }
        }


        public IEnumerator SetText(string content, DisplayeType type, float fadingDuration = 0.2f)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            ClearRubyText();
            SetText(content);
            yield return null;

            switch (type)
            {
                case DisplayeType.Default:
                    widget.RenderOpacity = 1;
                    SetAllRubyText();
                    OnFinished?.Invoke();
                    break;
                case DisplayeType.Fading:
                    widget.Fade(1,fadingDuration,OnFinished.Invoke);
                    SetAllRubyText();
                    break;
                case DisplayeType.Typing:
                    widget.Fade(1, fadingDuration, null);
                    typingCoroutine = StartCoroutine(Typing());
                    break;
            }
        }

        //newAlpha 范围0——255
        private void SetSingleCharacterAlpha(int index, byte newAlpha)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[index];
            if(!charInfo.isVisible)
            {
                return;
            }
            int matIndex = charInfo.materialReferenceIndex;
            int verIndex = charInfo.vertexIndex;

            for (int i = 0; i < 4; i++)
            {
                textInfo.meshInfo[matIndex].colors32[verIndex + i].a = newAlpha;
            }
            UpdateVertexData();
        }



        IEnumerator Typing()
        {
            ForceMeshUpdate();
            for (int i = 0; i < m_characterCount; i++)
            {

                SetSingleCharacterAlpha(i, 0);
            }
            typingIndex = 0;
            while(typingIndex < m_characterCount)
            {
                StartCoroutine(FadeInCharacter(typingIndex));

                if(SelfPreprocessor.IntervalDictionary.TryGetValue(typingIndex, out float result))
                {
                    yield return new WaitForSecondsRealtime(result);
                }
                else
                {
                    yield return new WaitForSecondsRealtime(defaultInterval);
                }
                typingIndex++;
            }

            OnFinished.Invoke();
        }

        IEnumerator FadeInCharacter(int index,float duration = 0.2f)
        {
            if(SelfPreprocessor.TryGetRubyStartFrom(index, out RubyData data))
            {
                
                SetRubyText(data);
            }

            if (duration <= 0)
            {
                SetSingleCharacterAlpha(index, 255);
            }
            else
            {
                float timer = 0;
                while (timer < duration)
                {
                    timer = Mathf.Min(duration, timer + Time.unscaledDeltaTime);
                    SetSingleCharacterAlpha(index, (byte)(255 * timer / duration));
                    yield return null;
                }
            }
        }
    }
}
