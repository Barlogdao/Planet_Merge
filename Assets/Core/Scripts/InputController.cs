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

        _gameEventMediator.LevelStaretd += OnLevelStarted;
        _gameEventMediator.LevelFinished += OnLevelFinished;
    }

    private void OnDestroy()
    {
        _gameEventMediator.LevelStaretd -= OnLevelStarted;
        _gameEventMediator.LevelFinished -= OnLevelFinished;
    }

    private void OnLevelFinished()
    {
        _playerInput.enabled = false;
    }

    private void OnLevelStarted()
    {
        _playerInput.enabled = true;
    }
}
