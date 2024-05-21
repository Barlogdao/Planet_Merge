using PlanetMerge.Systems;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private GameEventMediator _gameEventMediator;

    public void Initialize(PlayerInput playerInput, GameEventMediator gameEventMediator)
    {
        _playerInput = playerInput;
        _gameEventMediator = gameEventMediator;

        _gameEventMediator.LevelStarted += OnLevelStarted;
        _gameEventMediator.LevelResumed += OnLevelStarted;
        _gameEventMediator.LevelFinished += OnLevelFinished;

        DisableInput();
    }

    private void OnDestroy()
    {
        _gameEventMediator.LevelStarted -= OnLevelStarted;
        _gameEventMediator.LevelResumed -= OnLevelStarted;
        _gameEventMediator.LevelFinished -= OnLevelFinished;
    }

    private void OnLevelFinished()
    {
        DisableInput();
    }

    private void OnLevelStarted()
    {
        EnableInput();
    }

    private void EnableInput()
    {
        _playerInput.enabled = true;
    }

    private void DisableInput()
    {
        _playerInput.enabled = false;
    }
}
