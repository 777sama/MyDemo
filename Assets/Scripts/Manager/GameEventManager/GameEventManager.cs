using GGG.Tool;
using GGG.Tool.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class GameEventManager : SingletonNonMono<GameEventManager>
    {
        private interface IEventHelp
        {

        }

        private class EventHelp : IEventHelp
        {
            private event Action action;

            public EventHelp(Action action)
            {
                this.action = action;
            }

            //�����¼���ע�ắ��
            public void AddCall(Action act)
            {
                action += act;
            }

            //�����¼�
            public void Call()
            {
                action?.Invoke();
            }

            //�Ƴ��¼�
            public void Remove(Action act) 
            {
                action -= act;
            }
        }

        private class EventHelp<T> : IEventHelp
        {
            private event Action<T> action;

            public EventHelp(Action<T> action)
            {
                this.action = action;
            }

            //�����¼���ע�ắ��
            public void AddCall(Action<T> act)
            {
                action += act;
            }

            //�����¼�
            public void Call(T value)
            {
                action?.Invoke(value);
            }

            //�Ƴ��¼�
            public void Remove(Action<T> act)
            {
                action -= act;
            }
        }

        private class EventHelp<T1,T2> : IEventHelp
        {
            private event Action<T1, T2> action;

            public EventHelp(Action<T1, T2> action)
            {
                this.action = action;
            }

            //�����¼���ע�ắ��
            public void AddCall(Action<T1, T2> act)
            {
                action += act;
            }

            //�����¼�
            public void Call(T1 value1,T2 value2)
            {
                action?.Invoke(value1,value2);
            }

            //�Ƴ��¼�
            public void Remove(Action<T1, T2> act)
            {
                action -= act;
            }
        } 
        
        private class EventHelp<T1, T2, T3, T4> : IEventHelp
        {
            private event Action<T1, T2, T3, T4> action;

            public EventHelp(Action<T1, T2, T3, T4> action)
            {
                this.action = action;
            }

            //�����¼���ע�ắ��
            public void AddCall(Action<T1, T2, T3, T4> act)
            {
                action += act;
            }

            //�����¼�
            public void Call(T1 value1,T2 value2,T3 value3,T4 value4)
            {
                action?.Invoke(value1,value2,value3,value4);
            }

            //�Ƴ��¼�
            public void Remove(Action<T1, T2, T3, T4> act)
            {
                action -= act;
            }
        }

        private Dictionary<string,IEventHelp> eventCenter = new Dictionary<string,IEventHelp>();

        //����¼�����
        public void  AddEventListening(string eventName, Action act)
        {
            if(eventCenter.TryGetValue(eventName, out var e))
            {
                (e as EventHelp)?.AddCall(act);
            }
            else
            {
                eventCenter.Add(eventName, new EventHelp(act));
            }
            
        }

        public void AddEventListening<T>(string eventName, Action<T> act)
        {
            if (eventCenter.TryGetValue(eventName, out var e))
            {
                (e as EventHelp<T>)?.AddCall(act);
            }
            else
            {
                eventCenter.Add(eventName, new EventHelp<T>(act));
            }

        }
        public void AddEventListening<T1,T2>(string eventName, Action<T1, T2> act)
        {
            if (eventCenter.TryGetValue(eventName, out var e))
            {
                (e as EventHelp<T1, T2>)?.AddCall(act);
            }
            else
            {
                eventCenter.Add(eventName, new EventHelp<T1, T2>(act));
            }

        }

        public void AddEventListening<T1, T2, T3, T4>(string eventName, Action<T1, T2, T3, T4> act)
        {
            if (eventCenter.TryGetValue(eventName, out var e))
            {
                (e as EventHelp<T1, T2, T3, T4>)?.AddCall(act);
            }
            else
            {
                eventCenter.Add(eventName, new EventHelp<T1, T2, T3, T4>(act));
            }

        }


        //�����¼�
        public void CallEvent(string eventName)
        {
            if (eventCenter.TryGetValue(eventName, out var e))
            {
                (e as EventHelp)?.Call();
            }
            else
            {
                DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷�ִ��");
            }
        }

        public void CallEvent<T>(string eventName,T value)
        {
            if (eventCenter.TryGetValue(eventName, out var e))
            {
                (e as EventHelp<T>)?.Call(value);
            }
            else
            {
                DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷�ִ��");
            }
        }

        public void CallEvent<T1,T2>(string eventName,T1 value1,T2 value2)
        {
            if (eventCenter.TryGetValue(eventName, out var e))
            {
                (e as EventHelp<T1, T2>)?.Call(value1,value2);
            }
            else
            {
                DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷�ִ��");
            }
        }

        public void CallEvent<T1, T2, T3, T4>(string eventName,T1 value1,T2 value2,T3 value3,T4 value4)
        {
            if (eventCenter.TryGetValue(eventName, out var e))
            {
                (e as EventHelp<T1, T2, T3, T4>)?.Call(value1,value2,value3,value4);
            }
            else
            {
                DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷�ִ��");
            }
        }


        //�Ƴ��¼�
        public void RemoveEvent(string eventName,Action action)
        {
            if (eventCenter.TryGetValue(eventName, out var e))
            {
                (e as EventHelp)?.Remove(action);
            }
            else
            {
                DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷��Ƴ�");
            }
        }

        public void RemoveEvent<T>(string eventName, Action<T> action)
        {
            if (eventCenter.TryGetValue(eventName, out var e))
            {
                (e as EventHelp<T>)?.Remove(action);
            }
            else
            {
                DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷��Ƴ�");
            }
        }

        public void RemoveEvent<T1, T2>(string eventName, Action<T1, T2> action)
        {
            if (eventCenter.TryGetValue(eventName, out var e))
            {
                (e as EventHelp<T1, T2>)?.Remove(action);
            }
            else
            {
                DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷��Ƴ�");
            }
        }

        public void RemoveEvent<T1, T2, T3, T4>(string eventName, Action<T1, T2, T3, T4> action)
        {
            if (eventCenter.TryGetValue(eventName, out var e))
            {
                (e as EventHelp<T1, T2, T3, T4>)?.Remove(action);
            }
            else
            {
                DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷��Ƴ�");
            }
        }
    }
}
