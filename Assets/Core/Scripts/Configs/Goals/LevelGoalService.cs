using UnityEngine;
using NaughtyAttributes;

namespace PlanetMerge.Configs
{
    [CreateAssetMenu(fileName = "LevelGoalService", menuName = "Configs/Services/LevelGoalService", order = 1)]
    public class LevelGoalService : ScriptableObject
    {
        [SerializeField, Expandable] private LevelGoal[] _levelGoals;

        private readonly int _indexOffset = 1;

        [field: SerializeField, Expandable] public LevelGoal TutorialLevelGoal { get; private set; }

        public LevelGoal GetLevelGoal(int level)
        {
            int index;

            if (level <= _levelGoals.Length)
            {
                index = level - _indexOffset;
            }
            else
            {
                index = Random.Range(0, _levelGoals.Length);
            }

            return _levelGoals[index];
        }
    }
}