using System;
using System.Collections;
using Assets.Scripts.Core;
using Core;
using Interfaces.EventBus;
using UnityEngine;
using Utils.Enums;

namespace Managers.Gameplay
{
    public class BattleManager : MonoBehaviour, IDestroy
    {
        private Spawner Spawner { get; set; }

        #region UNITY

        void Awake()
        {
            Spawner = GetComponent<Spawner>();
        }

        void Start()
        {
            GameStart();
        }

        void OnEnable() => EventBus.AddListener<IDestroy>(this);

        void OnDisable() => EventBus.RemoveListener<IDestroy>(this);

        #endregion

        #region PRIVATE

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(3.0f);
            GameStart();
            StopCoroutine(nameof(Wait));
        }

        private void GameFail()
        {
            StartCoroutine(nameof(Wait));
            ScreenHolder.SetCurrentScreen(ScreenType.BattleFail).ShowScreen();
        }

        private void GameStart()
        {
            Spawner.Spawn();
            ScreenHolder.SetCurrentScreen(ScreenType.Battle).ShowScreen();
        }

        private void GameFinish()
        {
            ScreenHolder.SetCurrentScreen(ScreenType.BattleFinish).ShowScreen();
        }

        #endregion


        #region PUBLIC

        public void DestroyAircraft() => GameFail();

        #endregion
    }
}