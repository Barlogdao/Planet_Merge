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
        [SerializeField] private RectTransform _victoryWindow;
        [SerializeField] private RectTransform _looseWindow;

        [SerializeField] private LimitPanel _limitPanel;
        [SerializeField] private GoalPanel _goalPanel;
        [SerializeField] private TMP_Text _levelLabel;

       private UiPanel _uiPanel;

        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _resetLevelButton;
        [SerializeField] private Button _rewardButton;

        [SerializeField] private PlanetProgressionPanel _progressionPanel;

        public event Action NextLevelPressed;
        public event Action RestartLevelPressed;
        public event Action RewardPressed;

        public void Initialize(UiPanel uiPanel)
        {
            _uiPanel = uiPanel;

            _nextLevelButton.onClick.AddListener(OnNextLevelPressed);
            _resetLevelButton.onClick.AddListener(OnResetLevelPressed);
            _rewardButton.onClick.AddListener(OnRewardPressed);

            _progressionPanel.Initialize();
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
            _victoryWindow.gameObject.SetActive(false);
            _looseWindow.gameObject.SetActive(false);
        }

        public void ShowVictoryWindow(IReadOnlyPlayerData playerData)
        {
            _victoryWindow.gameObject.SetActive(true);
            _progressionPanel.Prepare(playerData.PlanetRank);
        }

        public void ShowProgress(IReadOnlyPlayerData playerData)
        {
            _progressionPanel.Set(playerData).Forget();
        }

        public void ShowLooseWindow()
        {
            _looseWindow.gameObject.SetActive(true);
        }
    }
}