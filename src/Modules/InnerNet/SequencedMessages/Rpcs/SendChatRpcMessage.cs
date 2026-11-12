using InnerNet;

namespace SkeldApi.Modules.InnerNet.SequencedMessages.Rpcs;

public sealed class SendChatRpcMessage : SequencedRpcMessage
{
    public SendChatRpcMessage(PlayerControl player, string msg, params ClientId[] targetClientIds)
    {
        InitializeRpcMessage(player.NetId, RpcCalls.SendChat, messageWriter =>
        {
            messageWriter.Write(msg);
        }, () =>
        {
            HudManager.Instance.Chat.AddChat(player, msg);
        }, targetClientIds);
    }
}