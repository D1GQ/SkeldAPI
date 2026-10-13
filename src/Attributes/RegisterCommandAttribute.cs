using SkeldApi.Commands;
using System.Reflection;

namespace SkeldApi.Attributes;

[AttributeUsage(AttributeTargets.Class)]
internal sealed class RegisterCommandAttribute : RegisterAttribute<SkeldCommand, RegisterCommandAttribute>
{
    internal static void Initialize()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            RegisterFrom(assembly);
        }
    }

    private static void RegisterFrom(Assembly assembly)
    {
        var entries = GetRegisterTypesFrom(assembly);
        foreach (var entry in entries)
        {
            var command = Activator.CreateInstance(entry.Type) as SkeldCommand;
            SkeldCommand.Commands.Add(command);
            command.Initialize();
        }
    }
}
