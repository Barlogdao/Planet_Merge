using PlanetMerge.Services.Pause;
using PlanetMerge.Systems;
using System;
using UnityEngine;

namespace PlanetMerge.SDK.Yandex
{
    public class RewardHandler : MonoBehaviour
    {
        [SerializeField] private int _rewardEnergyAmount = 5;

        private EnergyLimitController _energyLimitHandler;
        private PauseService _pauseService;

        public void Initialize(EnergyLimitController energyLimitHandler, PauseService pauseService)
        {
            _energyLimitHandler = energyLimitHandler;
            _pauseService = pauseService;
        }

        public void AddReward(Action OnSuccess, Action OnFail)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback, OnErrorCallback);
#else
            OnRewardedCallback();
#endif

            void OnOpenCallback()
            {
                _pauseService.Pause();
            }

            void OnRewardedCallback()
            {
                _energyLimitHandler.SetLimit(_rewardEnergyAmount);
                OnSuccess();
            }

            void OnCloseCallback()
            {
                _pauseService.Unpause();

            }

            void OnErrorCallback(string error)
            {
                OnFail();
            }
        }
    }
}