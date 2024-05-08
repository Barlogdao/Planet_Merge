using UnityEngine;
using TMPro;
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

        public event Action<int> Merged;
        public event Action Collided;

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
            Collided?.Invoke();
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
            DisplayRank();
        }

        public void AddForce(Vector2 force)
        {
            _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
        }

        public float GetSpeed()
        {
            return Mathf.Abs(_rigidbody2D.velocity.x) + Mathf.Abs(_rigidbody2D.velocity.y);
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
            otherPlanet.Absorb();

            DisplayRank();

            Merged?.Invoke(Rank);
        }

        private void DisplayRank()
        {
            _view.Set(Rank);
        }

        public void Absorb()
        {
            _releasePool.Release(this);
        }
    }
}