using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class AppearingEntity : MonoBehaviour
{
    private void Awake()
    {
        OnAwake();
    }

    public void ResetView()
    {
        OnResetView();
    }

    public abstract UniTask AppearAsync();

    public abstract UniTask DisappearAsync();

    protected virtual void OnAwake() { }

    protected virtual void OnResetView() { }
}
