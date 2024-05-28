using PlanetMerge.Services.Pools;
using System;
using UnityEngine;

namespace PlanetMerge.Systems.Visual
{
    public class CollisionSplashSystem : MonoBehaviour
    {
        private GameEventMediator _gameEventMediator;
        private CollisionSplashPool _pool;

        public void Initialize (GameEventMediator gameEventMediator, CollisionSplashPool pool)
        {
            _gameEventMediator = gameEventMediator;
            _pool = pool;

            _gameEventMediator.LevelStarted += OnLevelStarted;
            _gameEventMediator.LevelFinished += OnLevelFinished;
        }

        private void OnDestroy()
        {
            _gameEventMediator.LevelStarted += OnLevelStarted;
            _gameEventMediator.LevelFinished += OnLevelFinished;
            _gameEventMediator.PlanetCollided += OnPlanetCollided;

        }
        private void OnLevelStarted()
        {
            _gameEventMediator.PlanetCollided += OnPlanetCollided;
        }

        private void OnLevelFinished()
        {
            _gameEventMediator.PlanetCollided -= OnPlanetCollided;
        }

        private void OnPlanetCollided(Vector2 at)
        {
            _pool.Get(at);
        }
    }
}