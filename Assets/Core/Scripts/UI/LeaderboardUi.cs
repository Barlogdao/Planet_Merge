using Agava.YandexGames;
using PlanetMerge.SDK.Yandex.Leaderboard;
using PlanetMerge.UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetMerge.UI
{
    public class LeaderboardUi : MonoBehaviour
    {
        [SerializeField] private LeaderboardWindow _leaderBoardWindow;
        [SerializeField] private AuthorizeWindow _authorizeWindow;
        [SerializeField] private YandexLeaderboard _leaderBoard;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private Button _authorizeButton;

        private void Awake()
        {
            _leaderboardButton.onClick.AddListener(OpenLeaderboard);
            _authorizeButton.onClick.AddListener(Authorize);
        }

        private void Authorize()
        {
            PlayerAccount.Authorize();
            _authorizeWindow.Hide();
        }

        private void OpenLeaderboard()
        {
            if (PlayerAccount.IsAuthorized)
            {
                PlayerAccount.RequestPersonalProfileDataPermission();
                _leaderBoard.Fill();
                _leaderBoardWindow.Show();
            }
            else
            {
                _authorizeWindow.Show();
            }
        }
    }
}