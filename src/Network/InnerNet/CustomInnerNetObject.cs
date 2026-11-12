using InnerNet;
using SkeldApi.Interfaces;
using SkeldApi.Structs;
using UnityEngine;

namespace SkeldApi.Network.InnerNet;

public abstract class CustomInnerNetObject : MonoBehaviour, IUniquelyIdentifiable
{
    public string GUID { get; internal set; } = string.Empty;
    public Gid GID { get; internal set; }
    public InnerNetObject InnerNetObject { get; private set; } = null!;

    internal void Initialize(InnerNetObject innerNetObject)
    {
        InnerNetObject = innerNetObject;
        OnInitialize();
    }

    protected virtual void OnInitialize()
    {
    }
}
