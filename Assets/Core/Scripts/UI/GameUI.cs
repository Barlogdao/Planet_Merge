using UnityEngine;
using TMPro;
using PlanetMerge.Systems;
using System;
using UnityEngine.UI;

namespace PlanetMerge.UI
{
    public class GameUi : MonoBehaviour, IGameUiEvents
    {
        [SerializeField] private RectTransform _victoryWindow;
        [SerializeField] private RectTransform _looseWindow;

        [SerializeField] private LimitPanel _limitPanel;
        [SerializeField] private GoalPanel _goalPanel;
        [SerializeField] private TMP_Text _levelLabel;

        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _resetLevelButton;
        [SerializeField] private Button _rewardButton;


        public event Action NextLevelPressed;
        public event Action RestartLevelPressed;
        public event Action RewardPressed;

  
        private PlanetLimitHandler _planetLimitHandler;
        private LevelGoalHandler _levelGoalHandler;

        public void Initialize(PlanetLimitHandler planetLimitHandler, LevelGoalHandler levelGoalHandler)
        {
            _planetLimitHandler = planetLimitHandler;
            _levelGoalHandler = levelGoalHandler;

            _nextLevelButton.onClick.AddListener(OnNextLevelPressed);
            _resetLevelButton.onClick.AddListener(OnResetLevelPressed);
            _rewardButton.onClick.AddListener(OnRewardPressed);

            _limitPanel.Initialize(_planetLimitHandler);
            _goalPanel.Initialize(_levelGoalHandler);
        }

        private void OnDestroy()
        {
            _nextLevelButton.onClick.RemoveListener(OnNextLevelPressed);
            _resetLevelButton.onClick.RemoveListener(OnResetLevelPressed);
            _rewardButton.onClick.RemoveListener(OnRewardPressed);
        }

        public void Prepare(IReadOnlyPlayerData playerData)
        {
            int planetGoalRank = _levelGoalHandler.PlanetGoalRank;

            _limitPanel.Prepare(playerData.PlanetRank);
            _goalPanel.Prepare(planetGoalRank);
            _levelLabel.text = $"Уровень {playerData.Level}";
        }

        private void OnRewardPressed()
        {
            RewardPressed?.Invoke();
            Hide();
        }

        private void OnResetLevelPressed()
        {
            RestartLevelPressed?.Invoke();
            Hide();
        }

        private void OnNextLevelPressed()
        {
            NextLevelPressed?.Invoke();
            Hide();
        }

        public void Hide()
        {
            _victoryWindow.gameObject.SetActive(false);
            _looseWindow.gameObject.SetActive(false);
        }

        public void ShowVictoryWindow()
        {
            _victoryWindow.gameObject.SetActive(true);
        }

        public void ShowLooseWindow()
        {
            _looseWindow.gameObject.SetActive(true);
        }
    }
}