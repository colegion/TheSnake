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
                    _instance = FindObjectOfType<AudioManager>();
                    if (_instance == null)
                    {
                        var newGameObject = new GameObject("AudioManager");
                        _instance = newGameObject.AddComponent<AudioManager>();
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void PlayClip(AudioType type)
        {
            var audioConfig = audios?.Find(x => x.audioType == type);
            if (audioConfig?.audioClip == null) return;
            source.PlayOneShot(audioConfig.audioClip);
        }
    }

    public enum AudioType
    {
        IncreaseLevel,
        Move,
        Fail,
        Turn,
    }
}