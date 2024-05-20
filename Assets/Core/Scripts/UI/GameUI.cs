using UnityEngine;
using TMPro;
using PlanetMerge.Systems;
using System;
using UnityEngine.UI;

namespace PlanetMerge.UI
{
    public class GameUI : MonoBehaviour
    {

    
        [SerializeField] private RectTransform _levelFinishedWindow;
        [SerializeField] private RectTransform _levelLoosedWindow;

        [SerializeField] private LimitBar _limitBar;
        [SerializeField] private GoalBar _goalBar;
        [SerializeField] private TMP_Text _levelLabel;

        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _resetLevelButton;
        [SerializeField] private Button _rewardButton;


        public event Action NextLevelPressed;
        public event Action RestartLevelPressed;
        public event Action RewardPressed;

        private GameEventMediator _gameEventMediator;
        private PlanetLimitHandler _planetLimitHandler;
        private LevelGoalHandler _levelGoalHandler;

        public void Initialize(GameEventMediator gameEventMediator,PlanetLimitHandler planetLimitHandler, LevelGoalHandler levelGoalHandler)
        {
            _gameEventMediator = gameEventMediator;
            _planetLimitHandler = planetLimitHandler;
            _levelGoalHandler = levelGoalHandler;

            _gameEventMediator.LevelCreated += OnLevelCreated;
            _gameEventMediator.GameWinned += OnGameWinned;
            _gameEventMediator.GameLost += OnGameLost;

            _nextLevelButton.onClick.AddListener(OnNextLevelPressed);
            _resetLevelButton.onClick.AddListener(OnResetLevelPressed);
            _rewardButton.onClick.AddListener(OnRewardPressed);

            _limitBar.Initialize(_planetLimitHandler);
            _goalBar.Initialize(_levelGoalHandler);
        }

        private void OnDestroy()
        {
            _gameEventMediator.LevelCreated -= OnLevelCreated;
            _gameEventMediator.GameWinned -= OnGameWinned;
            _gameEventMediator.GameLost -= OnGameLost;

            _nextLevelButton.onClick.RemoveListener(OnNextLevelPressed);
            _resetLevelButton.onClick.RemoveListener(OnResetLevelPressed);
            _rewardButton.onClick.RemoveListener(OnRewardPressed);
        }

        public void Prepare(IReadOnlyPlayerData playerData)
        {
            int planetGoalRank = _levelGoalHandler.PlanetGoalRank;

            _limitBar.Prepare(playerData.PlanetRank);
            _goalBar.Prepare(planetGoalRank);
            _levelLabel.text = $"Уровень {playerData.Level}";
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

        private void OnGameLost()
        {
            ShowLooseWindow();
        }

        private void OnGameWinned()
        {
            ShowWinWindow();
        }

        private void OnLevelCreated()
        {
            Hide();
        }

        public void Hide()
        {
            _levelFinishedWindow.gameObject.SetActive(false);
            _levelLoosedWindow.gameObject.SetActive(false);
        }

        public void ShowWinWindow()
        {
            _levelFinishedWindow.gameObject.SetActive(true);
        }

        public void ShowLooseWindow()
        {
            _levelLoosedWindow.gameObject.SetActive(true);
        }
    }
}