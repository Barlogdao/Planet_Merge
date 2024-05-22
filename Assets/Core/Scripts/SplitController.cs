using PlanetMerge.Planets;
using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class SplitController : MonoBehaviour
{
    [SerializeField, Range(10, 50)] private int _maxPlanetsOnSplit = 30;
    private GameEventMediator _gameEventMediator;
    private PlanetSpawner _planetSpawner;
    private LevelPlanets _levelPlanets;

    public void Initialize(GameEventMediator gameEventMediator, PlanetSpawner planetSpawner, LevelPlanets levelPlanets)
    {
        _gameEventMediator = gameEventMediator;
        _planetSpawner = planetSpawner;
        _levelPlanets = levelPlanets;

        _gameEventMediator.PlanetSplitted += OnPlanetSplitted;
        _gameEventMediator.GameWon += OnGameWon;
    }

    private void OnGameWon()
    {
        foreach (Planet planet in _levelPlanets.Planets)
        {
            planet.Split().Forget();
        }
    }

    private void OnDestroy()
    {
        _gameEventMediator.PlanetSplitted -= OnPlanetSplitted;
        _gameEventMediator.GameWon -= OnGameWon;
    }

    private void OnPlanetSplitted(Planet planet)
    {
        if (_levelPlanets.PlanetsAmount > _maxPlanetsOnSplit)
            return;

        Vector2 position = RandomizePosition(planet.transform.position);
        Planet splittedPlanet = _planetSpawner.Spawn(position, planet.Rank);

        splittedPlanet.Split().Forget();
    }


    private Vector2 RandomizePosition(Vector2 position)
    {
        return Random.insideUnitCircle + position;
    }
}
