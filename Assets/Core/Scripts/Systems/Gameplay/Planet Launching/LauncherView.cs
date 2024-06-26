using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PlanetMerge.Systems.PlanetLaunching
{
    public class LauncherView : AppearingEntity
    {
        [SerializeField] private MovingAppearEntity _spaceShip;
        [SerializeField] private FadingAppearEntity _forceField;
        [SerializeField] private MovingAppearEntity _planetView;

        public override async UniTask AppearAsync()
        {
            await _spaceShip.AppearAsync();

            await UniTask.WhenAll(
                _forceField.AppearAsync(),
                _planetView.AppearAsync());
        }

        public override async UniTask DisappearAsync()
        {
            await UniTask.WhenAll(
                _forceField.DisappearAsync(),
                _planetView.DisappearAsync());

            await _spaceShip.DisappearAsync();
        }

        protected override void OnResetView()
        {
            _spaceShip.ResetView();
            _forceField.ResetView();
            _planetView.ResetView();
        }
    }
}