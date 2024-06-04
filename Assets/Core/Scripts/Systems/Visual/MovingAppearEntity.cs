using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class MovingAppearEntity : AppearingEntity
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private float _appearDuration = 0.4f;
    [SerializeField] private Ease _ease;

    private Vector3 _originPosition;

    protected Vector3 OriginPosition => _originPosition;
    protected Vector3 StartPosition => _startPosition;

    public override async UniTask AppearAsync()
    {
        await transform.DOMove(OriginPosition, _appearDuration).SetEase(_ease);
    }

    public override async UniTask DisapearAsync()
    {
        await transform.DOMove(StartPosition, _appearDuration).SetEase(_ease);
    }

    protected override void OnAwake()
    {
        _originPosition = transform.position;
    }

    protected override void OnResetView()
    {
        transform.position = _startPosition;
    }
}
