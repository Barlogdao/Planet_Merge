using PlanetMerge.Pools;
using PlanetMerge.Entities.Planets;
using UnityEngine;

namespace PlanetMerge.Spawners
{
    public class PlanetSpawner
    {
        private readonly PlanetPool _pool;

        public PlanetSpawner (PlanetPool pool)
        {
            _pool = pool;
        }
     
        public Planet Spawn(Vector2 atPosition, int rank)
        {
            Planet planet = _pool.Get(atPosition);
            planet.Prepare(rank);

            return planet;
        }
    }
}