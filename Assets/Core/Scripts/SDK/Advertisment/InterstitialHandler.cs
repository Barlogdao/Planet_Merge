using PlanetMerge.Services.Pause;
using System;

public class InterstitialHandler
{
    private PauseService _pauseService;

    public InterstitialHandler(PauseService pauseService)
    {
        _pauseService = pauseService;
    }

    public void ShowAd(Action OnClose/*, *//*Action OnFail, Action OnOffline*/)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.InterstitialAd.Show(OnOpenCallback, OnCloseCallback/*, OnErrorCallback, OnOfflineCallback*/);
#else
        OnClose();
#endif
        //void OnOfflineCallback()
        //{
        //    OnOffline();
        //}

        //void OnErrorCallback(string error)
        //{
        //    OnFail();
        //}

        void OnCloseCallback(bool wasShown)
        {
            _pauseService.Unpause();
            OnClose();
        }

        void OnOpenCallback()
        {
            _pauseService.Pause();
        }
    }

}