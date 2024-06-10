using PlanetMerge.SDK.Yandex;
using PlanetMerge.Services.Pause;
using System;
using UnityEngine;

public class AdvertisingService
{
    private readonly PauseService _pauseService;
    private readonly InterstitialHandler _interstitialHandler;
    private readonly RewardHandler _rewardHandler;

    public AdvertisingService(PauseService pauseService, RewardHandler rewardHandler)
    {
        _pauseService = pauseService;
        _interstitialHandler = new InterstitialHandler(_pauseService);
        _rewardHandler = rewardHandler;
    }

    public bool IsAdsPlaying { get; private set; }

    public void ShowInterstitialAd(Action OnClose)
    {
        _interstitialHandler.ShowAd(OnClose);
    }

    public void ShowRewardAd(Action OnSuccess, Action OnFail)
    {
        _rewardHandler.AddReward(OnSuccess, OnFail);
    }
}
