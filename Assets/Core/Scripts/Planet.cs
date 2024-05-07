using UnityEngine;
using TMPro;

namespace PlanetMerge.Planet
{
    public class Planet : MonoBehaviour
    {
        private int _level = 1;
        [SerializeField] private TextMeshProUGUI _levelLabel;

        [SerializeField] private MergeDetector _mergeDetector;

        public int Level => _level;

        private void Awake()
        {
            _mergeDetector.Initialize(this);
        }
        private void OnEnable()
        {
            _mergeDetector.MergeDetected += OnMerge;
        }

        private void OnDisable()
        {
            _mergeDetector.MergeDetected -= OnMerge;
        }


        private void Start()
        {

            DisplayLevel();
        }


        private void OnMerge(Planet otherPlanet)
        {
            if (enabled)
            {
                _level++;
                DisplayLevel();

                Destroy(otherPlanet.gameObject);
            }
        }

        private void DisplayLevel()
        {
            _levelLabel.text = _level.ToString();

        }
    }
}