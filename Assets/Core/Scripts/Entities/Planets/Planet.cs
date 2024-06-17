using System;
using System.Collections;
using PlanetMerge.Entities.Walls;
using PlanetMerge.Pools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlanetMerge.Entities.Planets
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Planet : MonoBehaviour
    {
        [SerializeField] private MergeDetector _mergeDetector;
        [SerializeField] private PlanetView _view;
        [SerializeField] private float _minSplitDelay = 0.1f;
        [SerializeField] private float _maxSplitDelay = 0.2f;

        private Rigidbody2D _rigidbody2D;
        private IReleasePool<Planet> _releasePool;
        private int _rank = Constants.MinimalPlanetRank;
        private bool _isSplitting = false;

        public event Action<Planet> Merged;
        public event Action<Vector2> PlanetCollided;
        public event Action<Vector2> WallCollided;
        public event Action<Planet> Splitted;

        public int Rank => _rank;

        private void OnEnable()
        {
            _isSplitting = false;

            _mergeDetector.MergeDetected += OnMergeDetected;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Vector2 collisionPoint = collision.GetContact(0).point;

            if (collision.gameObject.TryGetComponent<Planet>(out _))
            {
                PlanetCollided?.Invoke(collisionPoint);
            }
            else if (collision.gameObject.TryGetComponent<Wall>(out _))
            {
                WallCollided?.Invoke(collisionPoint);
            }

            _view.Collide();
        }

        public void Initialize(IReleasePool<Planet> releasePool)
        {
            _releasePool = releasePool;
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _mergeDetector.Initialize(this);
        }


        private void OnDisable()
        {
            _mergeDetector.MergeDetected -= OnMergeDetected;
        }

        public void Prepare(int rank)
        {
            _rank = rank;
            UpdateView();
        }

        public void AddForce(Vector2 force)
        {
            _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
        }

        public float GetSpeed()
        {
            return Mathf.Abs(_rigidbody2D.velocity.x) + Mathf.Abs(_rigidbody2D.velocity.y);
        }

        public void Release()
        {
            _releasePool.Release(this);
        }

        public void Split()
        {
            StartCoroutine(Splitting());
        }

        private void Merge(Planet otherPlanet)
        {
            _rank++;
            otherPlanet.Release();

            UpdateView();

            Merged?.Invoke(this);
        }

        private void UpdateView()
        {
            _view.Set(Rank);
        }

        private IEnumerator Splitting()
        {
            _isSplitting = true;

            float splitDelay = Random.Range(_minSplitDelay, _maxSplitDelay);
            WaitForSeconds delay = new WaitForSeconds(splitDelay);

            yield return delay;

            while (_rank > Constants.MinimalPlanetRank)
            {
                _rank--;
                UpdateView();

                Splitted?.Invoke(this);
                yield return delay;
            }

            _isSplitting = false;
            Release();
        }

        private void OnMergeDetected(Planet otherPlanet)
        {
            if (enabled && _isSplitting == false)
            {
                Merge(otherPlanet);
            }
        }
    }
}