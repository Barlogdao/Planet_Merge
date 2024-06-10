using Agava.YandexGames;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetMerge.SDK.Yandex
{
    public class YandexLeaderboard : MonoBehaviour
    {
        private const string LeaderboardName = "Leaderboard";
        private const string AnonymousName = "Anonymous";

        private readonly List<LeaderboardPlayer> _leaderboardPlayers = new();

        [SerializeField] private LeaderboardView _leaderboardView;

        public void SetPlayerScore(int score)
        {
            if (PlayerAccount.IsAuthorized == false)
                return;

            Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
            {
                if (result == null || result.score < score)
                    Leaderboard.SetScore(LeaderboardName, score);
            });
        }

        public void Fill()
        {
            if (PlayerAccount.IsAuthorized == false)
                return;

            _leaderboardPlayers.Clear();

            Leaderboard.GetEntries(LeaderboardName, (result) =>
            {
                foreach (var entry in result.entries)
                {
                    int rank = entry.rank;
                    int score = entry.score;
                    string name = entry.player.publicName;

                    if (string.IsNullOrEmpty(name))
                        name = Lean.Localization.LeanLocalization.GetTranslationText(AnonymousName);

                    _leaderboardPlayers.Add(new LeaderboardPlayer(rank, name, score));
                }

                _leaderboardView.ConstructLeaderboard(_leaderboardPlayers);
            });
        }
    }
}