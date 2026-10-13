using SkeldApi.Commands.Arguments;
using System.Collections.ObjectModel;

namespace SkeldApi.Commands;

public abstract class SkeldCommand
{
    public static ReadOnlyCollection<SkeldCommand> AllCommands => Commands.AsReadOnly();
    internal static readonly List<SkeldCommand> Commands = [];

    internal string[] CommandNames = [];
    internal SkeldCmdArg[] CommandArguments = [];
    internal void Initialize()
    {
        List<SkeldCmdArg> commandArguments = [];
        List<string> commandNamesList = [];
        OnInitialize(commandNamesList, commandArguments);
        CommandNames = [.. commandNamesList];
        CommandArguments = [.. commandArguments.OrderBy(ca => ca.Required)];
        foreach (var argument in CommandArguments)
        {
            argument.Initialize();
        }
    }

    public abstract void OnInitialize(List<string> commandNames, List<SkeldCmdArg> commandArguments);

    internal void RunCommand(PlayerControl sender, string[] args)
    {
        for (int i = 0; i < CommandArguments.Length; i++)
        {
            if (i > args.Length)
                break;

            var commandArgument = CommandArguments[i];
            commandArgument.ArgStr = args[i];
        }

        OnExecute(sender);

        foreach (var commandArgument in CommandArguments)
        {
            commandArgument.ArgStr = string.Empty;
        }
    }

    public abstract void OnExecute(PlayerControl sender);
}
