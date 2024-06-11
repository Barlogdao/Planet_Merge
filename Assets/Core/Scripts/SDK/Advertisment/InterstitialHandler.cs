using PlanetMerge.Systems.Pause;
using System;

namespace PlanetMerge.SDK.Yandex.Advertising
{
    public class InterstitialHandler
    {
        private PauseService _pauseService;

        public InterstitialHandler(PauseService pauseService)
        {
            _pauseService = pauseService;
        }

        public void ShowAd(Action OnClose)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.InterstitialAd.Show(OnOpenCallback, OnCloseCallback);
#else
            OnClose();
#endif
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
}