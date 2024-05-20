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
    private LevelGenerator _levelGenerator;
    private GameUI _gameUI;

    public event Action<Planet> PlanetCreated;
    public event Action<Planet> PlanetReleased;

    public event Action<Planet> PlanetMerged;
    public event Action<Vector2> PlanetCollided;

    public event Action LevelCreated;
    public event Action LevelStaretd;
    public event Action LevelFinished;

    public event Action GameLost;
    public event Action GameWinned;

    public event Action NextLevelSelected;
    public event Action RestartLevelSelected;
    public event Action RewardSelected;

    public void Initialize(IPlanetStatusNotifier planetStatusNotifier, GameOverHandler gameOverHandler, LevelGenerator levelGenerator, GameUI gameUI)
    {
        _planetStatusNotifier = planetStatusNotifier;
        _gameOverHandler = gameOverHandler;
        _levelGenerator = levelGenerator;
        _gameUI = gameUI;

        _planetStatusNotifier.PlanetCreated += OnPlanetCreated;
        _planetStatusNotifier.PlanetReleased += OnPlanetReleased;

        _gameOverHandler.GameWinned += OnGameWinned;
        _gameOverHandler.GameLost += OnGameLost;

        _levelGenerator.LevelCreated += OnLevelCreated;

        _gameUI.NextLevelPressed += OnNextLevePressed;
        _gameUI.RestartLevelPressed += OnRestareLevelPressed;
        _gameUI.RewardPressed += OnRewardPressed;
    }


    private void OnDestroy()
    {
        _planetStatusNotifier.PlanetCreated -= OnPlanetCreated;
        _planetStatusNotifier.PlanetReleased -= OnPlanetReleased;

        _gameOverHandler.GameWinned -= OnGameWinned;
        _gameOverHandler.GameLost -= OnGameLost;

        _levelGenerator.LevelCreated += OnLevelCreated;

        _gameUI.NextLevelPressed -= OnNextLevePressed;
        _gameUI.RestartLevelPressed -= OnRestareLevelPressed;
        _gameUI.RewardPressed -= OnRewardPressed;
    }

    private void OnGameWinned()
    {
        LevelFinished?.Invoke();
        GameWinned?.Invoke();
    }

    private void OnLevelCreated()
    {
        LevelCreated?.Invoke();
    }
    private void OnGameLost()
    {
        LevelFinished?.Invoke();
        GameLost?.Invoke();
    }

    private void OnPlanetCreated(Planet planet)
    {
        planet.Merged += OnPlanetMerged;
        planet.Collided += OnPlanetCollide;
        PlanetCreated?.Invoke(planet);
    }

    private void OnPlanetReleased(Planet planet)
    {
        planet.Merged -= OnPlanetMerged;
        planet.Collided -= OnPlanetCollide;
        PlanetReleased?.Invoke(planet);
    }

    private void OnPlanetMerged(Planet planet)
    {
        PlanetMerged?.Invoke(planet);
    }

    private void OnPlanetCollide(Vector2 atPoint)
    {
        PlanetCollided?.Invoke(atPoint);
    }


    private void OnRewardPressed() => RewardSelected?.Invoke();

    private void OnRestareLevelPressed() => RestartLevelSelected?.Invoke();

    private void OnNextLevePressed() => NextLevelSelected?.Invoke();
}
