using Assets.Scripts.SriptableObjects;
using Photon.Realtime;
using System;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    [Serializable]
    public class ProjectileDataModel
    {
        [SerializeField] private ProjectileDataScriptable _dataModel;

        public int Damage { get; set; }
        public float Speed { get; set; }
        public Player Owner { get; set; }
        public float Lag { get; set; }

        public void Init()
        {
            Damage = _dataModel.Damage;
            Speed = _dataModel.Speed;
        }
    }
}