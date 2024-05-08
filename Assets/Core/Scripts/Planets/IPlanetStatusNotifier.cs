using System;

namespace PlanetMerge.Planets
{
    public interface IPlanetStatusNotifier
    {
        event Action<Planet> PlanetCreated;
        event Action<Planet> PlanetReleased;
    }
}