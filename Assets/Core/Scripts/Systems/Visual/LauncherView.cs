using Cysharp.Threading.Tasks;
using UnityEngine;

public class LauncherView : AppearEntity
{
    [SerializeField] private MovingAppearEntity _spaceShip;
    [SerializeField] private FadingAppearEntity _forceField;
    [SerializeField] private MovingAppearEntity _planetView;

    protected override void OnResetView()
    {
        _spaceShip.ResetView();
        _forceField.ResetView();
        _planetView.ResetView();
    }

    public override async UniTask AppearAsync()
    {
        await _spaceShip.AppearAsync();

        await UniTask.WhenAll(
            _forceField.AppearAsync(),
            _planetView.AppearAsync());
    }

    public override async UniTask DisapearAsync()
    {
        await UniTask.WhenAll(
            _forceField.DisapearAsync(),
            _planetView.DisapearAsync());
   
        await _spaceShip.DisapearAsync();
    }
}
