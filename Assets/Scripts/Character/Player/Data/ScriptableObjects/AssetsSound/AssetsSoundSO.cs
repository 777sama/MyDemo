using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    [CreateAssetMenu(fileName = "Sound", menuName = "Custom/Assets/Sound")]
    public class AssetsSoundSO : ScriptableObject
    {
        [System.Serializable]
        private class Sounds
        {
            public SoundType soundType;
            public AudioClip[] audioClips;
        }

        [SerializeField] private List<Sounds> configSound = new List<Sounds>();


        public AudioClip GetAudioClip(SoundType soundType)
        {
            if(configSound.Count == 0) return null;
            
            switch (soundType)
            {
                case SoundType.ATK:
                    return configSound[0].audioClips[Random.Range(0, configSound[0].audioClips.Length)];
                    

                case SoundType.HIT:
                    return configSound[1].audioClips[Random.Range(0, configSound[1].audioClips.Length)];
                    

                case SoundType.BLOCK:
                    return configSound[2].audioClips[Random.Range(0, configSound[2].audioClips.Length)];
                    ;

                case SoundType.FOOT:
                    return configSound[3].audioClips[Random.Range(0, configSound[3].audioClips.Length)];
            }
            return null;

        }
    }
}
