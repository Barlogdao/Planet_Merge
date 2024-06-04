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
    }
}