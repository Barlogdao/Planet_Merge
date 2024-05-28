using DG.Tweening;
using PlanetMerge.Planets;
using PlanetMerge.Services.Pools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetMerge.Systems.Visual
{
    public class EnergyCollectSystem : MonoBehaviour
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
        }

        private void OnDestroy()
        {
            _gameEventMediator.PlanetMerged -= OnPlanetMerged;
        }

        private void OnPlanetMerged(Planet planet)
        {
            Vector2 startPosition = planet.transform.position;
            Vector2 endPosition = _launchPoint.LaunchPosition;
            Energy energy = _pool.Get(startPosition);

            energy.transform.DOMove(endPosition, _collectDuration).SetEase(_ease).OnComplete(() => _pool.Release(energy));
        }
    }
}