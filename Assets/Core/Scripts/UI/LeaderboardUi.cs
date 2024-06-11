using Agava.YandexGames;
using PlanetMerge.SDK.Yandex.Leaderboard;
using PlanetMerge.UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetMerge.UI
{
    public class LeaderboardUi : MonoBehaviour
    {
        [SerializeField] LeaderboardWindow _leaderBoardWindow;
        [SerializeField] AuthorizeWindow _authorizeWindow;
        [SerializeField] YandexLeaderboard _leaderBoard;
        [SerializeField] Button _leaderboardButton;
        [SerializeField] Button _authorizeButton;


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
                if (PlayerAccount.HasPersonalProfileDataPermission)
                {
                    _leaderBoard.Fill();
                    _leaderBoardWindow.Show();
                }
                else
                {
                    PlayerAccount.RequestPersonalProfileDataPermission();
                }

            }
            else
            {
                _authorizeWindow.Show();
            }

        }
    }
}