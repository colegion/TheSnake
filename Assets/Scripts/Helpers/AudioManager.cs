using System.Collections.Generic;
using Scriptables;
using UnityEngine;

namespace Helpers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private List<AudioConfig> audios;
        
        private static AudioManager _instance;

        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("AudioManager").AddComponent<AudioManager>();
                }
                return _instance;
            }
        }

        public void PlayClip(AudioType type)
        {
            var clip = audios.Find(x => x.audioType == type).audioClip;
            if (clip == null) return;
            source.PlayOneShot(clip);
        }

    }

    public enum AudioType
    {
        IncreaseLevel,
        DecreaseLevel,
        Fail,
        Success,
    }
}
