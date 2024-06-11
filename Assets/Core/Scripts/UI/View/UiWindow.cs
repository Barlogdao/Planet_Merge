using UnityEngine;

namespace PlanetMerge.UI.View
{
    public abstract class UiWindow : MonoBehaviour
    {
        [SerializeField] private Canvas _windowCanvas;

        public void Show()
        {
            _windowCanvas.enabled = true;
        }

        public void Hide()
        {
            _windowCanvas.enabled = false;
        }
    }
}