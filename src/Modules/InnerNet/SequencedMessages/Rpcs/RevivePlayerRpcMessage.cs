using InnerNet;

namespace SkeldApi.Modules.InnerNet.SequencedMessages.Rpcs;

public sealed class RevivePlayerRpcMessage : SequencedRpcMessage
{
    public RevivePlayerRpcMessage(PlayerControl player, params ClientId[] targetClientIds)
    {
        InitializeRpcMessage(player.NetId, RpcCalls.SetRole, messageWriter =>
        {
            messageWriter.Write((ushort)player.Data.RoleType);
            messageWriter.Write(true);
        }, () =>
        {
            player.Revive();
        }, targetClientIds);
    }
}