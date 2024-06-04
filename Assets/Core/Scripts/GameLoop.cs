using Cysharp.Threading.Tasks;
using PlanetMerge.SDK.Yandex;
using System;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private GameEventMediator _gameEventMediator;

    private StartLevelHandler _startLevelHandler;
    private EndLevelHandler _endLevelHandler;
    private PrepareLevelHandler _prepareLevelHandler;
    private RewardHandler _rewardHandler;

    public event Action LevelPrepared;
    public event Action LevelStarted;
    public event Action LevelResumed;

    public void Initialize(GameEventMediator gameEventMediator, LevelStateHandlers levelStateHandlers, RewardHandler rewardHandler)
    {
        _gameEventMediator = gameEventMediator;
        _prepareLevelHandler = levelStateHandlers.PrepareLevelHandler;
        _startLevelHandler = levelStateHandlers.StartLevelHandler;
        _endLevelHandler = levelStateHandlers.EndLevelHandler;
        _rewardHandler = rewardHandler;

        _gameEventMediator.GameWon += OnGameWon;
        _gameEventMediator.GameLost += OnGameLost;

        _gameEventMediator.NextLevelSelected += OnNextLevelSelected;
        _gameEventMediator.RestartLevelSelected += OnRestartLevelSelected;
        _gameEventMediator.RewardSelected += OnRewardSelected;
    }

    private void OnDestroy()
    {
        _gameEventMediator.GameWon -= OnGameWon;
        _gameEventMediator.GameLost -= OnGameLost;

        _gameEventMediator.NextLevelSelected -= OnNextLevelSelected;
        _gameEventMediator.RestartLevelSelected -= OnRestartLevelSelected;
        _gameEventMediator.RewardSelected -= OnRewardSelected;
    }

    public void Run()
    {
        PrepareLevel();
    }

    private void PrepareLevel()
    {
        _prepareLevelHandler.PrepareLevel();
        LevelPrepared?.Invoke();

        StartLevel().Forget();
    }

    private async UniTaskVoid StartLevel()
    {
        await _startLevelHandler.StartLevelAsync();
        LevelStarted?.Invoke();
    }

    private void OnGameWon()
    {
        _endLevelHandler.Win().Forget();
    }

    private void OnGameLost()
    {
        _endLevelHandler.Loose();
    }

    private void OnNextLevelSelected()
    {
        PrepareLevel();
    }

    private void OnRestartLevelSelected()
    {
        PrepareLevel();
    }

    private void OnRewardSelected()
    {
        _rewardHandler.AddReward(ResumeLevel, PrepareLevel);
    }

    private void ResumeLevel()
    {
        _startLevelHandler.ResumeLevel();
        LevelResumed?.Invoke();
    }
}
