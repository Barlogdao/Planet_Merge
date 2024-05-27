using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace PlanetMerge.Systems.Tutorial
{
    [System.Serializable]
    public class SecondTip : ClickTutorialTip
    {
        [SerializeField] private Vector2 _pointerEndPosition;
        [SerializeField] private float _tweenDuration = 1f;

        protected override async UniTask OnRun()
        {
            Pointer.SetIdle();
            var tween = Pointer.transform.DOMove(_pointerEndPosition, _tweenDuration).SetLoops(-1, LoopType.Yoyo);

            await UniTask.WaitUntilValueChanged(this, (tip) => tip.IsClicked);

            tween.Kill();
        }
    }
}