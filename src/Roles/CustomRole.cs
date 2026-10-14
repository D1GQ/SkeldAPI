using AmongUs.GameOptions;
using SkeldApi.Interfaces;
using SkeldApi.Structs;
using UnityEngine;

namespace SkeldApi.Roles;

public abstract class CustomRole : MonoBehaviour, IUniquelyIdentifiable
{
    public Enum RoleType { get; internal set; } = default!;
    public string GUID { get; internal set; } = string.Empty;
    public Gid GID { get; internal set; }
    public PlayerControl Player { get; internal set; } = null!;
    public abstract RoleTypes InitialVanillaRoleType { get; }

    internal void Initialize(PlayerControl player)
    {
        Player = player;
        RpcSetVanillaRole(InitialVanillaRoleType);
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

    public void RpcSetVanillaRole(RoleTypes roleType)
    {
        Player.RpcSetRole(roleType, true);
    }
}
