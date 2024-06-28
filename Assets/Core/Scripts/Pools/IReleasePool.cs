using UnityEngine;

namespace PlanetMerge.Pools
{
    public interface IReleasePool<T>
        where T : MonoBehaviour
    {
        void Release(T entity);
    }
}