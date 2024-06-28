using System;
using PlanetMerge.Entities.Planets;

namespace PlanetMerge.Systems.Events
{
    public interface IPlanetStatusNotifier
    {
        event Action<Planet> PlanetCreated;

        event Action<Planet> PlanetReleased;
    }
}