using GGG.Tool.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace zzz
{
    //闪避后的子弹时间，攻击手感，QTE，切人格挡,摄像机
    public class FightingEventManager : Singleton<FightingEventManager>
    {
        public float SlowTimeFactor;
        public float SlowTimeLength;
        public Player player;

        public GameObject globaVolum;
        public PPController ppController;

        public Cinemachine.CinemachineCollisionImpulseSource impulseSource;

        public bool QTE;

        public bool gameOver;

        public GameObject QTETimeLine;


        protected override void Awake()
        {
            base.Awake();
            gameOver = false;
            ppController = globaVolum.GetComponent<PPController>();

            Debug.Log(GameManager.MainInstance.currentTask.name);
        }

        //private void Awake()
        //{
        //    gameOver = false;
        //    ppController = globaVolum.GetComponent<PPController>();
        //}

        public void TriggerFreezeOnDash()
        {
            
            StartCoroutine(FreezeFrameOnDash());
        }

        public void TriggerFreezeOnDefense()
        {
            StartCoroutine(FreezeFameOnDefense());
            
        }

        public void TriggerShakeCamera(float force)
        {
            impulseSource.GenerateImpulseWithForce(force);
        }

        public void QTEEvent()
        {
            StartCoroutine(StartQTE()); 
        }

        public void GameOver()
        {
            

            GameManager.MainInstance.currentTask.currentState = TaskState.Finished;
            UIManager.MainInstance.OpenPanel(UIManager.UIConst.GameOverPanel);
            Cursor.lockState = CursorLockMode.None;

        }


        IEnumerator FreezeFrameOnDash()
        {
            ppController.SetSpeedAndMaxValue(1f, 0.45f);
            yield return new WaitForSecondsRealtime(0.2f);
            Time.timeScale = SlowTimeFactor;
            
            
            yield return new WaitForSecondsRealtime(SlowTimeLength);
            Time.timeScale = 1f;
            
        }

        IEnumerator FreezeFameOnDefense()
        {
            ppController.SetSpeedAndMaxValue(1f, 0.45f);
            ppController.shouldDO = true;
            Time.timeScale = 0.1f;
            yield return new WaitForSecondsRealtime(0.3f);
            Time.timeScale = 1f;
        }

        IEnumerator StartQTE()
        {
            
            QTETimeLine.SetActive(true);
            Time.timeScale = 0.3f;
            yield return new WaitForSecondsRealtime(1f);
            Time.timeScale = 1f;
        }

    }
}
