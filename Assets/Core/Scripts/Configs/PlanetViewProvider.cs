using UnityEngine;
using System;

namespace PlanetMerge.Configs
{
    [CreateAssetMenu(fileName = "PlanetViewProvider", menuName = "Configs/PlanetViewProvider", order = 1)]
    public class PlanetViewProvider : ScriptableObject
    {
        private const int ArrayOffset = 1;

        [SerializeField] private PlanetViewConfig[] _planetViewConfigs;

        public PlanetViewData GetViewData(int planetRank)
        {
            if (planetRank <= 0)
                throw new ArgumentOutOfRangeException(nameof(planetRank));

            int index = (planetRank - ArrayOffset) % _planetViewConfigs.Length;
            PlanetViewConfig config = _planetViewConfigs[index];

            return new PlanetViewData(config, planetRank.ToString());
        }
    }

}