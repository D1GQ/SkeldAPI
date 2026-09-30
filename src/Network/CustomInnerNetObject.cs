using InnerNet;
using UnityEngine;

namespace SkeldApi.Network;

public abstract class CustomInnerNetObject : MonoBehaviour
{
    public InnerNetObject InnerNetObject { get; internal set; } = null!;

    internal void HostInitialize()
    {
        OnHostInitialize();
    }

    protected virtual void OnHostInitialize()
    {
    }
}
