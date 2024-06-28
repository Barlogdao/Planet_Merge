using System;
using PlanetMerge.Systems.Gameplay;
using PlanetMerge.Systems.Pause;

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

        public void ShowInterstitialAd(Action onClose)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.InterstitialAd.Show(OnOpenCallback, OnCloseInterstitial);
#else
            OnCloseInterstitial(true);
#endif
            void OnCloseInterstitial(bool wasShown)
            {
                OnCloseCallback();
                onClose();
            }
        }

        public void ShowRewardAd(Action onSuccess, Action onFail)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback, OnErrorCallback);
#else
            OnRewardedCallback();
#endif
            void OnRewardedCallback()
            {
                _rewardHandler.GetReward();
                onSuccess();
            }

            void OnErrorCallback(string error)
            {
                onFail();
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