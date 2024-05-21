using PlanetMerge.Planets;
using System;
using UnityEngine;

namespace PlanetMerge.Systems.Events
{
    public interface IPlanetEvents
    {
        event Action<Planet> PlanetMerged;
        event Action<Vector2> PlanetCollided;
    }
}