using System;

namespace PlanetMerge.Systems.Events
{
    public interface IGameUiEvents
    {
        event Action NextLevelPressed;
        event Action RestartLevelPressed;
        event Action RewardPressed;
    }
}