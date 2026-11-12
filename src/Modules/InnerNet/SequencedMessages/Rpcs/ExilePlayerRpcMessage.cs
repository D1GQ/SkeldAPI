using InnerNet;

namespace SkeldApi.Modules.InnerNet.SequencedMessages.Rpcs;

public sealed class ExilePlayerRpcMessage : SequencedRpcMessage
{
    public ExilePlayerRpcMessage(PlayerControl player, params ClientId[] targetClientIds)
    {
        InitializeRpcMessage(player.NetId, RpcCalls.Exiled, null, player.Exiled, targetClientIds);
    }
}