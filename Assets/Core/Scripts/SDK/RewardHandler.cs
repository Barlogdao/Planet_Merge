using PlanetMerge.Services.Pause;
using PlanetMerge.Systems;
using System;
using UnityEngine;

namespace PlanetMerge.SDK.Yandex
{
    public class RewardHandler : MonoBehaviour
    {
        [SerializeField] private int _rewardEnergyAmount = 5;

        private EnergyLimitHandler _energyLimitHandler;
        private PauseService _pauseService;

        public void Initialize(EnergyLimitHandler energyLimitHandler, PauseService pauseService)
        {
            _energyLimitHandler = energyLimitHandler;
            _pauseService = pauseService;
        }

        public void AddReward(Action OnSuccessCallback, Action OnFailCallback)
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
                OnSuccessCallback();
            }

            void OnCloseCallback()
            {
                _pauseService.Unpause();

            }

            void OnErrorCallback(string error)
            {
                OnFailCallback();
            }
        }
    }
}