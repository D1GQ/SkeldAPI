using SkeldApi.Attributes;
using SkeldApi.Commands.Arguments;

namespace SkeldApi.Commands;

[RegisterCommand]
internal sealed class HelpCommand : SkeldCommand
{
    public override void OnInitialize(List<string> commandNames, List<SkeldCmdArg> commandArguments)
    {
        commandNames.Add("help");
    }

    public override void OnExecute(PlayerControl sender)
    {
    }
}
