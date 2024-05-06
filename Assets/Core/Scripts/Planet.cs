using UnityEngine;

namespace PlanetMerge.Planet
{
    public class Planet : MonoBehaviour
    {
        private int _level = 1;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<Planet>(out Planet planet))
            {

            }
        }
    }
}