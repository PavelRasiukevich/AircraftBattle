using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Assets.Scripts.Audio
{

    [Serializable]
    public sealed class SoundBank
    {
        private SoundBank() { }

        public List<Sound> _sounds;
        [SerializeField] private Pathes _assetSearchSettings;

        private string[] _assetGuids;
        private List<string> _previousGuids;

        public List<Sound> Sounds => _sounds;

        public List<string> PreviousGuids { get => _previousGuids; set => _previousGuids = value; }

        private void InitNames()
        {
            foreach (var sound in Sounds)
                sound.Name = sound.Clip.name;
        }


        #region Editor

        private string[] GetDirectories(string path)
            => Directory.GetDirectories(path);

        private string[] GetAssetsGUIDs()
            => AssetDatabase.FindAssets($"t:{AssetTypeFilter.AudioClip}", GetDirectories(_assetSearchSettings.AudioAssetsPath));

        public void AutofillList(AudioSetup setup)
        {

            Sounds.Clear();

            _assetGuids = GetAssetsGUIDs();

            for (int i = 0; i < _assetGuids.Length; i++)
            {
                Sound s = new Sound();

                Sounds.Add(s);

                if (_previousGuids != null)
                    if (_previousGuids.Contains(_assetGuids[i]))
                        LoadValues(setup, s, _assetGuids[i]);


                //находим клип из папки с ассетами
                //как еще 1 вариант можно попробовать
                //сериализовать файл и грузить его из JSON
                var path = AssetDatabase.GUIDToAssetPath(_assetGuids[i]);
                Sounds[i].Clip = AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip)) as AudioClip;
                InitNames();
            }

            _previousGuids = _assetGuids.ToList();

            setup.Values = _sounds.ToArray();
            setup.Keys = _assetGuids;

            Data.Set("AudioSetup", setup);

        }

        private void LoadValues(AudioSetup setup, Sound sound, string guid)
        {


            var settings = sound.Settings;

            var values = setup.Values.ToList();
            var keys = setup.Keys.ToList();

            if ((values == null || values.Count == 0)) return;

            var savedSound = values[keys.FindIndex(x => x.Contains(guid))];

            settings.Loop = savedSound.Settings.Loop;
            settings.Pitch = savedSound.Settings.Pitch;
            settings.PlayOnAwake = savedSound.Settings.PlayOnAwake;
            settings.Volume = savedSound.Settings.Volume;

            sound.Settings = settings;
        }

        #endregion


    }
}