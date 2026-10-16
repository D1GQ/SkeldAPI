using SkeldApi.Attributes.InnerNet;
using SkeldApi.Network.InnerNet;

namespace SkeldApi.Managers;

public static class InnerNetManager
{
    private sealed record MethodRpcInfo(uint Id, bool IsCmd);

    internal static void RegisterInnerNetRpc(SkeldPlugin.SkeldMod skeldMod, InnerNetRpc.HookEntry hookEntry)
    {
        string methodGuid = skeldMod.GUID + ".object.rpc." + hookEntry.MethodInfo.DeclaringType.Name.ToLower() + "." + hookEntry.MethodInfo.Name.ToLower();
        hookEntry.Attribute.GUID = methodGuid;
        ChainloaderSequencer.SequenceAction(methodGuid, gid =>
        {
            hookEntry.Attribute.GID = gid;
            SkeldPlugin.Logger.LogInfo($"Successfully registered {hookEntry.MethodInfo.Name} from {skeldMod.PluginInfo.Metadata.Name}");
        }, SkeldPlugin.Constants.Chainloader.CHAINLOADER_HOOK_PRIORITY);
    }

    internal static void RegisterInnerNetCmd(SkeldPlugin.SkeldMod skeldMod, InnerNetCmd.HookEntry hookEntry)
    {
        string methodGuid = skeldMod.GUID + ".object.cmd." + hookEntry.MethodInfo.DeclaringType.Name.ToLower() + "." + hookEntry.MethodInfo.Name.ToLower();
        hookEntry.Attribute.GUID = methodGuid;
        ChainloaderSequencer.SequenceAction(methodGuid, gid =>
        {
            hookEntry.Attribute.GID = gid;
            SkeldPlugin.Logger.LogInfo($"Successfully registered {hookEntry.MethodInfo.Name} from {skeldMod.PluginInfo.Metadata.Name}");
        }, SkeldPlugin.Constants.Chainloader.CHAINLOADER_HOOK_PRIORITY);
    }

    internal static void RegisterCustomInnerNetObject(SkeldPlugin.SkeldMod skeldMod, Type customInnerNetObject)
    {
        if (!typeof(CustomInnerNetObject).IsAssignableFrom(customInnerNetObject))
            throw new ArgumentException($"Type {customInnerNetObject.FullName} does not inherit from CustomInnerNetObject.");

        string customInnerNetObjectGuid = skeldMod.GUID + ".custom.innernet.object." + customInnerNetObject.Name.ToLower();
        ChainloaderSequencer.SequenceAction(customInnerNetObjectGuid, gid =>
        {
            SkeldPlugin.Logger.LogInfo($"Successfully registered {customInnerNetObject.Name} from {skeldMod.PluginInfo.Metadata.Name}");
        }, SkeldPlugin.Constants.Chainloader.CHAINLOADER_CUSTOM_INNERNET_OBJECT_PRIORITY);
    }

    internal static void OnChainloaderFinished()
    {
    }
}
