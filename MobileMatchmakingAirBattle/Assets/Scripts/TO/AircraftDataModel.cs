using System;
using UnityEngine;

namespace TO
{
    [Serializable]
    public class AircraftDataModel
    {
        [SerializeField] Speed _speed;
        [SerializeField] private int _hp;
        [SerializeField] private int _currentHp;
        [SerializeField] private float _reloadTime;

        public Transform RespawnPosition { get; set; }

        public Speed Speed => _speed;

        public int Hp => _hp;

        public int CurrentHp { get => _currentHp; set => _currentHp = value; }

        public bool IsControllable { get; set; }

        public float ReloadTime { get => _reloadTime; set => _reloadTime = value; }
    }

    [Serializable]
    public struct Speed
    {
        public float MoveSpeed;
        public float RotationSpeed;
    }
}