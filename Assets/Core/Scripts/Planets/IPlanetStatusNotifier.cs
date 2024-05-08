using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetMerge.Planets
{
    public interface IPlanetStatusNotifier
    {
        event Action<Planet> PlanetCreated;
        event Action<Planet> PlanetReleased;
    }
}