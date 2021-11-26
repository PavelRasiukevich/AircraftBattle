using System;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    [Serializable]
    public class AircraftDataModel
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _hp;
        [SerializeField] private int _currentHp;

        public float MoveSpeed => _moveSpeed;

        public int Hp => _hp;

        public int CurrentHp { get => _currentHp; set => _currentHp = value; }
    }
}