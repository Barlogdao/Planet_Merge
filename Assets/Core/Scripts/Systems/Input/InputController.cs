using PlanetMerge.Systems.Events;
using UnityEngine;

namespace PlanetMerge.Systems
{
    public class InputController : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private GameEventMediator _gameEventMediator;

        private void OnDestroy()
        {
            _gameEventMediator.LevelStarted -= EnableInput;
            _gameEventMediator.LevelResumed -= EnableInput;
            _gameEventMediator.LevelPrepared -= DisableInput;
            _gameEventMediator.LevelFinished -= DisableInput;
        }

        public void Initialize(PlayerInput playerInput, GameEventMediator gameEventMediator)
        {
            _playerInput = playerInput;
            _gameEventMediator = gameEventMediator;

            _gameEventMediator.LevelStarted += EnableInput;
            _gameEventMediator.LevelResumed += EnableInput;
            _gameEventMediator.LevelPrepared += DisableInput;
            _gameEventMediator.LevelFinished += DisableInput;
        }

        public void EnableInput()
        {
            _playerInput.enabled = true;
        }

        public void DisableInput()
        {
            _playerInput.enabled = false;
        }
    }
}