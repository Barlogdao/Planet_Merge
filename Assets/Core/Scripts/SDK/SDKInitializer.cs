using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlanetMerge.SDK.Yandex
{
    public sealed class SDKInitializer : MonoBehaviour
    {
        [SerializeField] private int _gameSceneIndex = 1;
        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            OnInitialize();
            yield break;
#endif
            yield return YandexGamesSdk.Initialize(OnInitialize);
        }

        private void OnInitialize()
        {
            SceneManager.LoadScene(_gameSceneIndex);
        }
    }
}