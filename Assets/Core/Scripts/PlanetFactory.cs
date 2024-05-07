using UnityEngine;
using System;

namespace PlanetMerge.Planets
{
    public class PlanetFactory : MonoBehaviour
    {
        private const int MinimalLevel = 1;

        private PlanetPool _pool;

        public void Initialize(PlanetPool pool)
        {
            _pool = pool;
        }
     
        public Planet Create(Vector2 atPosition, int level)
        {
            if (level < MinimalLevel)
                throw new ArgumentException($" {nameof(level)} can not be lower than {MinimalLevel}");

            Planet planet = _pool.Get(atPosition);
            planet.Prepare(level);

            return planet;
        }
    }
}