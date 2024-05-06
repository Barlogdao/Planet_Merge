using UnityEngine;
using TMPro;

namespace PlanetMerge.Planet
{
    public class Planet : MonoBehaviour
    {
        private int _level = 1;
        [SerializeField] private TextMeshProUGUI _levelLabel;

        private void Start()
        {
            DisplayLevel();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<Planet>(out Planet planet))
            {
                if (planet._level == _level)
                {
                    Merge(planet);

                }
            }
        }

        public void Merge(Planet planet)
        {
            if (enabled)
            {
                _level++;
                DisplayLevel();

                Destroy(planet.gameObject);
            }
        }

        private void DisplayLevel()
        {
            _levelLabel.text = _level.ToString();

        }
    }
}