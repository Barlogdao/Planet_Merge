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

    [SerializeField] private float _tweenDuration = 1f;
    [SerializeField] private float _targetScale = 1.3f;
    [SerializeField] private Vector3 _targetPosition = Vector3.zero;
    [SerializeField] private Ease _ease;

    private PlanetLimitHandler _planetLimitHandler;
    private LevelGoalHandler _levelGoalHandler;

    private Vector3 _originPosition;
    private Vector3 _originScale;

    private void Awake()
    {
        _originPosition = transform.position;
        _originScale = transform.localScale;
    }

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

        _limitPanel.Prepare(playerData.PlanetRank);
        _goalPanel.Prepare(planetGoalRank);
        _levelValue.text = playerData.Level.ToString();
    }

    public async UniTask Animate()
    {
        transform.position = _targetPosition;
        transform.localScale = _originScale * _targetScale;
        await UniTask.WaitForSeconds(_tweenDuration);
        await transform.DOScale(_originScale, _tweenDuration).SetEase(_ease);
        await transform.DOMove(_originPosition,_tweenDuration).SetEase(_ease);
    }
}