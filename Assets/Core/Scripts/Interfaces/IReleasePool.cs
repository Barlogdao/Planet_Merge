using UnityEngine;

namespace PlanetMerge.Planets
{
    public interface IReleasePool<T> where T : MonoBehaviour
    {
        void Release(T entity);
    }
}