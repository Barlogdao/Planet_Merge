using PlanetMerge.Configs;
using PlanetMerge.Systems;

public class LevelConditions
{
    private LevelGoalHandler _levelGoalHandler;
    private PlanetLimitHandler _planetLimitHandler;

    public LevelConditions(LevelGoalHandler levelGoalHandler, PlanetLimitHandler planetLimitHandler)
    {
        _levelGoalHandler = levelGoalHandler;
        _planetLimitHandler = planetLimitHandler;
    }

    public void Prepare(LevelGoal levelGoal, int planetRank, int limitAmount)
    {
        _levelGoalHandler.Prepare(levelGoal.MergeAmount, planetRank + levelGoal.PlanetRankModifier);
        _planetLimitHandler.SetLimit(limitAmount);
    }
}