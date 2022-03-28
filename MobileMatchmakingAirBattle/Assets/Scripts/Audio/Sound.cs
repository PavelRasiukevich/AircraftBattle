using System;
using UnityEngine;

namespace Assets.Scripts.Audio
{

    [Serializable]
    public class Sound
    {
        [SerializeField] private string _name;
        [SerializeField] private AudioClip _clip;
        [SerializeField] private SoundSettings _soundSettings;

        public string Name { get => _name; set => _name = value; }
        public AudioClip Clip { get => _clip; set => _clip = value; }

        public SoundSettings Settings { get => _soundSettings; set => _soundSettings = value; }
    }

    [Serializable]
    public struct SoundSettings
    {
        [Range(0, 1)] public float Volume;
        [Range(-3, 3)] public float Pitch;
        public bool PlayOnAwake;
        public bool Loop;

        public SoundSettings(SoundSettingsType type)
        {
            Volume = 1;
            Pitch = 1;
            PlayOnAwake = false;
            Loop = false;
        }
    }

    public enum SoundSettingsType
    {
        None,
        Default,
        BassBusted
    }
}