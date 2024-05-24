using PlanetMerge.Sevices.Audio;
using UnityEngine;

namespace PlanetMerge.Services.Pause
{
    public class PauseService
    {
        private AudioService _audioService;

        public PauseService(AudioService audioService)
        {
            _audioService = audioService;
        }

        public void Pause()
        {
            _audioService.PauseAudio();
            Time.timeScale = 0f;
        }

        public void Unpause()
        {
            _audioService.UnpauseAudio();
            Time.timeScale = 1f;
        }
    }
}