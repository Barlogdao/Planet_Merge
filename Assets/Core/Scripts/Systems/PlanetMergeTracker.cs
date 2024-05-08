using PlanetMerge.Planets;
using UnityEngine;

namespace PlanetMerge.Systems
{
    public abstract class PlanetMergeTracker : MonoBehaviour
    {
        private IPlanetStatusNotifier _planetStatusNotifier;

        public void Initialize(IPlanetStatusNotifier planetStatusNotifier)
        {
            _planetStatusNotifier = planetStatusNotifier;

            _planetStatusNotifier.PlanetCreated += OnPlanetCreated;
            _planetStatusNotifier.PlanetReleased += OnPlanetReleased;
        }

        private void OnDestroy()
        {
            _planetStatusNotifier.PlanetCreated -= OnPlanetCreated;
            _planetStatusNotifier.PlanetReleased -= OnPlanetReleased;
        }

        protected abstract void OnPlanetMerged(int rank);

        private void OnPlanetCreated(Planet planet)
        {
            planet.Merged += OnPlanetMerged;
        }

        private void OnPlanetReleased(Planet planet)
        {
            planet.Merged -= OnPlanetMerged;
        }
    }
}