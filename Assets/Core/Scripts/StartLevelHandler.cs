using Cysharp.Threading.Tasks;
using PlanetMerge.Systems.Tutorial;
using PlanetMerge.UI;

public class StartLevelHandler
{
    private GameUi _gameUi;
    private LevelPreparer _levelPreparer;
    private TutorialController _tutorialController;

    public StartLevelHandler(GameUi gameUi, LevelPreparer levelPreparer, TutorialController tutorialController)
    {
        _gameUi = gameUi;
        _levelPreparer = levelPreparer;
        _tutorialController = tutorialController;
    }

    public void PrepareLevel(IReadOnlyPlayerData playerData)
    {
        _levelPreparer.Prepare(playerData);
    }

    public async UniTask StartLevel(int level)
    {
        _gameUi.Hide();
        await _gameUi.Animate();
        if (level == Constants.TutorialLevel)
        {
            _tutorialController.ShowTitorial().Forget();
        }
    }

    public void ResumeLevel()
    {
        _gameUi.Hide();
    }
}
