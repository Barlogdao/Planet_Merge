using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;
using System.Threading;
using System.Collections;

namespace PlanetMerge.Planets
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Planet : MonoBehaviour
    {
        [SerializeField] private MergeDetector _mergeDetector;
        [SerializeField] private PlanetView _view;

        private Rigidbody2D _rigidbody2D;
        private IReleasePool<Planet> _releasePool;

        private int _rank = 1;
        private bool _isSplitting = false;

        public event Action<Planet> Merged;
        public event Action<Vector2> Collided;
        public event Action<Planet> Splitted;

        private CancellationTokenSource _disableCancellation = new CancellationTokenSource();

        public int Rank => _rank;

        public void Initialize(IReleasePool<Planet> releasePool)
        {
            _releasePool = releasePool;
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _mergeDetector.Initialize(this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Collided?.Invoke(collision.GetContact(0).point);
            _view.Collide();
        }

        private void OnEnable()
        {
            _isSplitting = false;

            _mergeDetector.MergeDetected += OnMergeDetected;

            if (_disableCancellation != null)
            {
                _disableCancellation.Dispose();
            }
            _disableCancellation = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            _mergeDetector.MergeDetected -= OnMergeDetected;

            _disableCancellation.Cancel();
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

            float waitDuration = Random.Range(0.2f, 0.4f);
            WaitForSeconds delay = new WaitForSeconds(waitDuration);

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