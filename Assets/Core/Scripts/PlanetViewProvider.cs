using UnityEngine;
using System;

namespace PlanetMerge.Systems
{
    [CreateAssetMenu(fileName = "PlanetViewProvider", menuName = "Configs/PlanetViewProvider", order = 1)]
    public class PlanetViewProvider : ScriptableObject
    {
        private const int ArrayOffset = 1;

        [SerializeField] private Sprite[] _planetSprites;

        public Sprite GetPlanetSprite(int planetRank)
        {
            if (planetRank <= 0)
                throw new ArgumentOutOfRangeException(nameof(planetRank));

            int index = (planetRank - ArrayOffset) % _planetSprites.Length;

            return _planetSprites[index];
        }
    }
}