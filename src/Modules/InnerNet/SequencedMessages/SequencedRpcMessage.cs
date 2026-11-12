using AmongUs.InnerNet.GameDataMessages;
using Hazel;
using InnerNet;

namespace SkeldApi.Modules.InnerNet.SequencedMessages;

public class SequencedRpcMessage : SequencedMessage
{
    public SequencedRpcMessage() { }

    public SequencedRpcMessage(NetId netId, RpcCalls rpcCall, Action<MessageWriter> writeTo, Action runLocal, params ClientId[] targetClientIds)
    {
        MessageWriter messageWriter = MessageWriter.Get();
        messageWriter.StartMessage((byte)GameDataTypes.RpcFlag);
        messageWriter.WritePacked(netId);
        messageWriter.Write((byte)rpcCall);
        writeTo.Invoke(messageWriter);
        messageWriter.EndMessage();
        InitializeMessage(messageWriter, runLocal, targetClientIds);
        messageWriter.Recycle();
    }

    protected void InitializeRpcMessage(NetId netId, RpcCalls rpcCall, Action<MessageWriter>? writeTo, Action runLocal, params ClientId[] targetClientIds)
    {
        MessageWriter messageWriter = MessageWriter.Get();
        messageWriter.StartMessage((byte)GameDataTypes.RpcFlag);
        messageWriter.WritePacked(netId);
        messageWriter.Write((byte)rpcCall);
        writeTo?.Invoke(messageWriter);
        messageWriter.EndMessage();
        InitializeMessage(messageWriter, runLocal, targetClientIds);
        messageWriter.Recycle();
    }
}
