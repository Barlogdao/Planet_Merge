using System;

namespace PlanetMerge.Systems.Gameplay
{
    public interface IEnergyLimitNotifier
    {
        event Action LimitExpired;
        event Action<int> LimitChanged;
    }
}