using UnityEngine;
using UnityEngine.Pool;

namespace PlanetMerge.Pools
{
    public class EntityPool<T>
        where T : MonoBehaviour
    {
        private readonly ObjectPool<T> _pool;
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly int _defaultCapacity;

        public EntityPool(T prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
            _defaultCapacity = 20;
            _pool = new ObjectPool<T>(Create, OnGet, OnRelease, OnDestroy, defaultCapacity: _defaultCapacity);
        }

        public T Get(Vector2 atPosition)
        {
            T entity = _pool.Get();
            entity.transform.SetPositionAndRotation(atPosition, Quaternion.identity);

            return entity;
        }

        public void Release(T entity)
        {
            _pool.Release(entity);
        }

        protected virtual void OnCreateAction(T entity)
        {
        }

        protected virtual void OnGetAction(T entity)
        {
        }

        protected virtual void OnReleaseAction(T entity)
        {
        }

        private T Create()
        {
            T entity = UnityEngine.Object.Instantiate(_prefab, _parent);

            OnCreateAction(entity);
            entity.gameObject.SetActive(false);

            return entity;
        }

        private void OnGet(T entity)
        {
            entity.gameObject.SetActive(true);
            OnGetAction(entity);
        }

        private void OnRelease(T entity)
        {
            OnReleaseAction(entity);
            entity.gameObject.SetActive(false);
        }

        private void OnDestroy(T entity)
        {
            Object.Destroy(entity.gameObject);
        }
    }
}