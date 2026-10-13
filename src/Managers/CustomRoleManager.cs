using SkeldApi.Roles;

namespace SkeldApi.Managers;

public static class CustomRoleManager
{
    internal static void RegisterCustomRole(SkeldPlugin.SkeldMod skeldMod, Type customRole, Enum customRoleEnum)
    {
        if (!typeof(CustomRole).IsAssignableFrom(customRole))
            throw new ArgumentException($"Type {customRole.FullName} does not inherit from CustomRole.");
    }
}
