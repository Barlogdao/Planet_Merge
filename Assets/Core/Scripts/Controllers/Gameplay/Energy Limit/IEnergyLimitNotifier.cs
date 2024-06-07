using System;

namespace PlanetMerge.Systems
{
    public interface IEnergyLimitNotifier
    {
        event Action LimitExpired;
        event Action<int> LimitChanged;
    }
}