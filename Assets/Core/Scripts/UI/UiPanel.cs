using PlanetMerge.Systems.Data;
using PlanetMerge.Systems.Gameplay;
using PlanetMerge.UI.View;
using TMPro;
using UnityEngine;

public class UiPanel : MonoBehaviour
{
    [SerializeField] private LimitPanel _limitPanel;
    [SerializeField] private GoalPanel _goalPanel;
    [SerializeField] private TMP_Text _levelValue;

    private IEnergyLimitNotifier _energyLimitNotifier;
    private ILevelGoalNotifier _levelGoalNotifier;

    public void Initialize(IEnergyLimitNotifier energyLimitNotifier, ILevelGoalNotifier levelGoalNotifier)
    {
        _energyLimitNotifier = energyLimitNotifier;
        _levelGoalNotifier = levelGoalNotifier;

        _limitPanel.Initialize(_energyLimitNotifier);
        _goalPanel.Initialize(_levelGoalNotifier);
    }

    public void Prepare(IReadOnlyPlayerData playerData)
    {
        int planetGoalRank = _levelGoalNotifier.PlanetGoalRank;

        _goalPanel.Prepare(planetGoalRank);
        _levelValue.text = playerData.Level.ToString();
    }
}
