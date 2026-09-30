using SkeldApi.Commands.Arguments;

namespace SkeldApi.Commands;

public abstract class SkeldCommand
{
    internal string[] CommandNames = [];
    internal SkeldCmdArg[] CommandArguments = [];
    internal void Initialize()
    {
        List<SkeldCmdArg> commandArguments = [];
        OnInitialize(CommandNames, commandArguments);
        CommandArguments = [.. commandArguments.OrderBy(ca => ca.Required)];
        foreach (var argument in CommandArguments)
        {
            argument.Initialize();
        }
    }

    public abstract void OnInitialize(string[] commandNames, List<SkeldCmdArg> commandArguments);

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
