using Cysharp.Threading.Tasks;
using PlanetMerge.Gameloop.Presenter;
using PlanetMerge.Systems.Data;
using PlanetMerge.Systems.Tutorial;

namespace PlanetMerge.Gameloop.States
{
    public class StartLevelState
    {
        private readonly TutorialSystem _tutorialSystem;
        private readonly StartLevelPresenter _startLevelPresenter;
        private readonly IReadOnlyPlayerData _playerData;

        public StartLevelState(IReadOnlyPlayerData playerData, TutorialSystem tutorialController, StartLevelPresenter startLevelPresenter)
        {
            _playerData = playerData;
            _tutorialSystem = tutorialController;
            _startLevelPresenter = startLevelPresenter;
        }

        public async UniTask StartLevelAsync()
        {
            if (_playerData.Level == Constants.TutorialLevel)
            {
                _startLevelPresenter.HideWindows();
                _tutorialSystem.RunTutorialAsync().Forget();
            }
            else
            {
                await _startLevelPresenter.StartLevelAsync();
            }
        }

        public void ResumeLevel()
        {
            _startLevelPresenter.ResumeLevel();
        }
    }
}