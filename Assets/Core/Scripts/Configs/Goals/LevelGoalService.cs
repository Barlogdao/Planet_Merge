using UnityEngine;

namespace PlanetMerge.Configs
{
    [CreateAssetMenu(fileName = "LevelGoalService", menuName = "Configs/Services/LevelGoalService", order = 1)]
    public class LevelGoalService : ScriptableObject
    {
        [SerializeField] private LevelGoal[] _levelGoals;

        [field:SerializeField] public LevelGoal TutorialLevelGoal { get; private set; } 

        public LevelGoal GetLevelGoal()
        {
            int index = Random.Range(0, _levelGoals.Length);

            return _levelGoals[index];
        }
    }
}