using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    //计时器工作状态
    public enum TimerState
    {
        NOTWORK,//没有工作
        WORKING,//工作中
        DONE//工作完成
    }
    public class GameTimer 
    {
        private float startTime;
        private Action task;
        private bool isStopTimer;
        private TimerState timerState;

        public GameTimer()
        {
            ResetTimer();
        }

        public void StartTimer(float time,Action task)
        {
            startTime = time;
            this.task = task;
            isStopTimer = false;
            timerState = TimerState.WORKING;
        }

        public void UpdateTimer()
        {
            if (isStopTimer) return;

            startTime -= Time.unscaledDeltaTime;
            if(startTime < 0)
            {
                task?.Invoke();
                timerState = TimerState.DONE;
                isStopTimer = true;
            }
        }

        public TimerState GetTimerState() => timerState;


        public void ResetTimer()
        {
            startTime = 0f;
            task = null;
            isStopTimer = true;
            timerState = TimerState.NOTWORK;
        }
    }
}
