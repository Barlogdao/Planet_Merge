using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private int _gameSceneIndex;
    private IEnumerator Start()
    {
#if !UNITY_EDITOR

        yield return YandexGamesSdk.Initialize();
#endif
        yield return null;
        SceneManager.LoadScene(_gameSceneIndex);
    }
}
