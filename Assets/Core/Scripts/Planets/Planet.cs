using UnityEngine;
using System;

namespace PlanetMerge.Planets
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Planet : MonoBehaviour
    {
        [SerializeField] private MergeDetector _mergeDetector;
        [SerializeField] private PlanetView _view;

        private int _rank = 1;
        private Rigidbody2D _rigidbody2D;
        private IReleasePool _releasePool;

        public event Action<Planet> Merged;
        public event Action<Vector2> Collided;

        public int Rank => _rank;

        public void Initialize(IReleasePool releasePool)
        {
            _releasePool = releasePool;
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _mergeDetector.Initialize(this);
            _view.Initialize(this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Collided?.Invoke(collision.GetContact(0).point);
        }

        private void OnEnable()
        {
            _mergeDetector.MergeDetected += OnMergeDetected;
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
            if (enabled)
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
    }
}