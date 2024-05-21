using PlanetMerge.Planets;
using System.Collections.Generic;
using UnityEngine;

public class LevelPlanets : MonoBehaviour
{
    private GameEventMediator _gameEventMediator;
    private List<Planet> _planets = new();



    public void Initialize(GameEventMediator gameEventMediator)
    {
        _gameEventMediator = gameEventMediator;

        _gameEventMediator.PlanetCreated += OnPlanetCreated;
        _gameEventMediator.PlanetReleased += OnPlanetReleased;
    }

    private void OnDestroy()
    {
        _gameEventMediator.PlanetCreated -= OnPlanetCreated;
        _gameEventMediator.PlanetReleased -= OnPlanetReleased;
    }

    private void OnPlanetCreated(Planet planet)
    {
        _planets.Add(planet);
    }

    private void OnPlanetReleased(Planet planet)
    {
        _planets.Remove(planet);
    }

    public void Clear()
    {
        List<Planet> planets = new List<Planet>(_planets);

        foreach (Planet planet in planets)
        {
            planet.Release();
        }
    }
}
