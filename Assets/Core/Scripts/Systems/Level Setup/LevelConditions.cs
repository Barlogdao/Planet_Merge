using PlanetMerge.Configs;
using PlanetMerge.Systems;

public class LevelConditions
{
    private readonly LevelGoalHandler _levelGoalHandler;
    private readonly EnergyLimitHandler _energyLimitHandler;

    public LevelConditions(LevelGoalHandler levelGoalHandler, EnergyLimitHandler energyLimitHandler)
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