using System;
using UnityEngine;

namespace UI.Screens.Battle.BattleScreen
{
    public class BattleScreenInputHandler : MonoBehaviour
    {
        public Actions Actions { get; private set; }

        private void Awake()
        {
            Actions = new Actions();
        }

        private void OnEnable()
        {
            Actions.Enable();
        }
        private void OnDisable()
        {
            Actions.Disable();
        }
    }
}