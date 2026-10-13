#pragma warning disable CS0162

using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using SkeldApi.Attributes;
using System.Reflection;

namespace SkeldApi;

[BepInPlugin(ModInfo.PLUGIN_GUID, ModInfo.PLUGIN_NAME, ModInfo.VERSION)]
[BepInProcess(ModInfo.AmongUs.PROCESS_NAME)]
public partial class SkeldPlugin : BasePlugin
{
    internal static SkeldPlugin Instance { get; private set; } = null!;

    internal static Harmony Harmony { get; } = new Harmony(ModInfo.PLUGIN_GUID);

    internal static ManualLogSource Logger { get; } = BepInEx.Logging.Logger.CreateLogSource("SkeldApi");
    internal static readonly List<SkeldMod> SkeldMods = [];

    public override void Load()
    {
        Instance = this;
        Harmony.PatchAll();

        if (IL2CPPChainloader.Instance.Plugins.TryGetValue(ModInfo.PLUGIN_GUID, out var pluginInfo))
        {
            AddSkeldMod(pluginInfo);
        }

        IL2CPPChainloader.Instance.PluginLoad += ChainloaderPluginLoad;
        IL2CPPChainloader.Instance.Finished += ChainloaderLoad;
    }

    private void ChainloaderPluginLoad(PluginInfo pluginInfo, Assembly assembly, BasePlugin basePlugin)
    {
        if (pluginInfo.Dependencies.Any(dep => dep.DependencyGUID == ModInfo.PLUGIN_GUID))
        {
            AddSkeldMod(pluginInfo);
        }
    }

    private void ChainloaderLoad()
    {
    }

    internal static void AddSkeldMod(PluginInfo pluginInfo)
    {
        foreach (var mod in SkeldMods)
        {
            if (mod.GetGuid() == pluginInfo.Metadata.GUID)
                return;
        }

        var skeldMod = new SkeldMod(pluginInfo);
        SkeldMods.Add(skeldMod);

        RegisterCommandAttribute.RegisterFrom(skeldMod.Assembly);
        RegisterGameModeAttribute.RegisterFrom(skeldMod.Assembly);
        RegisterCustomRoleAttribute.RegisterFrom(skeldMod.Assembly);
    }

    internal sealed class SkeldMod(PluginInfo pluginInfo)
    {
        internal readonly PluginInfo PluginInfo = pluginInfo;
        internal readonly BasePlugin Plugin = (BasePlugin)pluginInfo.Instance;
        internal readonly Assembly Assembly = pluginInfo.Instance.GetType().Assembly;
        internal string GetGuid()
        {
            return PluginInfo.Metadata.GUID;
        }
    }
}