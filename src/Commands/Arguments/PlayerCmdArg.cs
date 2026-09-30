namespace SkeldApi.Commands.Arguments;

public sealed class PlayerCmdArg : SkeldCmdArg<PlayerControl>
{
    public override void OnInitialize(ref string identifier)
    {
        identifier = "Player";
    }

    public override void GetSuggestions(List<string> suggestions)
    {
        foreach (var player in PlayerControl.AllPlayerControls)
        {
            if (player.Data == null)
                continue;

            if (!string.IsNullOrEmpty(player.Data.PlayerName))
            {
                suggestions.Add(player.Data.PlayerName);
            }

            if (!string.IsNullOrEmpty(player.Data.FriendCode))
            {
                suggestions.Add(player.Data.FriendCode);
            }

            suggestions.Add($"id:{player.PlayerId}");
        }
    }

    public override bool TryParse(out PlayerControl player)
    {
        foreach (var pc in PlayerControl.AllPlayerControls)
        {
            if (pc.Data == null)
                continue;

            if (pc.Data.PlayerName.ToLower() == ArgStr.ToLower())
            {
                player = pc;
                return true;
            }

            if (pc.Data.FriendCode.ToLower() == ArgStr.ToLower())
            {
                player = pc;
                return true;
            }

            if ($"id:{pc.PlayerId}" == ArgStr.ToLower())
            {
                player = pc;
                return true;
            }
        }

        player = null!;
        return false;
    }
}
