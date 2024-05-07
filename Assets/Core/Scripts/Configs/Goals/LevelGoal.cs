using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetMerge.Configs
{
    [CreateAssetMenu(fileName = "LevelGoal", menuName = "Configs/LevelGoal", order = 1)]
    public class LevelGoal : ScriptableObject
    {
        [field: SerializeField][Range(1, 15)] public int PlanetsMergedAmount;
        [field: SerializeField][Range(1, 7)] public int PlanetLevelModificator;
    }
}