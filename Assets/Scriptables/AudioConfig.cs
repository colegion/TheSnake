using UnityEngine;
using AudioType = Helpers.AudioType;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "NewAudioConfig", menuName = "Scriptable Objects/Audio Config")]
    public class AudioConfig : ScriptableObject
    {
        public AudioType audioType;
        public AudioClip audioClip;
    }
}
