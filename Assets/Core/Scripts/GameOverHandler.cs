using PlanetMerge.Systems;
using System;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    private PlanetLimitHandler _planetLimitHandler;
    private LevelGoalHandler _levelGoalHandler;

    public event Action LevelFinished;
    public event Action LevelLoosed;

    public void Initialize(PlanetLimitHandler planetLimitHandler, LevelGoalHandler levelGoalHandler)
    {
        _planetLimitHandler = planetLimitHandler;
        _levelGoalHandler = levelGoalHandler;

        _planetLimitHandler.LimitExpired += OnLimitExpired;
        _levelGoalHandler.GoalReached += OnGoalReached;
    }

    private void OnDestroy()
    {
        _planetLimitHandler.LimitExpired -= OnLimitExpired;
        _levelGoalHandler.GoalReached -= OnGoalReached;
    }

    private void OnGoalReached()
    {
        LevelFinished?.Invoke();
        Debug.Log("WIN");
    }

    private void OnLimitExpired()
    {
        LevelLoosed?.Invoke();
        Debug.Log("LOOSE");
    }
}
