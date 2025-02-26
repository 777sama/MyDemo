using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using GGG.Tool.Singleton;
using UnityEngine.SceneManagement;

namespace zzz
{
    public class GameManager : Singleton<GameManager>
    {
        public float loadValue;
        public bool applyChangeScene = false;
        public TaskDataSO currentTask;


        protected override void Awake()
        {

            
            Cursor.lockState = CursorLockMode.Locked;
            base.Awake();
            List<TaskDataSO> tasks = TaskLocalData.MainInstance.LoadTask();
            DontDestroyOnLoad(gameObject);
        }


        private void Start()
        {
            
        }

        //³¡¾°ÇÐ»»
        public void LoadSceneAsync(int id)
        {
            StartCoroutine(SceneLoadAsync(id));
        }

        




        IEnumerator SceneLoadAsync(int id)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(id);
            asyncOperation.allowSceneActivation = false;
            while (!asyncOperation.isDone) 
            {
                loadValue = asyncOperation.progress;
                yield return null;
                if(applyChangeScene)
                {
                    asyncOperation.allowSceneActivation = true;
                    applyChangeScene = false;
                }
            }
            
           
        }
    }



    public class SceneConst
    {
        public const int CityScene = 0;
        public const int FightScene = 1;
    }
}
