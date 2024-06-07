using System;

namespace PlanetMerge.Systems
{
    public interface ILevelGoalNotifier
    {
        event Action GoalReached;
        event Action<int> GoalChanged;

        int PlanetGoalRank { get; }
    }
}