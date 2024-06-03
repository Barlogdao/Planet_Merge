using Cysharp.Threading.Tasks;
using DG.Tweening;
using PlanetMerge.Utils.Extensions;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FadingAppearEntity : AppearEntity
{
    [SerializeField] private float _fadeDuration = 0.4f;
    [SerializeField] private Ease _ease;

    private SpriteRenderer _spriteRenderer;
    private float _minAlpha = 0f;
    private float _maxAlpha;

    public override async UniTask AppearAsync()
    {
        await _spriteRenderer.DOFade(_maxAlpha, _fadeDuration).SetEase(_ease);
    }

    public override async UniTask DisapearAsync()
    {
        await _spriteRenderer.DOFade(_minAlpha, _fadeDuration).SetEase(_ease);
    }

    protected override void OnResetView()
    {
        _spriteRenderer.color = _spriteRenderer.color.WithAlpha(_minAlpha);

    }

    protected override void OnAwake()
    {
       _spriteRenderer = GetComponent<SpriteRenderer>();
       _maxAlpha = _spriteRenderer.color.a;
    }
}
