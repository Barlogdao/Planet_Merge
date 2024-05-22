using PlanetMerge.Planets;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelPlanets : MonoBehaviour
{
    private GameEventMediator _gameEventMediator;
    private PlanetSpawner _planetSpawner;
    private List<Planet> _planets = new();
    private int _maxPlanetsOnSplit = 30;


    public IEnumerable<Planet> Planets => _planets;
    public void Initialize(GameEventMediator gameEventMediator, PlanetSpawner planetSpawner)
    {
        _gameEventMediator = gameEventMediator;
        _planetSpawner = planetSpawner;

        _gameEventMediator.PlanetCreated += OnPlanetCreated;
        _gameEventMediator.PlanetReleased += OnPlanetReleased;
        _gameEventMediator.PlanetSplitted += OnPlanetSplitted;
    }

    private void OnDestroy()
    {
        _gameEventMediator.PlanetCreated -= OnPlanetCreated;
        _gameEventMediator.PlanetReleased -= OnPlanetReleased;
        _gameEventMediator.PlanetSplitted -= OnPlanetSplitted;
    }


    public void Clear()
    {
        List<Planet> planets = new List<Planet>(_planets);

        foreach (Planet planet in planets)
        {
            planet.Release();
        }
    }

    public void Split()
    {
        foreach (Planet planet in _planets)
        {
            planet.Split().Forget();
        }
    }



    private void OnPlanetCreated(Planet planet)
    {
        _planets.Add(planet);
    }

    private void OnPlanetReleased(Planet planet)
    {
        _planets.Remove(planet);
    }

    private void OnPlanetSplitted(Planet planet)
    {
        if (_planets.Count > _maxPlanetsOnSplit)
            return;

        Vector2 position = RandomizePosition(planet.transform.position);
        var splittedPlanet = _planetSpawner.Spawn(position, planet.Rank);

        splittedPlanet.Split().Forget();
    }

    private Vector2 RandomizePosition(Vector2 position)
    {
        return Random.insideUnitCircle + position;
    }
}
