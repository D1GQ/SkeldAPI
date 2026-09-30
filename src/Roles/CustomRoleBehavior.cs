using AmongUs.GameOptions;
using UnityEngine;

namespace SkeldApi.Roles;

public abstract class CustomRoleBehavior : MonoBehaviour
{
    public PlayerControl Player { get; internal set; } = null!;
    public abstract RoleTypes InitialVanillaRoleType { get; }

    internal void HostInitialize(PlayerControl player)
    {
        Player = player;
        RpcSetVanillaRole(InitialVanillaRoleType);
        OnHostInitialize(player);
    }

    protected virtual void OnHostInitialize(PlayerControl player)
    {
    }

    internal void HostDeinitialize()
    {
        OnHostDeinitialize();
    }

    protected virtual void OnHostDeinitialize()
    {
    }

    public void RpcSetVanillaRole(RoleTypes roleType)
    {
        Player.RpcSetRole(roleType, true);
    }
}
