using System;
using UnityEngine;

namespace PlanetMerge.Systems.Audio
{
    public class AudioHandler : MonoBehaviour
    {
        private AudioService _audioService;
        private GameEventMediator _gameEventMediator;

        [SerializeField] private AudioClip _collideSound;
        [SerializeField] private AudioClip _musicClip;

        public void Initialize (AudioService audioService, GameEventMediator gameEventMediator)
        {
            _audioService = audioService;
            _gameEventMediator = gameEventMediator;

            _gameEventMediator.PlanetCollided += OnPlanetCollided;
        }

        private void Start()
        {
            _audioService.PlayMusic(_musicClip);
        }

        private void OnDestroy()
        {
            _gameEventMediator.PlanetCollided -= OnPlanetCollided;
        }

        private void OnPlanetCollided(Vector2 at)
        {
            _audioService.PlaySound(_collideSound);
        }
    }
}