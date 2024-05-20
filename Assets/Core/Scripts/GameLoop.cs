using PlanetMerge.Data;
using PlanetMerge.Systems;
using PlanetMerge.UI;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private PlayerData _playerData;
    private LevelGenerator _levelGenerator;
    private PlanetLimitHandler _planetLimitHandler;
    private LevelPlanets _planetOnLevel;

    private GameEventMediator _gameEventMediator;

    private int bonusPlanetAmount = 5;

    public void Initialize(GameEventMediator gameEventMediator, PlayerData playerData, LevelGenerator levelGenerator, PlanetLimitHandler planetLimitHandler, LevelPlanets planetsOnLevel)
    {
        _gameEventMediator = gameEventMediator;
        _playerData = playerData;
        _levelGenerator = levelGenerator;
        _planetLimitHandler = planetLimitHandler;
        _planetOnLevel = planetsOnLevel;


        _gameEventMediator.GameWinned += OnGameWinned;
        _gameEventMediator.NextLevelSelected += OnNextLevelSelected;
        _gameEventMediator.RestartLevelSelected += OnRestartLevelSelected;
        _gameEventMediator.RewardSelected += OnRewardSelected;
    }

    private void OnDestroy()
    {
        _gameEventMediator.GameWinned -= OnGameWinned;
        _gameEventMediator.NextLevelSelected -= OnNextLevelSelected;
        _gameEventMediator.RestartLevelSelected -= OnRestartLevelSelected;
        _gameEventMediator.RewardSelected -= OnRewardSelected;
    }

    public void PrepareLevel()
    {
        _planetOnLevel.Clear();
        _levelGenerator.Generate(_playerData);
        RunGame();
    }

    private void RunGame()
    {
        _gameEventMediator.RunLevel();
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
        _planetLimitHandler.SetLimit(bonusPlanetAmount);
        RunGame();
    }

    private void OnGameWinned()
    {
        _playerData.Level++;
    }
}
