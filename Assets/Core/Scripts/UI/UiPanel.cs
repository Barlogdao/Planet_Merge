using PlanetMerge.Systems;
using PlanetMerge.UI;
using TMPro;
using UnityEngine;

public class UiPanel : MonoBehaviour
{
    [SerializeField] private LimitPanel _limitPanel;
    [SerializeField] private GoalPanel _goalPanel;
    [SerializeField] private TMP_Text _levelValue;

    private EnergyLimitHandler _energyLimitHandler;
    private LevelGoalHandler _levelGoalHandler;

    public void Initialize(EnergyLimitHandler energyLimitHandler, LevelGoalHandler levelGoalHandler)
    {
        _energyLimitHandler = energyLimitHandler;
        _levelGoalHandler = levelGoalHandler;

        _limitPanel.Initialize(_energyLimitHandler);
        _goalPanel.Initialize(_levelGoalHandler);
    }

    public void Prepare(IReadOnlyPlayerData playerData)
    {
        int planetGoalRank = _levelGoalHandler.PlanetGoalRank;

        _goalPanel.Prepare(planetGoalRank);
        _levelValue.text = playerData.Level.ToString();
    }
}
