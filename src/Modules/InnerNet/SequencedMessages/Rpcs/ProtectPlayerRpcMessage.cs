using InnerNet;

namespace SkeldApi.Modules.InnerNet.SequencedMessages.Rpcs;

public sealed class ProtectPlayerRpcMessage : SequencedRpcMessage
{
    public ProtectPlayerRpcMessage(PlayerControl player, PlayerControl target, params ClientId[] targetClientIds)
    {
        InitializeRpcMessage(player.NetId, RpcCalls.ProtectPlayer, messageWriter =>
        {
            messageWriter.WriteNetObject(target);
            messageWriter.Write(target.CurrentOutfit.ColorId);
        }, () =>
        {
            player.ProtectPlayer(target, target.CurrentOutfit.ColorId);
        }, targetClientIds);
    }
}