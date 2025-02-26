using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    public enum SoundType
    {
        ATK,
        HIT,
        BLOCK,
        FOOT
    }
 
    public class PoolItemSound : PoolItemBase
    {
        private AudioSource audioSource;
        [SerializeField] private SoundType soundType;    
        [SerializeField] private AssetsSoundSO soundSO;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public override void Spawn()
        {
            PlaySound();
        }

        public override void Recycl()
        {
            
        }

        private void PlaySound()
        {
            
            audioSource.clip = soundSO.GetAudioClip(soundType);
            audioSource.Play();
            StartRecycl();
        }

        private void StartRecycl()
        {
            TimerManager.MainInstance.TryGetOneTimer(1.2f, DisableSelf);
        }

        private void DisableSelf()
        {
            audioSource.Stop();
            this.gameObject.SetActive(false);
        }
    }
}
