#pragma warning disable CS0162

using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace SkeldApi;

[BepInPlugin(ModInfo.PLUGIN_GUID, ModInfo.PLUGIN_NAME, ModInfo.VERSION)]
[BepInProcess(ModInfo.AmongUs.PROCESS_NAME)]
internal partial class SkeldPlugin : BasePlugin
{
    internal static SkeldPlugin Instance { get; private set; } = null!;

    internal static Harmony Harmony { get; } = new Harmony(ModInfo.PLUGIN_GUID);

    internal static ManualLogSource Logger { get; } = BepInEx.Logging.Logger.CreateLogSource("SkeldApi");

    public override void Load()
    {
        Instance = this;
        Harmony.PatchAll();
    }
}