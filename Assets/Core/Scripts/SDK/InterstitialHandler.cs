using UnityEngine;

public class InterstitialHandler : MonoBehaviour
{
    [SerializeField] private GameEventMediator _gameEventMediator;
    private void OnEnable()
    {
        _gameEventMediator.RestartLevelSelected += ShowAd;
        _gameEventMediator.NextLevelSelected += ShowAd;
    }

    private void OnDisable()
    {
        _gameEventMediator.RestartLevelSelected -= ShowAd;
        _gameEventMediator.NextLevelSelected -= ShowAd;
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
