using PlanetMerge.Systems.Gameplay;
using PlanetMerge.Systems.Pause;
using System;

namespace PlanetMerge.SDK.Yandex.Advertising
{
    public class AdvertisingService
    {
        private readonly PauseService _pauseService;
        private readonly RewardHandler _rewardHandler;

        public AdvertisingService(PauseService pauseService, RewardHandler rewardHandler)
        {
            _pauseService = pauseService;
            _rewardHandler = rewardHandler;
        }

        public bool IsAdsPlaying { get; private set; } = false;

        public void ShowInterstitialAd(Action OnClose)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.InterstitialAd.Show(OnOpenCallback, OnCloseInterstitial);
#else
            OnCloseInterstitial(true);
#endif

            void OnCloseInterstitial(bool wasShown)
            {
                OnCloseCallback();
                OnClose();
            }
        }

        public void ShowRewardAd(Action OnSuccess, Action OnFail)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback, OnErrorCallback);
#else
            OnRewardedCallback();
#endif

            void OnRewardedCallback()
            {
                _rewardHandler.GetReward();
                OnSuccess();
            }

            void OnErrorCallback(string error)
            {
                OnFail();
            }
        }
        private void OnCloseCallback()
        {
            IsAdsPlaying = false;
            _pauseService.Unpause();
        }

        private void OnOpenCallback()
        {
            IsAdsPlaying = true;
            _pauseService.Pause();
        }
    }
}