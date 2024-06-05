using Agava.WebUtility;
using PlanetMerge.Services.Pause;
using UnityEngine;

namespace PlanetMerge.Handlers.Pause
{
    public class FocusController : MonoBehaviour
    {
        private PauseService _pauseService;

        public void Initialize(PauseService pauseService)
        {
            _pauseService = pauseService;
        }

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

        private void OnInBackgroundChangeApp(bool inApp)
        {
            PauseGame(!inApp);
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
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