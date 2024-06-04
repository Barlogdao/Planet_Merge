using Cysharp.Threading.Tasks;
using PlanetMerge.Systems.Tutorial;
using PlanetMerge.Systems.Visual;

public class StartLevelHandler
{
    private readonly TutorialSystem _tutorialSystem;
    private readonly StartLevelPresenter _startLevelPresenter;
    private readonly IReadOnlyPlayerData _playerData;

    public StartLevelHandler(IReadOnlyPlayerData playerData, TutorialSystem tutorialController, StartLevelPresenter startLevelPresenter)
    {
        _playerData = playerData;
        _tutorialSystem = tutorialController;
        _startLevelPresenter = startLevelPresenter;
    }

    public async UniTask StartLevelAsync()
    {
        if (_playerData.Level == Constants.TutorialLevel)
        {
            _tutorialSystem.RunTutorialAsync().Forget();
        }
        else
        {
            await _startLevelPresenter.StartLevelAsync();
        }
    }

    public void ResumeLevel()
    {
        _startLevelPresenter.ResetLevel();
    }
}
