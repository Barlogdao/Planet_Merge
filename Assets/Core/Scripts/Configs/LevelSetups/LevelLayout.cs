using UnityEngine;
using NaughtyAttributes;

namespace PlanetMerge.Configs
{
    [CreateAssetMenu(fileName = "LevelLayout", menuName = "Configs/LevelLayout", order = 1)]
    public class LevelLayout : ScriptableObject
    {
        [field: SerializeField] public PlanetSetup[] PlanetSetups { get; private set; }
    }
}