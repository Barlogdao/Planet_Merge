using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class AppearEntity : MonoBehaviour
{
    private void Awake()
    {
        OnAwake();
    }

    public void ResetView()
    {
        OnResetView();
    }

    protected virtual void OnAwake() { }

    public abstract UniTask AppearAsync();
    public abstract UniTask DisapearAsync();
    protected virtual void OnResetView() { }
}
