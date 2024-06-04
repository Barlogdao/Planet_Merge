using PlanetMerge.Services.Pause;
using UnityEngine;

public class InterstitialHandler : MonoBehaviour
{
    private GameEventMediator _gameEventMediator;
    private PauseService _pauseService;

    public void Initialize(GameEventMediator gameEventMediator, PauseService pauseService)
    {
        _gameEventMediator = gameEventMediator;
        _pauseService = pauseService;

        _gameEventMediator.RestartLevelSelected += ShowAd;
        _gameEventMediator.NextLevelSelected += ShowAd;
    }

    private void OnDestroy()
    {
        _gameEventMediator.RestartLevelSelected -= ShowAd;
        _gameEventMediator.NextLevelSelected -= ShowAd;
    }

    private void ShowAd()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.InterstitialAd.Show(OnOpenCallback, OnCloseCallback);
#endif
        return;
    }

    private void OnCloseCallback(bool wasShown)
    {
        _pauseService.Unpause();
    }

    private void OnOpenCallback()
    {
        _pauseService.Pause();
    }
}
