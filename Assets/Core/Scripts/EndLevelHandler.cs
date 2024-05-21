using PlanetMerge.SDK.Yandex;
using PlanetMerge.UI;
using UnityEngine;

public class EndLevelHandler
{
    private GameUi _gameUI;
    private RewardHandler _rewardHandler;

    public EndLevelHandler(GameUi gameUI, RewardHandler rewardHandler)
    {
        _gameUI = gameUI;
        _rewardHandler = rewardHandler;
    }

    public void Win()
    {
        _gameUI.ShowVictoryWindow();
    }

    public void Loose()
    {
        _gameUI.ShowLooseWindow();
    }

    public void AddReward()
    {
        _rewardHandler.AddReward();
    }
}
