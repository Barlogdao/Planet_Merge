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
        [SerializeField] private RectTransform _looseWindow;
        [SerializeField] private LevelScoreWindow _levelScoreWindow;

        [SerializeField] private LimitPanel _limitPanel;
        [SerializeField] private GoalPanel _goalPanel;

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
            _levelScoreWindow.Hide();

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

        public async UniTask ShowLevelScore(int levelScore)
        {
            _levelScoreWindow.Show();
            await _levelScoreWindow.Animate(levelScore);
            _levelScoreWindow.Hide();
        }

        public async UniTask Animate()
        {
            await _uiPanel.Animate();
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
            _looseWindow.gameObject.SetActive(false);
        }

        public void ShowVictoryWindow(IReadOnlyPlayerData playerData)
        {
            _victoryWindow.Show();
            _victoryWindow.Prepare(playerData);
        }

        public void ShowProgress(IReadOnlyPlayerData playerData)
        {
            _victoryWindow.ShowProgress(playerData);
        }

        public void ShowLooseWindow()
        {
            _looseWindow.gameObject.SetActive(true);
        }
    }
}