using System;
using System.Collections;
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

        private int _rank = 1;
        private bool _isSplitting = false;

        public event Action<Planet> Merged;
        public event Action<Vector2> Collided;
        public event Action<Planet> Splitted;

        public int Rank => _rank;

        public void Initialize(IReleasePool<Planet> releasePool)
        {
            _releasePool = releasePool;
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _mergeDetector.Initialize(this);
        }

        private void OnEnable()
        {
            _isSplitting = false;

            _mergeDetector.MergeDetected += OnMergeDetected;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Collided?.Invoke(collision.GetContact(0).point);
            _view.Collide();
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

        private void OnMergeDetected(Planet otherPlanet)
        {
            if (enabled && _isSplitting == false)
            {
                Merge(otherPlanet);
            }
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

        public void Split()
        {
            StartCoroutine(Splitting());
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
    }
}