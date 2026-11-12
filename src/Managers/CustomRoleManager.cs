using SkeldApi.Attributes.InnerNet;
using SkeldApi.Roles;
using SkeldApi.Structs;
using System.Collections.ObjectModel;

namespace SkeldApi.Managers;

public static class CustomRoleManager
{
    private static readonly List<CustomRole> customRoles = [];
    public static ReadOnlyCollection<CustomRole> AllCustomRoles { get; private set; } = null!;
    private static readonly List<RoleModifier> roleModifiers = [];
    public static ReadOnlyCollection<RoleModifier> AllRoleModifiers { get; private set; } = null!;

    internal static void RegisterCustomRole(SkeldPlugin.SkeldMod skeldMod, Type customRole, Enum customRoleEnum)
    {
        if (!typeof(CustomRole).IsAssignableFrom(customRole))
            throw new ArgumentException($"Type {customRole.FullName} does not inherit from CustomRole.");

        string customRoleGuid = skeldMod.GUID + ".role." + customRole.Name.ToLower();
        ChainloaderSequencer.SequenceAction(customRoleGuid, gid =>
        {
            SkeldPlugin.Logger.LogInfo($"Successfully registered {customRole.Name} from {skeldMod.PluginInfo.Metadata.Name}");
        }, SkeldPlugin.Constants.Chainloader.CHAINLOADER_CUSTOM_ROLE_PRIORITY);
    }

    internal static void RegisterRoleModifier(SkeldPlugin.SkeldMod skeldMod, Type roleModifier, Enum roleModifierEnum)
    {
        if (!typeof(RoleModifier).IsAssignableFrom(roleModifier))
            throw new ArgumentException($"Type {roleModifier.FullName} does not inherit from RoleModifier.");

        string roleModifierGuid = skeldMod.GUID + ".role." + roleModifier.Name.ToLower();
        ChainloaderSequencer.SequenceAction(roleModifierGuid, gid =>
        {
            SkeldPlugin.Logger.LogInfo($"Successfully registered {roleModifier.Name} from {skeldMod.PluginInfo.Metadata.Name}");
        }, SkeldPlugin.Constants.Chainloader.CHAINLOADER_ROLE_MODIFIER_PRIORITY);
    }

    [InnerNetRpc]
    public static void RpcSetCustomRole(PlayerControl player, Gid roleGid)
    {

    }

    [InnerNetRpc]
    public static void RpcAddModifier(PlayerControl player, Gid roleModifierGid)
    {

    }

    [InnerNetRpc]
    public static void RpcRemoveModifier(PlayerControl player, Gid roleModifierGid)
    {

    }
}
