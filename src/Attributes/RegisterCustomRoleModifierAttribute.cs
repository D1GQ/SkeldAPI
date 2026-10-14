using SkeldApi.Managers;
using SkeldApi.Roles;

namespace SkeldApi.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class RegisterCustomRoleModifierAttribute(object roleModifierTypeEnum) : RegisterAttribute<RoleModifier, RegisterCustomRoleModifierAttribute>
{
    internal readonly Enum RoleType = (Enum)roleModifierTypeEnum;

    internal static void RegisterFrom(SkeldPlugin.SkeldMod skeldMod)
    {
        var entries = GetRegisterTypesFrom(skeldMod.Assembly);
        foreach (var entry in entries)
        {
            CustomRoleManager.RegisterRoleModifier(skeldMod, entry.Type, entry.Attribute.RoleType);
        }
    }
}
