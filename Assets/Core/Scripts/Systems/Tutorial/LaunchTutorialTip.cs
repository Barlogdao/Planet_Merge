namespace PlanetMerge.Systems.Tutorial
{
    public abstract class LaunchTutorialTip : TutorialTip
    {
        private bool _isPlanetLauched = false;
        protected bool IsPlanetLauched => _isPlanetLauched;

        protected override void Activate()
        {
            base.Activate();
            _isPlanetLauched = false;

            TutorialController.PlanetLaunched += OnLaunch;
        }

        protected override void Deactivate()
        {
            base.Deactivate();
            TutorialController.PlanetLaunched -= OnLaunch;
        }

        private void OnLaunch()
        {
            _isPlanetLauched = true;
        }
    }
}