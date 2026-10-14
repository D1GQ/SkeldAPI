using SkeldApi.Managers;
using SkeldApi.Roles;

namespace SkeldApi.Attributes.Register;

[AttributeUsage(AttributeTargets.Class)]
public sealed class RegisterCustomRoleAttribute(object roleTypeEnum) : RegisterAttribute<CustomRole, RegisterCustomRoleAttribute>
{
    internal readonly Enum RoleType = (Enum)roleTypeEnum;

    internal static void RegisterFrom(SkeldPlugin.SkeldMod skeldMod)
    {
        var entries = GetRegisterTypesFrom(skeldMod.Assembly);
        foreach (var entry in entries)
        {
            CustomRoleManager.RegisterCustomRole(skeldMod, entry.Type, entry.Attribute.RoleType);
        }
    }
}
