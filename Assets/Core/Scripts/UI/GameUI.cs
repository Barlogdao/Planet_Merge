using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlanetMerge.Systems;
using System;
using UnityEngine.UI;

namespace PlanetMerge.UI
{
    public class GameUI : MonoBehaviour
    {

        [SerializeField] private TMP_Text _goalLabel;
        [SerializeField] private RectTransform _levelFinishedWindow;
        [SerializeField] private RectTransform _levelLoosedWindow;

        [SerializeField] private LimitBar _limitBar;
        [SerializeField] private GoalBar _goalBar;

        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _resetLevelButton;
        [SerializeField] private Button _rewardButton;

        public event Action NextLevelPressed;
        public event Action ResetLevelPressed;
        public event Action RewardPressed;

        private PlanetLimitHandler _planetLimitHandler;
        private LevelGoalHandler _levelGoalHandler;

        public void Initialize(PlanetLimitHandler planetLimitHandler, LevelGoalHandler levelGoalHandler)
        {
            _planetLimitHandler = planetLimitHandler;
            _levelGoalHandler = levelGoalHandler;

            _planetLimitHandler.LimitChanged += OnLimitChanged;
            _levelGoalHandler.GoalChanged += OnGoalChanged;

            _nextLevelButton.onClick.AddListener(OnNextLevelPressed);
            _resetLevelButton.onClick.AddListener(OnResetLevelPressed);
            _rewardButton.onClick.AddListener(OnRewardPressed);
            _limitBar.Initialize(_planetLimitHandler);
            _goalBar.Initialize(_levelGoalHandler);
        }

        private void OnDestroy()
        {
            _planetLimitHandler.LimitChanged -= OnLimitChanged;
            _levelGoalHandler.GoalChanged -= OnGoalChanged;

            _nextLevelButton.onClick.RemoveListener(OnNextLevelPressed);
            _resetLevelButton.onClick.RemoveListener(OnResetLevelPressed);
            _rewardButton.onClick.RemoveListener(OnRewardPressed);
        }

        public void Prepare(IReadOnlyPlayerData playrData)
        {
            int planetGoalRank = _levelGoalHandler.PlanetGoalRank;

            _limitBar.Prepare(playrData.PlanetRank);
            _goalBar.Prepare(planetGoalRank);
        }

        private void OnRewardPressed()
        {
            RewardPressed?.Invoke();
        }

        private void OnResetLevelPressed()
        {
            ResetLevelPressed?.Invoke();
        }

        private void OnNextLevelPressed()
        {
            NextLevelPressed?.Invoke();
        }



        private void OnLimitChanged(int amount)
        {
            
        }

        private void OnGoalChanged(int mergeLeft)
        {
            _goalLabel.text = mergeLeft.ToString();
        }

        public void Hide()
        {
            _levelFinishedWindow.gameObject.SetActive(false);
            _levelLoosedWindow.gameObject.SetActive(false);
        }

        public void ShowFinishWindow()
        {
            _levelFinishedWindow.gameObject.SetActive(true);
        }

        public void ShowLooseWindow()
        {
            _levelLoosedWindow.gameObject.SetActive(true);
        }
    }
}