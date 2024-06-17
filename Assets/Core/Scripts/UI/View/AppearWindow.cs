using Cysharp.Threading.Tasks;
using PlanetMerge.Utils;
using UnityEngine;

namespace PlanetMerge.UI.View
{
    public class AppearWindow : UiWindow
    {
        [SerializeField] private AppearTween _appearTween;

        public async UniTask AppearAsync()
        {
            Show();
            await _appearTween.RunAsync(transform);
        }
    }
}