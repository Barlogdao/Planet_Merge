using System;

namespace PlanetMerge.UI
{
    public interface IGameUiEvents
    {
        event Action NextLevelPressed;
        event Action RestartLevelPressed;
        event Action RewardPressed;
    }
}