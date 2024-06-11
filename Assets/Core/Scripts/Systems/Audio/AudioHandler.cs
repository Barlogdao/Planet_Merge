using UnityEngine;
using PlanetMerge.Entities.Planets;
using PlanetMerge.Systems.Events;

namespace PlanetMerge.Systems.Audio
{
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField] private AudioClip _collideSound;
        [SerializeField] private AudioClip _mergeSound;
        [SerializeField] private AudioClip _musicClip;

        private GameEventMediator _gameEventMediator;
        private AudioService _audioService;

        public void Initialize (AudioService audioService, GameEventMediator gameEventMediator)
        {
            _audioService = audioService;
            _gameEventMediator = gameEventMediator;

            _gameEventMediator.PlanetCollided += OnPlanetCollided;
            _gameEventMediator.PlanetMerged += OnPlanetMerged;
        }

        private void Start()
        {
            _audioService.PlayMusic(_musicClip);
        }

        private void OnDestroy()
        {
            _gameEventMediator.PlanetCollided -= OnPlanetCollided;
            _gameEventMediator.PlanetMerged -= OnPlanetMerged;
        }

        private void OnPlanetMerged(Planet planet)
        {
            _audioService.PlaySound(_mergeSound);
        }

        private void OnPlanetCollided(Vector2 at)
        {
            _audioService.PlaySound(_collideSound);
        }
    }
}