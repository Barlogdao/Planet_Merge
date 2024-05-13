using PlanetMerge.Data;
using PlanetMerge.Systems;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private GameOverHandler _gameOverHandler;
    private GameUI _gameUI;
    private PlayerData _playerData;
    private LevelGenerator _levelGenerator;
    private PlanetLimit _planetLimit;
    private PlanetsOnLevel _planetOnLevel;

    private int bonusPlanetAmount = 5;

    public void Initialize(GameOverHandler gameOverHandler, GameUI gameUI, PlayerData playerData, LevelGenerator levelGenerator, PlanetLimit planetLimit, PlanetsOnLevel planetsOnLevel)
    {
        _gameOverHandler = gameOverHandler;
        _gameUI = gameUI;
        _playerData = playerData;
        _levelGenerator = levelGenerator;
        _planetLimit = planetLimit;
        _planetOnLevel = planetsOnLevel;

        _gameOverHandler.LevelFinished += OnLevelFinidhed;
        _gameOverHandler.LevelLoosed += OnLevelLoosed;

        _gameUI.NextLevelPressed += OnNextLevelPressed;
        _gameUI.ResetLevelPressed += OnResetLevelPressed;
        _gameUI.RewardPressed += OnRewardPressed;
    }

    private void OnDestroy()
    {
        _gameOverHandler.LevelFinished -= OnLevelFinidhed;
        _gameOverHandler.LevelLoosed -= OnLevelLoosed;

        _gameUI.NextLevelPressed -= OnNextLevelPressed;
        _gameUI.ResetLevelPressed -= OnResetLevelPressed;
        _gameUI.RewardPressed -= OnRewardPressed;
    }

    private void OnLevelLoosed()
    {
        _gameUI.ShowLooseWindow();
    }

    private void OnRewardPressed()
    {
        _planetLimit.Add(bonusPlanetAmount);
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
        _gameUI.ShowFinishWindow();
    }

    public void PrepareLevel()
    {
        _gameUI.Hide();
        _planetOnLevel.Clear();
        _levelGenerator.Generate();
    }

    public void RunGame()
    {
        _gameUI.Hide();
    }


}
