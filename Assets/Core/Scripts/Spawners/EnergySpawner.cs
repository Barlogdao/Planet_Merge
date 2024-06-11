using UnityEngine;
using DG.Tweening;
using PlanetMerge.Entities.Energy;
using PlanetMerge.Entities.Planets;
using PlanetMerge.Pools;
using PlanetMerge.Systems.Events;
using PlanetMerge.Systems.Gameplay.PlanetLaunching;

namespace PlanetMerge.Spawners
{
    public class EnergySpawner : MonoBehaviour
    {
        [SerializeField] private float _collectDuration;
        [SerializeField] private Ease _ease;

        private GameEventMediator _gameEventMediator;
        private ILaunchPoint _launchPoint;
        private EntityPool<Energy> _pool;

        public void Initialize(GameEventMediator gameEventMediator, ILaunchPoint launchPoint, EntityPool<Energy> pool)
        {
            _gameEventMediator = gameEventMediator;
            _launchPoint = launchPoint;
            _pool = pool;

            _gameEventMediator.PlanetMerged += OnPlanetMerged;
            _gameEventMediator.PlanetSplitted += OnPlanetSplitted;
        }


        private void OnDestroy()
        {
            _gameEventMediator.PlanetMerged -= OnPlanetMerged;
            _gameEventMediator.PlanetSplitted -= OnPlanetSplitted;
        }

        private void Spawn(Vector2 startPosition)
        {
            Vector2 endPosition = _launchPoint.LaunchPosition;
            Energy energy = _pool.Get(startPosition);

            energy.transform.DOMove(endPosition, _collectDuration).SetEase(_ease).OnComplete(() => _pool.Release(energy));
        }

        private void OnPlanetMerged(Planet planet)
        {
            Spawn(planet.transform.position);
        }

        private void OnPlanetSplitted(Planet planet)
        {
            Spawn(planet.transform.position);
        }
    }
}