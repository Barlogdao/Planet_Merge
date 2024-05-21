using Cysharp.Threading.Tasks;
using PlanetMerge.UI;

public class StartLevelHandler
{
    private GameUi _gameUi;
    private LevelPreparer _levelPreparer;

    public StartLevelHandler(GameUi gameUi, LevelPreparer levelPreparer)
    {
        _gameUi = gameUi;
        _levelPreparer = levelPreparer;
    }

    public void PrepareLevel(IReadOnlyPlayerData playerData)
    {
        _levelPreparer.Prepare(playerData);
    }

    public async UniTask StartLevel(int level)
    {
        _gameUi.Hide();
        await _gameUi.Animate();
    }

    public void ResumeLevel()
    {
        _gameUi.Hide();
    }
}
