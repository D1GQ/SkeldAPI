using SkeldApi.Commands;
using SkeldApi.Managers;

namespace SkeldApi.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class RegisterCommandAttribute : RegisterAttribute<SkeldCommand, RegisterCommandAttribute>
{
    internal static void RegisterFrom(SkeldPlugin.SkeldMod skeldMod)
    {
        var entries = GetRegisterTypesFrom(skeldMod.Assembly);
        foreach (var entry in entries)
        {
            CommandManager.RegisterCommand(skeldMod, entry.Type);
        }
    }
}
