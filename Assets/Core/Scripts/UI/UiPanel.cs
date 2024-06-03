using Cysharp.Threading.Tasks;
using DG.Tweening;
using PlanetMerge.Systems;
using PlanetMerge.UI;
using TMPro;
using UnityEngine;

public class UiPanel : MonoBehaviour
{
    [SerializeField] private LimitPanel _limitPanel;
    [SerializeField] private GoalPanel _goalPanel;

    [SerializeField] private TMP_Text _levelValue;

    private PlanetLimitHandler _planetLimitHandler;
    private LevelGoalHandler _levelGoalHandler;
    public void Initialize(PlanetLimitHandler planetLimitHandler, LevelGoalHandler levelGoalHandler)
    {
        _planetLimitHandler = planetLimitHandler;
        _levelGoalHandler = levelGoalHandler;

        _limitPanel.Initialize(_planetLimitHandler);
        _goalPanel.Initialize(_levelGoalHandler);
    }

    public void Prepare(IReadOnlyPlayerData playerData)
    {
        int planetGoalRank = _levelGoalHandler.PlanetGoalRank;

        _goalPanel.Prepare(planetGoalRank);
        _levelValue.text = playerData.Level.ToString();
    }
}
