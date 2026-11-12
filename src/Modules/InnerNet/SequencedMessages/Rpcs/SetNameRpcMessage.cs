using InnerNet;

namespace SkeldApi.Modules.InnerNet.SequencedMessages.Rpcs;

public sealed class SetNameRpcMessage : SequencedRpcMessage
{
    public SetNameRpcMessage(PlayerControl player, string name, params ClientId[] targetClientIds)
    {
        InitializeRpcMessage(player.NetId, RpcCalls.SetName, messageWriter =>
        {
            messageWriter.Write(player.Data.NetId);
            messageWriter.Write(name);
        }, () =>
        {
            player.SetName(name);
        }, targetClientIds);
    }
}