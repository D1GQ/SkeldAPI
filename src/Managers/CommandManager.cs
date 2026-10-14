using SkeldApi.Commands;
using System.Collections.ObjectModel;

namespace SkeldApi.Managers;

public static class CommandManager
{
    public static ReadOnlyCollection<SkeldCommand> AllCommands => Commands.AsReadOnly();
    internal static readonly List<SkeldCommand> Commands = [];
    internal static void RegisterCommand(SkeldPlugin.SkeldMod skeldMod, Type command)
    {
        string commandGuid = skeldMod.GUID + ".command." + command.Name.ToLower();
        ChainloaderSequencer.SequenceAction(commandGuid, gid =>
        {
            if (!typeof(SkeldCommand).IsAssignableFrom(command))
                throw new ArgumentException($"Type {command.FullName} does not inherit from SkeldCommand.");

            var cmd = Activator.CreateInstance(command) as SkeldCommand;
            cmd.GUID = commandGuid;
            cmd.SkeldMod = skeldMod;
            cmd.GID = gid;
            Commands.Add(cmd);
            cmd.Initialize();

            SkeldPlugin.Logger.LogInfo($"Successfully registered {command.Name} from {skeldMod.PluginInfo.Metadata.Name}");
        }, SkeldPlugin.Constants.Chainloader.CHAINLOADER_COMMAND_PRIORITY);
    }
}
