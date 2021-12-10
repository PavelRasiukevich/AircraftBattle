using System;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    [Serializable]
    public class AircraftDataModel
    {
        [SerializeField] Speed _speed;
        [SerializeField] private int _hp;
        [SerializeField] private int _currentHp;

        public Speed Speed => _speed;

        public int Hp => _hp;

        public int CurrentHp { get => _currentHp; set => _currentHp = value; }
    }

    [Serializable]
    public struct Speed
    {
        public float MoveSpeed;
        public float RotationSpeed;
    }
}