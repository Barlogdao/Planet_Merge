using NaughtyAttributes;
using UnityEngine;

namespace PlanetMerge.Configs.Goals
{
    [CreateAssetMenu(fileName = "LevelGoalService", menuName = "Configs/Services/LevelGoalService", order = 1)]
    public class LevelGoalService : ScriptableObject
    {
        private const int IndexOffset = 1;

        [SerializeField, Expandable] private LevelGoal[] _levelGoals;

        [field: SerializeField, Expandable] public LevelGoal TutorialLevelGoal { get; private set; }

        public LevelGoal GetLevelGoal(int level)
        {
            int index;

            if (level <= _levelGoals.Length)
            {
                index = level - IndexOffset;
            }
            else
            {
                index = Random.Range(0, _levelGoals.Length);
            }

            return _levelGoals[index];
        }
    }
}