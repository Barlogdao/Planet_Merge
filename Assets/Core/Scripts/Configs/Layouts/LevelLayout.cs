using UnityEngine;

namespace PlanetMerge.Configs.Layouts
{
    [CreateAssetMenu(fileName = "LevelLayout", menuName = "Configs/LevelLayout", order = 1)]
    public class LevelLayout : ScriptableObject
    {
        [field: SerializeField] public PlanetSetup[] PlanetSetups { get; private set; }
    }
}