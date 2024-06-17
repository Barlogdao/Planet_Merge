namespace PlanetMerge.Gameloop.States
{
    public class LevelStates
    {
        public LevelStates(
            PrepareLevelState prepareLevelState,
            StartLevelState startLevelState,
            EndLevelState endLevelState)
        {
            PrepareLevelState = prepareLevelState;
            StartLevelState = startLevelState;
            EndLevelState = endLevelState;
        }

        public PrepareLevelState PrepareLevelState { get; }
        public StartLevelState StartLevelState { get; }
        public EndLevelState EndLevelState { get; }
    }
}