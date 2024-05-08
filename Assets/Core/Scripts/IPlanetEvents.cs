using PlanetMerge.Planets;
using System;

namespace PlanetMerge.Systems.Events
{
    public interface IPlanetEvents
    {
        event Action<Planet> PlanetMerged;
        event Action PlanetCollided;
    }
}