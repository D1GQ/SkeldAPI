using SkeldApi.Interfaces;
using SkeldApi.Managers;

namespace SkeldApi.Attributes.Register;

[AttributeUsage(AttributeTargets.Class)]
public sealed class RegisterGameModeAttribute(object gamemodeEnum) : RegisterAttribute<IGameMode, RegisterGameModeAttribute>
{
    internal readonly Enum GameMode = (Enum)gamemodeEnum;

    internal static void RegisterFrom(SkeldPlugin.SkeldMod skeldMod)
    {
        var entries = GetRegisterTypesFrom(skeldMod.Assembly);
        foreach (var entry in entries)
        {
            GameModeManager.RegisterGameMode(skeldMod, entry.Type, entry.Attribute.GameMode);
        }
    }
}
