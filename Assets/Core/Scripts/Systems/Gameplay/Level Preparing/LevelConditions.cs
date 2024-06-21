using PlanetMerge.Configs.Goals;

namespace PlanetMerge.Systems.Gameplay.LevelPreparing
{
    public class LevelConditions
    {
        private readonly LevelGoalController _levelGoalHandler;
        private readonly EnergyLimitController _energyLimitHandler;

        public LevelConditions(LevelGoalController levelGoalHandler, EnergyLimitController energyLimitHandler)
        {
            _levelGoalHandler = levelGoalHandler;
            _energyLimitHandler = energyLimitHandler;
        }

        public void Prepare(LevelGoal levelGoal, int planetRank, int limitAmount)
        {
            _levelGoalHandler.Prepare(levelGoal.MergeAmount, planetRank + levelGoal.PlanetRankModifier);
            _energyLimitHandler.SetLimit(limitAmount);
        }
    }
}