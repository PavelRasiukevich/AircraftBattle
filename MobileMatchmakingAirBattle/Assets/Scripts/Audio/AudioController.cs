using Assets.Scripts.Projectiles;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private SoundBank _soundBank;

        private string _currentName;
        private AudioClip _clipToPlay;
        private AudioSource _source;
        private ClipGetter _clipGetter;
        private Sound _sound;

        private void Awake()
        {
            _clipGetter = new ClipGetter();
        }

        public void PlaySound(string clipName, GameObject sender)
        {

            if (string.IsNullOrEmpty(_currentName) || !_currentName.Equals(clipName))
            {
                _currentName = clipName;
                _sound = _clipGetter.GetSoundClipToPlayByName(clipName, _soundBank.Sounds);
                _clipToPlay = _sound.Clip;
            }

            _source = sender.AddComponent<AudioSource>();
            _source.clip = _clipToPlay;

            _source.playOnAwake = _sound.Settings.PlayOnAwake;
            _source.volume = _sound.Settings.Volume;
            _source.pitch = _sound.Settings.Pitch;
            _source.loop = _sound.Settings.Loop;

            _source.Play();

        }

        [ContextMenu("Autofill SoundBank")]
        private void PerformAction() => _soundBank.AutofillList();
    }

    public class ClipGetter
    {
        public List<Sound> Sounds { get; set; }

        public Sound GetSoundClipToPlayByName(string name, List<Sound> sounds)
        {

            int clipNameLength = name.Length;

            for (int i = 0; i < sounds.Count; i++)
            {
                string simplifiedName = sounds[i].Name.Substring(0, clipNameLength);

                if (simplifiedName.Equals(name)) return sounds[i];
            }

            return null;
        }
    }
}