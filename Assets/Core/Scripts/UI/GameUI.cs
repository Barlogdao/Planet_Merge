using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlanetMerge.Systems;
using System;

public class GameUI : MonoBehaviour
{
    [SerializeField] TMP_Text _limitLabel;
    [SerializeField] TMP_Text _goalLabel;

    private PlanetLimitHandler _planetLimitHandler;
    private LevelGoalHandler _levelGoalHandler;

    public void Initialize(PlanetLimitHandler planetLimitHandler, LevelGoalHandler levelGoalHandler)
    {
        _planetLimitHandler = planetLimitHandler;
        _levelGoalHandler = levelGoalHandler;

        _planetLimitHandler.LimitChanged += OnLimitChanged;
        _levelGoalHandler.GoalChanged += OnGoalChanged;
    }

    private void OnDestroy()
    {
        _planetLimitHandler.LimitChanged -= OnLimitChanged;
        _levelGoalHandler.GoalChanged -= OnGoalChanged;

    }

    private void OnLimitChanged(int amount)
    {
        _limitLabel.text = amount.ToString();
    }

    private void OnGoalChanged(int mergeLeft)
    {
        _goalLabel.text = mergeLeft.ToString();
    }
}
