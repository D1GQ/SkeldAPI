using SkeldApi.Commands.Arguments;
using SkeldApi.Interfaces;
using SkeldApi.Structs;

namespace SkeldApi.Commands;

public abstract class SkeldCommand : IUniquelyIdentifiable
{
    public string GUID { get; internal set; } = string.Empty;
    public Gid GID { get; internal set; }
    public SkeldPlugin.SkeldMod SkeldMod { get; internal set; } = null!;
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
