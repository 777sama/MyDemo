using System;
using System.Collections;
using System.Collections.Generic;
using GGG.Tool;
using GGG.Tool.Singleton;
using Unity.VisualScripting;
using UnityEngine;

namespace zzz
{
    public class TimerManager : GGG.Tool.Singleton.Singleton<TimerManager>
    {
        [SerializeField] private int initMaxTimerCount;
        
        private Queue<GameTimer> notWorkTimer = new Queue<GameTimer>();
        private List<GameTimer> workingTimer = new List<GameTimer>();


        protected override void Awake()
        {
            base.Awake();
            
        }

        private void Start()
        {
            InitTimerManager();
        }

        private void Update()
        {
            UpdateWorkingTimer();
        }

        private void InitTimerManager()
        {
            for (int i = 0; i < initMaxTimerCount; i++)
            {
                CreatTimer();
            }
        }

        public void CreatTimer()
        {
            var timer = new GameTimer();

            notWorkTimer.Enqueue(timer);
        }

        public void TryGetOneTimer(float time,Action task)
        {
            if(notWorkTimer.Count == 0)
            {
                CreatTimer();
                var timer = notWorkTimer.Dequeue();
                timer.StartTimer(time, task);
                workingTimer.Add(timer);
            }
            else
            {
                var timer = notWorkTimer.Dequeue();
                timer.StartTimer(time,task);
                workingTimer.Add(timer);
            }
        }

        public void UpdateWorkingTimer()
        {
            if (workingTimer.Count == 0) return;
            for(var i = 0; i < workingTimer.Count; i++)
            {
                if(workingTimer[i].GetTimerState() == TimerState.WORKING)
                {
                    workingTimer[i].UpdateTimer();
                }
                else
                {
                    notWorkTimer.Enqueue(workingTimer[i]);
                    workingTimer[i].ResetTimer();
                    workingTimer.Remove(workingTimer[i]);
                }
            }
        }
    }
}
