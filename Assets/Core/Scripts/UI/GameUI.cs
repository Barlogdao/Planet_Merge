using UnityEngine;
using TMPro;
using PlanetMerge.Systems;
using System;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace PlanetMerge.UI
{
    public class GameUi : MonoBehaviour, IGameUiEvents
    {
        [SerializeField] private VictoryWindow _victoryWindow;
        [SerializeField] private AppearWindow _looseWindow;
        [SerializeField] private LevelScoreWindow _levelScoreWindow;
        [SerializeField] private FadingBackground _whiteScreen;

        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _resetLevelButton;
        [SerializeField] private Button _rewardButton;

        private UiPanel _uiPanel;

        public event Action NextLevelPressed;
        public event Action RestartLevelPressed;
        public event Action RewardPressed;

        public void Initialize(UiPanel uiPanel)
        {
            _uiPanel = uiPanel;

            _victoryWindow.Initialize();
            _whiteScreen.Initialize();
            _levelScoreWindow.Initialize();

            _nextLevelButton.onClick.AddListener(OnNextLevelPressed);
            _resetLevelButton.onClick.AddListener(OnResetLevelPressed);
            _rewardButton.onClick.AddListener(OnRewardPressed);

        }

        private void OnDestroy()
        {
            _nextLevelButton.onClick.RemoveListener(OnNextLevelPressed);
            _resetLevelButton.onClick.RemoveListener(OnResetLevelPressed);
            _rewardButton.onClick.RemoveListener(OnRewardPressed);
        }

        public void Prepare(IReadOnlyPlayerData playerData)
        {
            _uiPanel.Prepare(playerData);
        }

        public async UniTask ShowLevelScoreAsync(int levelScore)
        {
            await _levelScoreWindow.ShowScoreAsync(levelScore);
        }

        public async UniTask RunLevelAsync()
        {
            await UniTask.WhenAll(_uiPanel.BeginAnimateAsync(), _whiteScreen.UnfadeAsync());
            await UniTask.WhenAll(_uiPanel.EndAnimateAsync(), _whiteScreen.FadeAsync()); 
        }

        private void OnRewardPressed()
        {
            RewardPressed?.Invoke();
        }

        private void OnResetLevelPressed()
        {
            RestartLevelPressed?.Invoke();
        }

        private void OnNextLevelPressed()
        {
            NextLevelPressed?.Invoke();
        }

        public void Hide()
        {
            _victoryWindow.Hide();
            _looseWindow.Hide();
        }

        public void ShowVictoryWindow(IReadOnlyPlayerData playerData)
        {
            _victoryWindow.AppearAsync().Forget();
            _victoryWindow.Prepare(playerData);
        }

        public void ShowProgress(IReadOnlyPlayerData playerData)
        {
            _victoryWindow.ShowProgress(playerData);
        }

        public void ShowLooseWindow()
        {
            _looseWindow.AppearAsync().Forget();
        }
    }
}