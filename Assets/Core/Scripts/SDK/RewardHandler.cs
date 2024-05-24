using PlanetMerge.Services.Pause;
using PlanetMerge.Systems;
using System;
using UnityEngine;

namespace PlanetMerge.SDK.Yandex
{
    public class RewardHandler : MonoBehaviour
    {
        [SerializeField] private int _bonusPlanetAmount = 5;

        private PlanetLimitHandler _planetLimitHandler;
        private PauseService _pauseService;

        public void Initialize(PlanetLimitHandler planetLimitHandler, PauseService pauseService)
        {
            _planetLimitHandler = planetLimitHandler;
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
                _planetLimitHandler.SetLimit(_bonusPlanetAmount);
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