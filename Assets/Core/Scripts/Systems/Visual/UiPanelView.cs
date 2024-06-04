using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UiPanelView : AppearingEntity
{
    [SerializeField] private float _tweenDuration = 1f;
    [SerializeField] private float _targetScale = 1.3f;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _middlePosition = Vector3.zero;
    [SerializeField] private Ease _ease;

    private readonly float _minAlpha = 0f;
    private readonly float _maxAlpha = 1f;

    private Vector3 _originPosition;
    private Vector3 _originScale;
    private CanvasGroup _canvasGroup;

    protected override void OnAwake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _originPosition = transform.position;
        _originScale = transform.localScale;
    }

    protected override void OnResetView()
    {
        transform.position = _startPosition;
        transform.localScale = _originScale;
        _canvasGroup.alpha = _maxAlpha;
    }

    public override async UniTask AppearAsync()
    {
        await UniTask.WhenAll(
            transform.DOMove(_middlePosition, _tweenDuration).SetEase(_ease).ToUniTask(),
            transform.DOScale(_targetScale, _tweenDuration).SetEase(_ease).ToUniTask());

        await UniTask.WhenAll(
            transform.DOScale(_originScale, _tweenDuration).SetEase(_ease).ToUniTask(),
            transform.DOMove(_originPosition, _tweenDuration).SetEase(_ease).ToUniTask());
    }

    public override async UniTask DisapearAsync()
    {
        await _canvasGroup.DOFade(_minAlpha, _tweenDuration);
    }
}
