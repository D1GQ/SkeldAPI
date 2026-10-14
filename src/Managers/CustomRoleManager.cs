using SkeldApi.Roles;

namespace SkeldApi.Managers;

public static class CustomRoleManager
{
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
}
