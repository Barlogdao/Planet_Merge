using PlanetMerge.Planets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsOnLevel : MonoBehaviour
{
    private IPlanetStatusNotifier _planetStatusNotifier;
    private List<Planet> _planets = new();

    public void Initialize(IPlanetStatusNotifier planetStatusNotifier)
    {
        _planetStatusNotifier = planetStatusNotifier;

        _planetStatusNotifier.PlanetCreated += OnPlanetCreated;
        _planetStatusNotifier.PlanetReleased += OnPlanetReleased;
    }

    private void OnDestroy()
    {
        _planetStatusNotifier.PlanetCreated -= OnPlanetCreated;
        _planetStatusNotifier.PlanetReleased -= OnPlanetReleased;
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
        foreach (Planet planet in _planets)
        {
            planet.Release();
        }
    }
}
