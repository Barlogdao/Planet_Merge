using System;

namespace PlanetMerge.Systems.Gameplay
{
    public interface ILevelGoalNotifier
    {
        event Action GoalReached;

        event Action<int> GoalChanged;

        int PlanetGoalRank { get; }
    }
}