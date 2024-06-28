using Cysharp.Threading.Tasks;
using PlanetMerge.Loop.View;
using PlanetMerge.Systems.Data;
using PlanetMerge.Systems.Tutorial;
using PlanetMerge.Utils;

namespace PlanetMerge.Loop.States
{
    public class StartLevelState
    {
        private readonly TutorialSystem _tutorialSystem;
        private readonly StartLevelView _startLevelView;
        private readonly IReadOnlyPlayerData _playerData;

        public StartLevelState(IReadOnlyPlayerData playerData, TutorialSystem tutorialController, StartLevelView startLevelView)
        {
            _playerData = playerData;
            _tutorialSystem = tutorialController;
            _startLevelView = startLevelView;
        }

        public async UniTask StartLevelAsync()
        {
            if (_playerData.Level == Constants.TutorialLevel)
            {
                _startLevelView.HideWindows();
                _tutorialSystem.RunTutorialAsync().Forget();
            }
            else
            {
                await _startLevelView.StartLevelAsync();
            }
        }

        public void ResumeLevel()
        {
            _startLevelView.ResumeLevel();
        }
    }
}