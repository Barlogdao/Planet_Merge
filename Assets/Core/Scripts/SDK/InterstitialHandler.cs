using PlanetMerge.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitialHandler : MonoBehaviour
{
    [SerializeField] private GameEventMediator _gameEventMediator;


    private void OnEnable()
    {
        _gameEventMediator.LevelPrepared += ShowAd;
    }

    private void OnDisable()
    {
        _gameEventMediator.LevelPrepared -= ShowAd;

    }

    private void ShowAd()
    {
#if UNITY_WEBGL && !UNITY_EDITOR

        Agava.YandexGames.InterstitialAd.Show(OnOpenCallback, OnCloseCallback);
#endif
    }

    private void OnCloseCallback(bool wasShown)
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;
    }

    private void OnOpenCallback()
    {
        Time.timeScale = 0f;
        AudioListener.volume = 0f;
    }
}
