using SkeldApi.Commands;
using System.Collections.ObjectModel;

namespace SkeldApi.Managers;

public static class CommandManager
{
    public static ReadOnlyCollection<SkeldCommand> AllCommands => Commands.AsReadOnly();
    internal static readonly List<SkeldCommand> Commands = [];
    internal static void RegisterCommand(SkeldPlugin.SkeldMod skeldMod, Type command)
    {
        if (!typeof(SkeldCommand).IsAssignableFrom(command))
            throw new ArgumentException($"Type {command.FullName} does not inherit from SkeldCommand.");

        var cmd = Activator.CreateInstance(command, skeldMod) as SkeldCommand;
        Commands.Add(cmd);
        cmd.Initialize();
    }
}
