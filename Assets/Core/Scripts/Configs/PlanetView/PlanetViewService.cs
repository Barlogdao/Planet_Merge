using System;
using NaughtyAttributes;
using UnityEngine;

namespace PlanetMerge.Configs
{
    [CreateAssetMenu(fileName = "PlanetViewService", menuName = "Configs/Services/PlanetViewService", order = 1)]
    public class PlanetViewService : ScriptableObject
    {
        private const int ArrayOffset = 1;

        [SerializeField, Expandable] private PlanetViewConfig[] _planetViewConfigs;

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