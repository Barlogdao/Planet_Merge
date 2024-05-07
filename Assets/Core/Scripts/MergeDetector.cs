using System;
using UnityEngine;

namespace PlanetMerge.Planets
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class MergeDetector : MonoBehaviour
    {
        private Planet _planet;

        public event Action<Planet> MergeDetected;

        private int PlanetLevel => _planet.Level;

        public void Initialize(Planet planet)
        {
            _planet = planet;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<Planet>(out Planet planet))
            {
                if (planet.Level == PlanetLevel)
                {
                    MergeDetected?.Invoke(planet);
                }
            }
        }
    }
}