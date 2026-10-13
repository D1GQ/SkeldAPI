using SkeldApi.Interfaces;

namespace SkeldApi.Managers;

public static class GameModeManager
{
    internal static void RegisterGameMode(SkeldPlugin.SkeldMod skeldMod, Type gameMode, Enum gamemodeEnum)
    {
        if (!typeof(IGameMode).IsAssignableFrom(gameMode))
            throw new ArgumentException("Type must implement IGameMode", nameof(gameMode));
    }
}
