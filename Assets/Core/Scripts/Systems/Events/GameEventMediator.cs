using System;
using PlanetMerge.Entities.Planets;
using PlanetMerge.Loop;
using PlanetMerge.Systems.Gameplay;
using UnityEngine;

namespace PlanetMerge.Systems.Events
{
    public class GameEventMediator : MonoBehaviour, IPlanetEvents
    {
        private IPlanetStatusNotifier _planetStatusNotifier;
        private GameOverHandler _gameOverHandler;
        private GameLoop _gameLoop;
        private IGameUiEvents _uiEvents;
        private ILauncherNotifier _launcherNotifier;

        public event Action<Planet> PlanetCreated;

        public event Action<Planet> PlanetReleased;

        public event Action<Planet> PlanetSplitted;

        public event Action<Planet> PlanetMerged;

        public event Action<Vector2> PlanetCollided;

        public event Action<Vector2> WallCollided;

        public event Action LevelPrepared;

        public event Action LevelStarted;

        public event Action LevelResumed;

        public event Action LevelFinished;

        public event Action GameLost;

        public event Action GameWon;

        public event Action NextLevelSelected;

        public event Action RestartLevelSelected;

        public event Action RewardSelected;

        public event Action PlanetLaunched;

        private void OnDestroy()
        {
            _planetStatusNotifier.PlanetCreated -= OnPlanetCreated;
            _planetStatusNotifier.PlanetReleased -= OnPlanetReleased;

            _gameOverHandler.GameWon -= OnGameWon;
            _gameOverHandler.GameLost -= OnGameLost;

            _gameLoop.LevelPrepared += OnLevelPrepared;
            _gameLoop.LevelStarted -= OnLevelStarted;
            _gameLoop.LevelResumed -= OnLevelResumed;

            _uiEvents.NextLevelPressed -= OnNextLevePressed;
            _uiEvents.RestartLevelPressed -= OnRestartLevelPressed;
            _uiEvents.RewardPressed -= OnRewardPressed;

            _launcherNotifier.PlanetLaunched -= OnPlanetLaunched;
        }

        public void Initialize(
            IPlanetStatusNotifier planetStatusNotifier,
            GameOverHandler gameOverHandler,
            GameLoop gameLoop,
            IGameUiEvents uiEvents,
            ILauncherNotifier launcherNotifier)
        {
            _planetStatusNotifier = planetStatusNotifier;
            _gameOverHandler = gameOverHandler;
            _gameLoop = gameLoop;
            _uiEvents = uiEvents;
            _launcherNotifier = launcherNotifier;

            _planetStatusNotifier.PlanetCreated += OnPlanetCreated;
            _planetStatusNotifier.PlanetReleased += OnPlanetReleased;

            _gameOverHandler.GameWon += OnGameWon;
            _gameOverHandler.GameLost += OnGameLost;

            _gameLoop.LevelPrepared += OnLevelPrepared;
            _gameLoop.LevelStarted += OnLevelStarted;
            _gameLoop.LevelResumed += OnLevelResumed;

            _uiEvents.NextLevelPressed += OnNextLevePressed;
            _uiEvents.RestartLevelPressed += OnRestartLevelPressed;
            _uiEvents.RewardPressed += OnRewardPressed;

            _launcherNotifier.PlanetLaunched += OnPlanetLaunched;
        }

        private void OnGameWon()
        {
            LevelFinished?.Invoke();
            GameWon?.Invoke();
        }

        private void OnGameLost()
        {
            LevelFinished?.Invoke();
            GameLost?.Invoke();
        }

        private void OnLevelPrepared()
        {
            LevelPrepared?.Invoke();
        }

        private void OnLevelStarted()
        {
            LevelStarted?.Invoke();
        }

        private void OnLevelResumed()
        {
            LevelResumed?.Invoke();
        }

        private void OnRewardPressed()
        {
            RewardSelected?.Invoke();
        }

        private void OnRestartLevelPressed()
        {
            RestartLevelSelected?.Invoke();
        }

        private void OnNextLevePressed()
        {
            NextLevelSelected?.Invoke();
        }

        private void OnPlanetLaunched()
        {
            PlanetLaunched?.Invoke();
        }

        private void OnPlanetCreated(Planet planet)
        {
            planet.Merged += OnPlanetMerged;
            planet.PlanetCollided += OnPlanetCollide;
            planet.WallCollided += OnWallCollide;
            planet.Splitted += OnPlanetSplitted;

            PlanetCreated?.Invoke(planet);
        }

        private void OnPlanetReleased(Planet planet)
        {
            planet.Merged -= OnPlanetMerged;
            planet.PlanetCollided -= OnPlanetCollide;
            planet.WallCollided -= OnWallCollide;
            planet.Splitted -= OnPlanetSplitted;

            PlanetReleased?.Invoke(planet);
        }

        private void OnPlanetSplitted(Planet planet)
        {
            PlanetSplitted?.Invoke(planet);
        }

        private void OnPlanetMerged(Planet planet)
        {
            PlanetMerged?.Invoke(planet);
        }

        private void OnWallCollide(Vector2 atPoint)
        {
            WallCollided?.Invoke(atPoint);
        }

        private void OnPlanetCollide(Vector2 atPoint)
        {
            PlanetCollided?.Invoke(atPoint);
        }
    }
}