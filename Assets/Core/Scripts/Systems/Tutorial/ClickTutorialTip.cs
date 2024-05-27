namespace PlanetMerge.Systems.Tutorial
{
    public abstract class ClickTutorialTip : TutorialTip
    {
        private bool _isClicked = false;
        protected bool IsClicked => _isClicked;

        protected override void Activate()
        {
            base.Activate();

            TutorialController.IsClicked += OnClick;
        }
        protected override void Deactivate()
        {
            base.Deactivate();

            TutorialController.IsClicked -= OnClick;
        }

        private void OnClick()
        {
            _isClicked = true;
        }
    }
}