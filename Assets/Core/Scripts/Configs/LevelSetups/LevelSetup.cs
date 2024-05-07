using UnityEngine;

namespace PlanetMerge.Configs
{
    [CreateAssetMenu(fileName = "LevelSetup", menuName = "Configs/LevelSetup", order = 1)]
    public class LevelSetup : ScriptableObject
    {
        [field: SerializeField] public PlanetSetup[] PlanetSetups { get; private set; }
    }
}