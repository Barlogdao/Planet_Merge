using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetMerge.Systems.Tutorial
{
    [System.Serializable]
    public class ButtonTutorialTip : TutorialTip
    {
        [SerializeField] Button _button;
        protected override async UniTask OnRun()
        {
            await _button.OnClickAsync();
        }

        protected override void Activate()
        {
            base.Activate();

            _button.gameObject.SetActive(true);
        }

        protected override void Deactivate()
        {
            base.Deactivate();

            _button.gameObject.SetActive(false);
        }
    }
}