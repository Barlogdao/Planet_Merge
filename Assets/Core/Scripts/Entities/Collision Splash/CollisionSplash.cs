using PlanetMerge.Pools;
using UnityEngine;

namespace PlanetMerge.Entities.Splash
{
    [RequireComponent(typeof(ParticleSystem))]
    public class CollisionSplash : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private IReleasePool<CollisionSplash> _pool;

        private void OnEnable()
        {
            _particleSystem.Play();
        }

        private void OnParticleSystemStopped()
        {
            _pool.Release(this);
        }

        public void Initialize(IReleasePool<CollisionSplash> pool)
        {
            _pool = pool;
        }
    }
}