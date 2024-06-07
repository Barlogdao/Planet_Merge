using PlanetMerge.Systems;
using System;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    private IEnergyLimitNotifier _energyLimitNotifier;
    private ILevelGoalNotifier _levelGoalNotifier;

    public event Action GameWon;
    public event Action GameLost;

    public void Initialize(IEnergyLimitNotifier energyLimitNotifier, ILevelGoalNotifier levelGoalNotifier)
    {
        _energyLimitNotifier = energyLimitNotifier;
        _levelGoalNotifier = levelGoalNotifier;

        _energyLimitNotifier.LimitExpired += OnLimitExpired;
        _levelGoalNotifier.GoalReached += OnGoalReached;
    }

    private void OnDestroy()
    {
        _energyLimitNotifier.LimitExpired -= OnLimitExpired;
        _levelGoalNotifier.GoalReached -= OnGoalReached;
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
