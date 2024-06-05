using PlanetMerge.Planets;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class CollisionSplash : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private IReleasePool<CollisionSplash> _pool;

    public void Initialize(IReleasePool<CollisionSplash> pool)
    {
        _pool = pool;
    }

    private void OnEnable()
    {
        _particleSystem.Play();
    }

    private void OnParticleSystemStopped()
    {
        _pool.Release(this);
    }
}
