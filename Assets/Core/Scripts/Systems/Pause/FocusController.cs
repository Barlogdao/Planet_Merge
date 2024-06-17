using Agava.WebUtility;
using PlanetMerge.SDK.Yandex.Advertising;
using PlanetMerge.Systems.Pause;
using UnityEngine;

namespace PlanetMerge.SDK.Yandex
{
    public class FocusController : MonoBehaviour
    {
        private PauseService _pauseService;
        private AdvertisingService _advertisingService;

        private void OnEnable()
        {
            Application.focusChanged += OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
        }

        private void OnDisable()
        {
            Application.focusChanged -= OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
        }

        public void Initialize(PauseService pauseService, AdvertisingService advertisingService)
        {
            _pauseService = pauseService;
            _advertisingService = advertisingService;
        }

        private void OnInBackgroundChangeApp(bool inApp)
        {
            if (_advertisingService.IsAdsPlaying == false)
                PauseGame(!inApp);
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            if (_advertisingService.IsAdsPlaying == false)
                PauseGame(isBackground);
        }

        private void PauseGame(bool isUnfocused)
        {
            if (isUnfocused)
            {
                _pauseService.Pause();
            }
            else
            {
                _pauseService.Unpause();
            }
        }
    }
}