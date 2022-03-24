using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Audio
{

    [Serializable]
    public sealed class SoundBank
    {
        private SoundBank() { }

        [SerializeField] private List<Sound> _sounds;
        [SerializeField] private Pathes _assetSearchSettings;

        private string[] _assetGuids;

        public List<Sound> Sounds => _sounds;

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

        public void AutofillList()
        {

            Sounds.Clear();

            _assetGuids = GetAssetsGUIDs();

            for (int i = 0; i < _assetGuids.Length; i++)
            {
                Sounds.Add(new Sound());
                var path = AssetDatabase.GUIDToAssetPath(_assetGuids[i]);
                Sounds[i].Clip = AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip)) as AudioClip;
                InitNames();
            }
        }

        #endregion
    }
}