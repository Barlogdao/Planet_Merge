using PlanetMerge.Systems;
using UnityEngine;

namespace PlanetMerge.SDK.Yandex
{
    public class RewardHandler : MonoBehaviour
    {
        [SerializeField] private int _bonusPlanetAmount = 5;

        private PlanetLimitHandler _planetLimitHandler;

        public void Initialize(PlanetLimitHandler planetLimitHandler)
        {
            _planetLimitHandler = planetLimitHandler;
        }

        public void AddReward()
        {
            ShowAd();
        }

        private void ShowAd()
        {

#if UNITY_WEBGL && !UNITY_EDITOR
            Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback);
#endif
        }

        private void OnOpenCallback()
        {
            Time.timeScale = 0f;
            AudioListener.volume = 0f;
        }

        private void OnRewardedCallback()
        {
            _planetLimitHandler.SetLimit(_bonusPlanetAmount);
        }

        private void OnCloseCallback()
        {
            Time.timeScale = 1f;
            AudioListener.volume = 1f;
        }
    }
}