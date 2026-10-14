#pragma warning disable CS0162

using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using SkeldApi.Attributes.Register;
using SkeldApi.Interfaces;
using SkeldApi.Managers;
using SkeldApi.Structs;
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
            AddSkeldMod(pluginInfo, this);
        }

        IL2CPPChainloader.Instance.PluginLoad += ChainloaderPluginLoad;
        IL2CPPChainloader.Instance.Finished += ChainloaderLoad;
    }

    private void ChainloaderPluginLoad(PluginInfo pluginInfo, Assembly assembly, BasePlugin basePlugin)
    {
        if (pluginInfo.Dependencies.Any(dep => dep.DependencyGUID == ModInfo.PLUGIN_GUID))
        {
            AddSkeldMod(pluginInfo, basePlugin);
        }
    }

    internal void AddSkeldMod(PluginInfo pluginInfo, BasePlugin basePlugin)
    {
        foreach (var mod in SkeldMods)
        {
            if (mod.GUID == pluginInfo.Metadata.GUID)
                return;
        }

        var skeldMod = new SkeldMod(pluginInfo, basePlugin);
        SkeldMods.Add(skeldMod);

        ChainloaderSequencer.SequenceAction(skeldMod.GUID + ".mod", gid =>
        {
            skeldMod.GID = gid;
            Logger.LogInfo($"Successfully registered {basePlugin.GetType().Name} SkeldMod from {pluginInfo.Metadata.Name}");
        }, basePlugin == this ? Constants.Chainloader.CHAINLOADER_SKELDMOD_SELF_PRIORITY : Constants.Chainloader.CHAINLOADER_SKELDMOD_PRIORITY);

        RegisterCommandAttribute.RegisterFrom(skeldMod);
        RegisterGameModeAttribute.RegisterFrom(skeldMod);
        RegisterCustomRoleAttribute.RegisterFrom(skeldMod);
    }

    private void ChainloaderLoad()
    {
        ChainloaderSequencer.RunSequenced();
    }

    public sealed class SkeldMod(PluginInfo pluginInfo, BasePlugin basePlugin) : IUniquelyIdentifiable
    {
        public string GUID { get; } = pluginInfo.Metadata.GUID;
        public Gid GID { get; internal set; }
        public readonly PluginInfo PluginInfo = pluginInfo;
        public readonly BasePlugin Plugin = basePlugin;
        public readonly Assembly Assembly = basePlugin.GetType().Assembly;
    }
}