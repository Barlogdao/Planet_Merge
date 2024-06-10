using Cysharp.Threading.Tasks;

namespace PlanetMerge.Systems.Tutorial
{
    [System.Serializable]
    public class FirstTip : LaunchTutorialTip
    {
        protected override async UniTask OnRun()
        {
            Pointer.SetScaling();
            await UniTask.WaitUntilValueChanged(this, (tip) => tip.IsPlanetLauched);
        }
    }
}