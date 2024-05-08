using PlanetMerge.Planets;
using PlanetMerge.Systems.Events;
using System;
using UnityEngine;

public class GameEventBus : MonoBehaviour, IPlanetEvents
{
    private IPlanetStatusNotifier _planetStatusNotifier;

    public event Action<Planet> PlanetMerged;
    public event Action PlanetCollided;

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
        planet.Merged += OnPlanetMerged;
        planet.Collided += OnPlanetCollide;
    }

    private void OnPlanetReleased(Planet planet)
    {
        planet.Merged -= OnPlanetMerged;
    }

    private void OnPlanetMerged(Planet planet)
    {
        PlanetMerged?.Invoke(planet);
    }

    private void OnPlanetCollide()
    {
        PlanetCollided?.Invoke();
    }
}
