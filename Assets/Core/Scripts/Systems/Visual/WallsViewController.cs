using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks.Linq;
using DG.Tweening;

public class WallsViewController : AppearEntity
{
    private const string DissolveProperty = "_FullGlowDissolveFade";
    [SerializeField] private SpriteRenderer[] _spriteRenderers;
    [SerializeField] private float _dissolveDuration = 0.2f;

    private int _fadePropertyID;
    private Material[] _materials;
    private float _dissolveValue = 0f;
    private float _undissolveValue = 1f;
    protected override void OnAwake()
    {
        _materials = _spriteRenderers.Select(x => x.material).ToArray();
        _fadePropertyID = Shader.PropertyToID(DissolveProperty);
    }

    protected override void OnResetView()
    {
        foreach (Material material in _materials)
        {
            material.SetFloat(_fadePropertyID,_dissolveValue);
        }
    }

    public override async UniTask AppearAsync()
    {
        foreach (Material material in _materials)
        {
            await material.DOFloat(_undissolveValue, _fadePropertyID, _dissolveDuration);
        }
    }

    public override async UniTask DisapearAsync()
    {
        foreach (Material material in _materials)
        {
            await material.DOFloat(_dissolveValue, _fadePropertyID, _dissolveDuration);
        }
    }

}
