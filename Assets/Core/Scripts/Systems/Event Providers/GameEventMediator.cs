using PlanetMerge.Planets;
using PlanetMerge.Systems;
using PlanetMerge.Systems.Events;
using PlanetMerge.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEventMediator : MonoBehaviour, IPlanetEvents
{
    private IPlanetStatusNotifier _planetStatusNotifier;
    private GameOverHandler _gameOverHandler;
    private GameLoop _gameLoop;
    private IGameUiEvents _uiEvents;

    public event Action<Planet> PlanetCreated;
    public event Action<Planet> PlanetReleased;
    public event Action<Planet> PlanetSplitted;

    public event Action<Planet> PlanetMerged;
    public event Action<Vector2> PlanetCollided;

    public event Action LevelPrepared;
    public event Action LevelStarted;
    public event Action LevelResumed;
    public event Action LevelFinished;

    public event Action GameLost;
    public event Action GameWon;

    public event Action NextLevelSelected;
    public event Action RestartLevelSelected;
    public event Action RewardSelected;

    public void Initialize(IPlanetStatusNotifier planetStatusNotifier, GameOverHandler gameOverHandler, GameLoop gameLoop, IGameUiEvents uiEvents)
    {
        _planetStatusNotifier = planetStatusNotifier;
        _gameOverHandler = gameOverHandler;
        _gameLoop = gameLoop;
        _uiEvents = uiEvents;

        _planetStatusNotifier.PlanetCreated += OnPlanetCreated;
        _planetStatusNotifier.PlanetReleased += OnPlanetReleased;

        _gameOverHandler.GameWon += OnGameWon;
        _gameOverHandler.GameLost += OnGameLost;

        _gameLoop.LevelPrepared += OnLevelPrepared;
        _gameLoop.LevelStarted += OnLevelStarted;
        _gameLoop.LevelResumed += OnLevelResumed;

        _uiEvents.NextLevelPressed += OnNextLevePressed;
        _uiEvents.RestartLevelPressed += OnRestareLevelPressed;
        _uiEvents.RewardPressed += OnRewardPressed;
    }



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
        _uiEvents.RestartLevelPressed -= OnRestareLevelPressed;
        _uiEvents.RewardPressed -= OnRewardPressed;
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

    private void OnLevelPrepared() => LevelPrepared?.Invoke();
    private void OnLevelStarted() => LevelStarted?.Invoke();
    private void OnLevelResumed() => LevelResumed?.Invoke();

    private void OnRewardPressed() => RewardSelected?.Invoke();
    private void OnRestareLevelPressed() => RestartLevelSelected?.Invoke();
    private void OnNextLevePressed() => NextLevelSelected?.Invoke();

    private void OnPlanetCreated(Planet planet)
    {
        planet.Merged += OnPlanetMerged;
        planet.Collided += OnPlanetCollide;
        planet.Splitted += OnPlanetSplitted;

        PlanetCreated?.Invoke(planet);
    }

    private void OnPlanetReleased(Planet planet)
    {
        planet.Merged -= OnPlanetMerged;
        planet.Collided -= OnPlanetCollide;
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

    private void OnPlanetCollide(Vector2 atPoint)
    {
        PlanetCollided?.Invoke(atPoint);
    }
}