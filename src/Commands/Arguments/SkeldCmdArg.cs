namespace SkeldApi.Commands.Arguments;

public abstract class SkeldCmdArg
{
    internal string Identifier = string.Empty;
    public bool Required;
    public string ArgStr { get; internal set; } = string.Empty;
    internal void Initialize()
    {
        OnInitialize(ref Identifier);
        if (Required)
        {
            Identifier = '{' + Identifier + '}';
        }
        else
        {
            Identifier = '[' + Identifier + ']';
        }
    }
    public abstract void OnInitialize(ref string identifier);
    public abstract void GetSuggestions(List<string> suggestions);
}

public abstract class SkeldCmdArg<T> : SkeldCmdArg where T : class
{
    public abstract bool TryParse(out T value);
}
