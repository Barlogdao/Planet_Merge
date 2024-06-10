using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetMerge.Systems.Tutorial
{
    [System.Serializable]
    public class SecondTip : LaunchTutorialTip
    {
        [SerializeField] private Vector2 _pointerEndPosition;
        [SerializeField] private float _tweenDuration = 1f;
        [SerializeField] private Ease _ease;
        [SerializeField] private Image _arrows;

        protected override async UniTask OnRun()
        {
            _arrows.enabled = true;

            Pointer.SetIdle();
            var tween = Pointer.transform.DOMove(_pointerEndPosition, _tweenDuration).SetLoops(-1, LoopType.Yoyo).SetEase(_ease) ;

            await UniTask.WaitUntilValueChanged(this, (tip) => tip.IsPlanetLauched);

            tween.Kill();

            _arrows.enabled = false;
        }
    }
}