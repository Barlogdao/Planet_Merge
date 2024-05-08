using UnityEngine;
using System;

namespace PlanetMerge.Planets
{
    public class PlanetFactory : MonoBehaviour
    {
        private const int MinimalPlanetRank = 1;

        private PlanetPool _pool;

        public void Initialize(PlanetPool pool)
        {
            _pool = pool;
        }
     
        public Planet Create(Vector2 atPosition, int rank)
        {
            if (rank < MinimalPlanetRank)
                throw new ArgumentException($" {nameof(rank)} can not be lower than {MinimalPlanetRank}");

            Planet planet = _pool.Get(atPosition);
            planet.Prepare(rank);

            return planet;
        }
    }
}