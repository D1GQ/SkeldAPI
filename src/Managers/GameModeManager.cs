using SkeldApi.GameModes;

namespace SkeldApi.Managers;

public static class GameModeManager
{
    internal static void RegisterGameMode(SkeldPlugin.SkeldMod skeldMod, Type gamemode, Enum gamemodeEnum)
    {
        if (!typeof(CustomGameMode).IsAssignableFrom(gamemode))
            throw new ArgumentException("Type must implement IGameMode", nameof(gamemode));

        string gamemodeGuid = skeldMod.GUID + ".gamemode." + gamemode.Name.ToLower();
        ChainloaderSequencer.SequenceAction(gamemodeGuid, gid =>
        {
            SkeldPlugin.Logger.LogInfo($"Successfully registered {gamemode.Name} from {skeldMod.PluginInfo.Metadata.Name}");
        }, SkeldPlugin.Constants.Chainloader.CHAINLOADER_GAMEMODE_PRIORITY);
    }
}
