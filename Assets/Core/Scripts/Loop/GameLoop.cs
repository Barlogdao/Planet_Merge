using System;
using Cysharp.Threading.Tasks;
using PlanetMerge.Loop.States;
using PlanetMerge.SDK.Yandex.Advertising;
using PlanetMerge.Systems.Events;
using UnityEngine;

namespace PlanetMerge.Loop
{
    public class GameLoop : MonoBehaviour
    {
        private GameEventMediator _gameEventMediator;
        private StartLevelState _startLevelState;
        private EndLevelState _endLevelState;
        private PrepareLevelState _prepareLevelState;
        private AdvertisingService _advertisingService;

        public event Action LevelPrepared;

        public event Action LevelStarted;

        public event Action LevelResumed;

        private void OnDestroy()
        {
            _gameEventMediator.GameWon -= OnGameWon;
            _gameEventMediator.GameLost -= OnGameLost;

            _gameEventMediator.NextLevelSelected -= OnNextLevelSelected;
            _gameEventMediator.RestartLevelSelected -= OnRestartLevelSelected;
            _gameEventMediator.RewardSelected -= OnRewardSelected;
        }

        public void Initialize(GameEventMediator gameEventMediator, LevelStates levelStates, AdvertisingService advertisingService)
        {
            _gameEventMediator = gameEventMediator;
            _prepareLevelState = levelStates.PrepareLevelState;
            _startLevelState = levelStates.StartLevelState;
            _endLevelState = levelStates.EndLevelState;
            _advertisingService = advertisingService;

            _gameEventMediator.GameWon += OnGameWon;
            _gameEventMediator.GameLost += OnGameLost;

            _gameEventMediator.NextLevelSelected += OnNextLevelSelected;
            _gameEventMediator.RestartLevelSelected += OnRestartLevelSelected;
            _gameEventMediator.RewardSelected += OnRewardSelected;
        }

        public void Run()
        {
            PrepareLevel();
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.YandexGamesSdk.GameReady();
#endif
        }

        private void PrepareLevel()
        {
            _prepareLevelState.PrepareLevel();
            LevelPrepared?.Invoke();

            StartLevelAsync().Forget();
        }

        private async UniTaskVoid StartLevelAsync()
        {
            await _startLevelState.StartLevelAsync();
            LevelStarted?.Invoke();
        }

        private void OnGameWon()
        {
            _endLevelState.Win().Forget();
        }

        private void OnGameLost()
        {
            _endLevelState.Loose();
        }

        private void OnNextLevelSelected()
        {
            _advertisingService.ShowInterstitialAd(PrepareLevel);
        }

        private void OnRestartLevelSelected()
        {
            _advertisingService.ShowInterstitialAd(PrepareLevel);
        }

        private void OnRewardSelected()
        {
            _advertisingService.ShowRewardAd(ResumeLevel, PrepareLevel);
        }

        private void ResumeLevel()
        {
            _startLevelState.ResumeLevel();
            LevelResumed?.Invoke();
        }
    }
}