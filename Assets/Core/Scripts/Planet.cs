using UnityEngine;
using TMPro;
using System;

namespace PlanetMerge.Planets
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Planet : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _rankLabel;
        [SerializeField] private MergeDetector _mergeDetector;

        private int _rank = 1;
        private Rigidbody2D _rigidbody2D;
        private IReleasePool _releasePool;

        public int Rank => _rank;
        public event Action<int> Merged;

        public void Initialize(IReleasePool releasePool)
        {
            _releasePool = releasePool;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _mergeDetector.Initialize(this);
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
            DisplayLevel();
        }

        public void AddForce(Vector2 force)
        {
            _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
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

            DisplayLevel();

            Merged?.Invoke(Rank);
        }

        private void DisplayLevel()
        {
            _rankLabel.text = _rank.ToString();
        }

        public void Absorb()
        {
            _releasePool.Release(this);
        }
    }
}