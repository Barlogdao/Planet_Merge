using UnityEngine;
using PlanetMerge.Entities.Planets;
using PlanetMerge.Systems.Events;
using System;

namespace PlanetMerge.Systems.Audio
{
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField] private AudioClip _musicClip;
        [SerializeField] private AudioClip _planetCollideSound;
        [SerializeField] private AudioClip _wallCollideSound;
        [SerializeField] private AudioClip _mergeSound;
        [SerializeField] private AudioClip _winSound;
        [SerializeField] private AudioClip _looseSound;

        private GameEventMediator _gameEventMediator;
        private AudioService _audioService;

        private void Start()
        {
            _audioService.PlayMusic(_musicClip);
        }

        private void OnDestroy()
        {
            _gameEventMediator.PlanetCollided -= OnPlanetCollided;
            _gameEventMediator.PlanetMerged -= OnPlanetMerged;
            _gameEventMediator.WallCollided -= OnWallCollided;
            _gameEventMediator.GameWon -= OnGameWon;
            _gameEventMediator.GameLost -= OnGameLost;
        }

        public void Initialize(AudioService audioService, GameEventMediator gameEventMediator)
        {
            _audioService = audioService;
            _gameEventMediator = gameEventMediator;

            _gameEventMediator.PlanetCollided += OnPlanetCollided;
            _gameEventMediator.PlanetMerged += OnPlanetMerged;
            _gameEventMediator.WallCollided += OnWallCollided;
            _gameEventMediator.GameWon += OnGameWon;
            _gameEventMediator.GameLost += OnGameLost;
        }

        private void OnPlanetMerged(Planet planet)
        {
            _audioService.PlaySound(_mergeSound);
        }

        private void OnPlanetCollided(Vector2 at)
        {
            _audioService.PlaySound(_planetCollideSound);
        }

        private void OnWallCollided(Vector2 atPoint)
        {
            _audioService.PlaySound(_wallCollideSound);
        }

        private void OnGameLost()
        {
            _audioService.PlaySound(_looseSound);
        }

        private void OnGameWon()
        {
            _audioService.PlaySound(_winSound);
        }
    }
}