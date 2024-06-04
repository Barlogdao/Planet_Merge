using PlanetMerge.Systems;
using System;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    private EnergyLimitHandler _energyLimitHandler;
    private LevelGoalHandler _levelGoalHandler;

    public event Action GameWon;
    public event Action GameLost;

    public void Initialize(EnergyLimitHandler energyLimitHandler, LevelGoalHandler levelGoalHandler)
    {
        _energyLimitHandler = energyLimitHandler;
        _levelGoalHandler = levelGoalHandler;

        _energyLimitHandler.LimitExpired += OnLimitExpired;
        _levelGoalHandler.GoalReached += OnGoalReached;
    }

    private void OnDestroy()
    {
        _energyLimitHandler.LimitExpired -= OnLimitExpired;
        _levelGoalHandler.GoalReached -= OnGoalReached;
    }

    private void OnGoalReached()
    {
        GameWon?.Invoke();
    }

    private void OnLimitExpired()
    {
        GameLost?.Invoke();
    }
}
