using InnerNet;

namespace SkeldApi.Modules.InnerNet.SequencedMessages.Rpcs;

public sealed class MurderPlayerRpcMessage : SequencedRpcMessage
{
    public MurderPlayerRpcMessage(PlayerControl killer, PlayerControl target, params ClientId[] targetClientIds)
    {
        InitializeRpcMessage(killer.NetId, RpcCalls.MurderPlayer, messageWriter =>
        {
            messageWriter.WriteNetObject(target);
            messageWriter.Write((int)MurderResultFlags.DecisionByHost);
        }, () =>
        {
            killer.MurderPlayer(target, MurderResultFlags.DecisionByHost);
        }, targetClientIds);
    }
}
