using Cinemachine;
using GGG.Tool.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public class SwitchCharacter : Singleton<SwitchCharacter>
    {
        public List<GameObject> characterList = new List<GameObject>();

        public int index = 0;
        public int currentIndex;

        public bool isSwitched = true;//ÅÐ¶ÏÊÇ·ñ¾­¹ýÇÐ»»

        public float switchOutCharacterTime;

        public Player currentPlayer;
        public Player lastPlayer;

        public float nomalSpawnDistance;
        public float defenseSpawnDistance;


        public float SwitchColdTime;

        public Transform attacter;

        public CinemachineVirtualCamera virtualCamera;
        public CinemachineVirtualCamera QTECamera;


        private void Awake()
        {
           
        }

        Coroutine switchOutCharacterTimerCoroutine;
        public void ChangeCharacter()
        {
            isSwitched = true;
            lastPlayer = characterList[index].GetComponent<Player>();

            UpdateIndex();

            if(lastPlayer.beAttacking)
            {
                DefentseSpawnPosition();
            }
            else
            {
                SetSpawnPosition();
            }
            

            characterList[index].SetActive(true);

            
            ChangeCameraLookAt();
            currentPlayer = characterList[index].GetComponent<Player>();
            currentPlayer.SetSwitchedState();
            //TimerManager.MainInstance.TryGetOneTimer(switchOutCharacterTime,SetActiveFalse);
            if (switchOutCharacterTimerCoroutine != null)
            {
                StopCoroutine(switchOutCharacterTimerCoroutine);
            }
            switchOutCharacterTimerCoroutine = StartCoroutine(CharacterActiveTimerCoroutine(switchOutCharacterTime));
        }

        IEnumerator CharacterActiveTimerCoroutine(float time)
        {
            
            yield return new WaitForSeconds(time);
            SetActiveFalse();
        }


        private void SetActiveFalse()
        {
            for (int i = 0; i < characterList.Count; i++)
            {
                if (characterList[i] != characterList[index]) 
                {
                    characterList[i].SetActive(false);
                }

            }
        }
        private void UpdateIndex()
        {
            currentIndex = index;
            index++;
            index %= characterList.Count;
        }

        private void SetEnemy()
        {
            currentPlayer.currentEnemy = lastPlayer.currentEnemy;
        }

        private void SetSpawnPosition()
        {
            characterList[index].transform.position = characterList[currentIndex].transform.position - characterList[currentIndex].transform.forward * nomalSpawnDistance - characterList[currentIndex].transform.right * 0.6f;
            characterList[index].transform.rotation = characterList[currentIndex].transform.rotation;
        }

        private void DefentseSpawnPosition()
        {
            characterList[index].transform.position = characterList[currentIndex].transform.position + attacter.transform.forward * defenseSpawnDistance;
            characterList[index].transform.LookAt(attacter.transform.position);
        }

        private void ChangeCameraLookAt()
        {
            virtualCamera.LookAt = characterList[index].transform.Find("CameraBasePoint");
            virtualCamera.Follow = characterList[index].transform.Find("CameraBasePoint");
            QTECamera.LookAt = characterList[index].transform.Find("CameraBasePoint");
            QTECamera.Follow = characterList[index].transform.Find("CameraBasePoint");
            QTECamera.transform.rotation = characterList[index].transform.rotation;
            virtualCamera.transform.rotation = characterList[index].transform.rotation;
        }

    }
}
