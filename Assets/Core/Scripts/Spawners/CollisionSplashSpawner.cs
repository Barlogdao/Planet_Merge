using PlanetMerge.Pools;
using PlanetMerge.Systems.Events;
using UnityEngine;

namespace PlanetMerge.Spawners
{
    public class CollisionSplashSpawner : MonoBehaviour
    {
        private GameEventMediator _gameEventMediator;
        private CollisionSplashPool _pool;

        public void Initialize (GameEventMediator gameEventMediator, CollisionSplashPool pool)
        {
            _gameEventMediator = gameEventMediator;
            _pool = pool;

            _gameEventMediator.PlanetCollided += SpawnSplash;
            _gameEventMediator.WallCollided += SpawnSplash;
        }

        private void OnDestroy()
        {
            _gameEventMediator.PlanetCollided -= SpawnSplash;
            _gameEventMediator.WallCollided -= SpawnSplash;
        }


        private void SpawnSplash(Vector2 at)
        {
            _pool.Get(at);
        }
    }
}