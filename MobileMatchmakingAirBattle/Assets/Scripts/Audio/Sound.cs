using System;
using UnityEngine;

namespace Assets.Scripts.Audio
{

    public enum SoundType
    {
        Attack,
        Move,
        Crash
    }

    public enum SoundName
    {
        DefaultShot,
        FreezerShot,
        PlasmaShot,
        BulletImpact,
        PlaneImpact,
        PlaneMove
    }

    [Serializable]
    public class Sound
    {
        [SerializeField] private string _name;
        [SerializeField] private AudioClip _clip;
        [SerializeField] private SoundSettings _soundSettings;

        public string Name { get => _name; set => _name = value; }
        public AudioClip Clip { get => _clip; set => _clip = value; }

        public bool PlayOnAwake { get; set; } = false;
        public SoundSettings Settings => _soundSettings;
    }

    [Serializable]
    public struct SoundSettings
    {
        [Range(0, 1)] public float Volume;
        [Range(-3, 3)] public float Pitch;
        public bool PlayOnAwake;
        public bool Loop;
    }
}