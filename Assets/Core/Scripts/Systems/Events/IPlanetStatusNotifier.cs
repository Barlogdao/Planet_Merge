using PlanetMerge.Entities.Planets;
using System;

namespace PlanetMerge.Systems.Events
{
    public interface IPlanetStatusNotifier
    {
        event Action<Planet> PlanetCreated;

        event Action<Planet> PlanetReleased;
    }
}