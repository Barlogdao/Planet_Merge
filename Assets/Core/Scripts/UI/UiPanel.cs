using Cysharp.Threading.Tasks;
using DG.Tweening;
using PlanetMerge.Systems;
using PlanetMerge.UI;
using PlanetMerge.Utils.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiPanel : MonoBehaviour
{
    [SerializeField] private LimitPanel _limitPanel;
    [SerializeField] private GoalPanel _goalPanel;
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _levelValue;

    [SerializeField] private float _tweenDuration = 1f;
    [SerializeField] private float _fadeDuration = 0.5f;
    [SerializeField] private float _targetScale = 1.3f;
    [SerializeField] private Vector3 _targetPosition = Vector3.zero;
    [SerializeField] private Ease _ease;

    private PlanetLimitHandler _planetLimitHandler;
    private LevelGoalHandler _levelGoalHandler;

    private Vector3 _originPosition;
    private Vector3 _originScale;
    private float _maxAlphaValue = 1f;
    private float _minAlphaValue = 0f;

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
        _background.color = _background.color.WithAlpha(_minAlphaValue);
    }

    public void Prepare(IReadOnlyPlayerData playerData)
    {
        int planetGoalRank = _levelGoalHandler.PlanetGoalRank;

        _goalPanel.Prepare(planetGoalRank);
        _levelValue.text = playerData.Level.ToString();
    }

    public async UniTask Animate()
    {
        _background.color = _background.color.WithAlpha(_maxAlphaValue);
        transform.position = _targetPosition;
        transform.localScale = _originScale * _targetScale;
        await UniTask.WaitForSeconds(_tweenDuration);
        _background.DOFade(_minAlphaValue, _fadeDuration);
        await transform.DOScale(_originScale, _tweenDuration).SetEase(_ease);
        await transform.DOMove(_originPosition,_tweenDuration).SetEase(_ease);
    }
}
