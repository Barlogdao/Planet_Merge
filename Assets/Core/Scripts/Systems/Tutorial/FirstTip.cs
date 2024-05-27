using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetMerge.Systems.Tutorial
{
    [System.Serializable]
    public class FirstTip : ClickTutorialTip
    {
        protected override async UniTask OnRun()
        {
            Pointer.SetScaling();
            await UniTask.WaitUntilValueChanged(this, (tip) => tip.IsClicked);
        }
    }
}