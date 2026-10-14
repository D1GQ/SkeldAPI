using SkeldApi.Attributes.Injection;

namespace SkeldApi.Managers;

public static class InnerNetManager
{
    internal static void RegisterInnerNetRpc(SkeldPlugin.SkeldMod skeldMod, InnerNetRpc.HookEntry hookEntry)
    {
        string methodGuid = skeldMod.GUID + ".object.rpc." + hookEntry.MethodInfo.DeclaringType.Name.ToLower() + "." + hookEntry.MethodInfo.Name.ToLower();
        hookEntry.Attribute.GUID = methodGuid;
        ChainloaderSequencer.SequenceAction(methodGuid, gid =>
        {
            hookEntry.Attribute.GID = gid;
        }, SkeldPlugin.Constants.Chainloader.CHAINLOADER_HOOK_PRIORITY);
    }

    internal static void RegisterInnerNetCmd(SkeldPlugin.SkeldMod skeldMod, InnerNetCmd.HookEntry hookEntry)
    {
        string methodGuid = skeldMod.GUID + ".object.cmd." + hookEntry.MethodInfo.DeclaringType.Name.ToLower() + "." + hookEntry.MethodInfo.Name.ToLower();
        hookEntry.Attribute.GUID = methodGuid;
        ChainloaderSequencer.SequenceAction(methodGuid, gid =>
        {
            hookEntry.Attribute.GID = gid;
        }, SkeldPlugin.Constants.Chainloader.CHAINLOADER_HOOK_PRIORITY);
    }
}
