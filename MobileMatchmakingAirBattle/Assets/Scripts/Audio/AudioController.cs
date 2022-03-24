using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Assets.Scripts.Audio
{
    [Serializable]
    public class AudioSetup
    {
        [SerializeField] public string[] Keys;
        [SerializeField] public Sound[] Values;

        public AudioSetup()
        {
            Values = new Sound[AssetDatabase.FindAssets($"t:{AssetTypeFilter.AudioClip}").Length];
        }
    }

    public class AudioController : MonoBehaviour
    {
        [SerializeField] private SoundBank _soundBank;

        private string _currentName;
        private AudioClip _clipToPlay;
        private AudioSource _source;
        private ClipGetter _clipGetter;
        private Sound _sound;
        private AudioSetup _audioSetup;

        public static AudioController Instance { get; private set; }

        #region UNITY

        private void Awake()
        {
            _clipGetter = new ClipGetter();

            {
                if (Instance)
                    throw new System.Exception("Instance not null");

                Instance = this;
            }


            if (Data.IsExists("AudioSetup"))
                _audioSetup = Data.Get<AudioSetup>("AudioSetup");
            else
                _audioSetup = new AudioSetup();

        }

        private void OnValidate()
        {
            Debug.Log("OnValidate");

            _audioSetup.Values = _soundBank.Sounds.ToArray();
            Data.Set("AudioSetup", _audioSetup);
        }

        #endregion 

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
        private void PerformAction() => _soundBank.AutofillList(_audioSetup);
    }

    public class ClipGetter
    {

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