using UnityEngine;
using TMPro;
using System;

namespace PlanetMerge.Planets
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Planet : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelLabel;
        [SerializeField] private MergeDetector _mergeDetector;

        private int _level = 1;
        private Rigidbody2D _rigidbody2D;
        private IReleasePool _releasePool;

        public int Level => _level;
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


        private void Start()
        {

            DisplayLevel();
        }

        public void Prepare(int level)
        {
            _level = level;
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
            _level++;
            otherPlanet.Absorb();

            DisplayLevel();

            Merged?.Invoke(Level);
        }

        private void DisplayLevel()
        {
            _levelLabel.text = _level.ToString();
        }

        public void Absorb()
        {
            _releasePool.Release(this);
        }
    }
}