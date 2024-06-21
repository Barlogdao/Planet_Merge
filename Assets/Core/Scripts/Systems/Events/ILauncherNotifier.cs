using System;

namespace PlanetMerge.Systems.Events
{
    public interface ILauncherNotifier
    {
        event Action PlanetLaunched;
    }
}