using System.Collections.ObjectModel;

namespace SkeldApi.Structs;

public readonly struct PlayerControllers(IEnumerable<PlayerControl> players)
{
    public readonly ReadOnlyCollection<PlayerControl> Players = new([.. players]);

    public static implicit operator PlayerControllers(PlayerControl player)
    {
        return new PlayerControllers([player]);
    }

    public static implicit operator PlayerControllers(PlayerControl[] players)
    {
        return new PlayerControllers(players);
    }

    public static implicit operator PlayerControllers(List<PlayerControl> players)
    {
        return new PlayerControllers(players);
    }
}