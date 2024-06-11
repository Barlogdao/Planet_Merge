using UnityEngine;
using UnityEngine.UI;

namespace PlanetMerge.UI.View
{
    public class LeaderboardWindow : UiWindow
    {
        [SerializeField] private Button _backButton;

        private void Awake()
        {
            _backButton.onClick.AddListener(Close);
            Hide();
        }

        private void Close()
        {
            Hide();
        }
    }
}