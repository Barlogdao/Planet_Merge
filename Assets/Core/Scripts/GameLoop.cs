using PlanetMerge.Data;
using PlanetMerge.Systems;
using PlanetMerge.UI;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private GameUI _gameUI;
    private PlayerData _playerData;
    private LevelGenerator _levelGenerator;
    private PlanetLimitHandler _planetLimitHandler;
    private LevelPlanets _planetOnLevel;


    private GameEventMediator _gameEventMediator;

    private int bonusPlanetAmount = 5;

    public void Initialize(GameEventMediator gameEventMediator, GameUI gameUI, PlayerData playerData, LevelGenerator levelGenerator, PlanetLimitHandler planetLimitHandler, LevelPlanets planetsOnLevel)
    {
        _gameEventMediator = gameEventMediator;
        _gameUI = gameUI;
        _playerData = playerData;
        _levelGenerator = levelGenerator;
        _planetLimitHandler = planetLimitHandler;
        _planetOnLevel = planetsOnLevel;


        _gameEventMediator.GameWinned += OnLevelFinidhed;
        _gameEventMediator.GameLost += OnLevelLoosed;

        _gameUI.NextLevelPressed += OnNextLevelPressed;
        _gameUI.RestartLevelPressed += OnResetLevelPressed;
        _gameUI.RewardPressed += OnRewardPressed;
    }

    private void OnDestroy()
    {
        _gameEventMediator.GameWinned -= OnLevelFinidhed;
        _gameEventMediator.GameLost -= OnLevelLoosed;

        _gameUI.NextLevelPressed -= OnNextLevelPressed;
        _gameUI.RestartLevelPressed -= OnResetLevelPressed;
        _gameUI.RewardPressed -= OnRewardPressed;
    }

    private void OnLevelLoosed()
    {
        _gameUI.ShowLooseWindow();
    }

    private void OnRewardPressed()
    {
        _planetLimitHandler.SetLimit(bonusPlanetAmount);
        RunGame();
    }

    private void OnResetLevelPressed()
    {
        PrepareLevel();
    }

    private void OnNextLevelPressed()
    {
        PrepareLevel();
    }

    private void OnLevelFinidhed()
    {
        _playerData.Level++;

        _gameUI.ShowWinWindow();
    }

    public void PrepareLevel()
    {
        _gameUI.Hide();
        _planetOnLevel.Clear();
        _levelGenerator.Generate();
        RunGame();
    }

    public void RunGame()
    {
        _gameUI.Hide();

    }

}
