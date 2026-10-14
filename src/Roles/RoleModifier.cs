using SkeldApi.Interfaces;
using SkeldApi.Structs;
using UnityEngine;

namespace SkeldApi.Roles;

public abstract class RoleModifier : MonoBehaviour, IUniquelyIdentifiable
{
    public Enum RoleModifierType { get; internal set; } = default!;
    public string GUID { get; internal set; } = string.Empty;
    public Gid GID { get; internal set; }
    public PlayerControl Player { get; internal set; } = null!;

    internal void Initialize(PlayerControl player)
    {
        Player = player;
        OnInitialize(player);
    }

    protected virtual void OnInitialize(PlayerControl player)
    {
    }

    internal void Deinitialize()
    {
        OnDeinitialize();
    }

    protected virtual void OnDeinitialize()
    {
    }


}
