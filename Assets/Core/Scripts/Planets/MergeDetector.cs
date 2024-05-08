using System;
using UnityEngine;

namespace PlanetMerge.Planets
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class MergeDetector : MonoBehaviour
    {
        private Planet _planet;

        public event Action<Planet> MergeDetected;

        private int PlanetRank => _planet.Rank;

        public void Initialize(Planet planet)
        {
            _planet = planet;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<Planet>(out Planet otherPlanet))
            {
                if (otherPlanet.Rank == PlanetRank && IsFaster(otherPlanet))
                {
                    MergeDetected?.Invoke(otherPlanet);
                }
            }
        }

        private bool IsFaster(Planet otherPlanet)
        {
            return _planet.GetSpeed() >= otherPlanet.GetSpeed();
        }
    }
}