using Hazel;
using InnerNet;

namespace SkeldApi.Modules.InnerNet.SequencedMessages;

public class SequencedMessage
{
    protected void InitializeMessage(MessageWriter messageWriter, Action runLocal, params ClientId[] targetClientIds)
    {
        GameDataTag = targetClientIds.Length > 0 ? Tags.GameDataTo : Tags.GameData;
        Message = messageWriter.ToByteArray(true);
        RunLocal = runLocal;
        TargetClientIds = targetClientIds;
    }

    public byte GameDataTag { get; private set; }
    public byte[] Message { get; private set; } = null!;
    public Action RunLocal { get; private set; } = null!;
    public ClientId[] TargetClientIds { get; private set; } = null!;
}
