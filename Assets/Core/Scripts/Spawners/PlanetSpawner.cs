using UnityEngine;
using System;
using PlanetMerge.Pools;
using PlanetMerge.Entities.Planets;

namespace PlanetMerge.Spawners
{
    public class PlanetSpawner
    {
        private const int MinimalPlanetRank = 1;

        private readonly PlanetPool _pool;

        public PlanetSpawner (PlanetPool pool)
        {
            _pool = pool;
        }
     
        public Planet Spawn(Vector2 atPosition, int rank)
        {
            if (rank < MinimalPlanetRank)
                throw new ArgumentException($" {nameof(rank)} can not be lower than {MinimalPlanetRank}");

            Planet planet = _pool.Get(atPosition);
            planet.Prepare(rank);

            return planet;
        }
    }
}