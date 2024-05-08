using System;
using UnityEngine;
using UnityEngine.Pool;

namespace PlanetMerge.Planets
{
    public class PlanetPool : IReleasePool, IPlanetStatusNotifier
    {
        private readonly ObjectPool<Planet> _pool;

        private readonly Planet _prefab;
        private readonly Transform _parent;
        private readonly int _defaultCapacity;

        public PlanetPool(Planet prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
            _defaultCapacity = 20;
            _pool = new ObjectPool<Planet>(Create, OnGet, OnRelease, OnDestroy, defaultCapacity: _defaultCapacity);
        }

        public event Action<Planet> PlanetCreated;
        public event Action<Planet> PlanetReleased;

        public Planet Get(Vector2 atPosition)
        {
            Planet planet = _pool.Get();
            planet.transform.SetPositionAndRotation(atPosition, Quaternion.identity);

            return planet;
        }

        public void Release(Planet planet)
        {
            _pool.Release(planet);
        }

        private Planet Create()
        {
            Planet planet = UnityEngine.Object.Instantiate(_prefab, _parent);
            planet.Initialize(this);

            planet.gameObject.SetActive(false);

            return planet;
        }

        private void OnGet(Planet planet)
        {
            planet.gameObject.SetActive(true);
            PlanetCreated?.Invoke(planet);
        }
        private void OnRelease(Planet planet)
        {
            PlanetReleased?.Invoke(planet);
            planet.gameObject.SetActive(false);
        }

        private void OnDestroy(Planet planet)
        {
            UnityEngine.Object.Destroy(planet.gameObject);
        }
    }
}