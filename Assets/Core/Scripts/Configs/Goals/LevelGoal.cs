using UnityEngine;

namespace PlanetMerge.Configs.Goals
{
    [CreateAssetMenu(fileName = "LevelGoal", menuName = "Configs/LevelGoal", order = 1)]
    public class LevelGoal : ScriptableObject
    {
        [field: SerializeField, Range(1, 15)] public int MergeAmount { get; private set; }

        [field: SerializeField, Range(1, 7)] public int PlanetRankModifier { get; private set; }
    }
}