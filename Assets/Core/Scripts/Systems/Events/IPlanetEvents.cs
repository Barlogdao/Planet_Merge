using PlanetMerge.Entities.Planets;
using System;
using UnityEngine;

namespace PlanetMerge.Systems.Events
{
    public interface IPlanetEvents
    {
        event Action<Planet> PlanetMerged;
        event Action<Vector2> PlanetCollided;
        event Action<Vector2> WallCollided;
    }
}