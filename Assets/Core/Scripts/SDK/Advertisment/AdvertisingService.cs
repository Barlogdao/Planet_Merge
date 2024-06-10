using PlanetMerge.SDK.Yandex;
using PlanetMerge.Services.Pause;
using System;

public class AdvertisingService
{
    private readonly PauseService _pauseService;
    private readonly RewardHandler _rewardHandler;

    public AdvertisingService(PauseService pauseService, RewardHandler rewardHandler)
    {
        _pauseService = pauseService;
        _rewardHandler = rewardHandler;
    }

    public bool IsAdsPlaying { get; private set; }

    public void ShowInterstitialAd(Action OnClose)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.InterstitialAd.Show(OnOpenCallback, OnCloseInterstitial);
#else
        OnClose();
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
        OnSuccess();
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
