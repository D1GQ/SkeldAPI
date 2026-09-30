#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member

namespace SkeldApi.Commands.Arguments;

public sealed class StringCmdArg(string identifier, Action<List<string>>? getSuggestions = null) : SkeldCmdArg<string>
{
    private readonly string _identifier = identifier;
    private readonly Action<List<string>>? _getSuggestions = getSuggestions;

    public override void GetSuggestions(List<string> suggestions)
    {
        _getSuggestions?.Invoke(suggestions);
    }

    public override void OnInitialize(ref string identifier)
    {
        identifier = _identifier;
    }

    [Obsolete("Get string Argument from ArgStr property!")]
    public override bool TryParse(out string value)
    {
        throw new NotImplementedException();
    }
}
