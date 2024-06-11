using UnityEngine;

namespace PlanetMerge.Systems.Gameplay.PlanetLaunching
{
    public interface ILaunchPoint
    {
        public Vector2 LaunchPosition { get; }
    }
}