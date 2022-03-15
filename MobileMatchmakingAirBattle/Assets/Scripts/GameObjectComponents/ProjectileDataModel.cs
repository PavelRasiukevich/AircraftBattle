using System;
using Assets.Scripts.Projectiles;
using Photon.Realtime;
using UnityEngine;

namespace Assets.Scripts.SriptableObjects
{
    [Serializable]
    public class ProjectileDataModel
    {
        [SerializeField] private ProjectileDataScriptable _data;

        public ProjectileDataScriptable ScriptableData => _data;
        public Player Owner { get; set; }
        public float Lag { get; set; }
    }
}