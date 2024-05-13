using PlanetMerge.Data;
using PlanetMerge.Systems;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private GameOverHandler _gameOverHandler;
    private GameUI _gameUI;
    private PlayerData _playerData;
    private LevelGenerator _levelGenerator;
    private PlanetLimitHandler _planetLimitHandler;
    private PlanetsOnLevel _planetOnLevel;
    private PlayerInput _playerInput;

    private int bonusPlanetAmount = 5;

    public void Initialize(GameOverHandler gameOverHandler, GameUI gameUI, PlayerData playerData, LevelGenerator levelGenerator, PlanetLimitHandler planetLimitHandler, PlanetsOnLevel planetsOnLevel, PlayerInput playerInput)
    {
        _gameOverHandler = gameOverHandler;
        _gameUI = gameUI;
        _playerData = playerData;
        _levelGenerator = levelGenerator;
        _planetLimitHandler = planetLimitHandler;
        _planetOnLevel = planetsOnLevel;
        _playerInput = playerInput;

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
        DisableInput();
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
        DisableInput();
        _gameUI.ShowFinishWindow();
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
        EnableInput();
    }

    private void DisableInput()
    {
        _playerInput.enabled = false;
    }

    private void EnableInput()
    {
        _playerInput.enabled = true;
    }


}
