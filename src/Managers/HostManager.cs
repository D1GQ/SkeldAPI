using AmongUs.InnerNet.GameDataMessages;
using Hazel;
using InnerNet;
using SkeldApi.Network;

namespace SkeldApi.Managers;

public static class HostManager
{
    public static void StartMessage(MessageWriter messageWriter, Action action, params ClientId[] targetClientIds)
    {
        if (targetClientIds.Length > 0)
        {
            if (targetClientIds.Length == 1)
            {
                messageWriter.StartMessage(Tags.GameDataTo);
                messageWriter.Write(AmongUsClient.Instance.GameId);
                messageWriter.WritePacked(targetClientIds[0]);
                action.Invoke();
                messageWriter.EndMessage();
            }
            else
            {
                messageWriter.StartMessage(Tags.PackedGameDataTo);
                messageWriter.WritePacked(AmongUsClient.Instance.GameId);
                for (int i = 0; i < targetClientIds.Length; i++)
                {
                    if (messageWriter.Length > 500 || i + 3 > AmongUsClient.Instance.GetMaxMessagePackingLimit())
                    {
                        messageWriter.EndMessage();
                        return;
                    }

                    ClientId clientId = targetClientIds[i];
                    messageWriter.StartMessage(Tags.GameDataTo);
                    messageWriter.Write(AmongUsClient.Instance.GameId);
                    messageWriter.WritePacked(clientId);
                    action.Invoke();
                    messageWriter.EndMessage();
                }
                messageWriter.EndMessage();
            }
            return;
        }

        messageWriter.StartMessage(Tags.GameData);
        messageWriter.Write(AmongUsClient.Instance.GameId);
        action.Invoke();
        messageWriter.EndMessage();
    }

    public static void StartGameDataMessage(MessageWriter messageWriter, GameDataTypes gameDataType, Action action)
    {
        messageWriter.StartMessage((byte)gameDataType);
        action.Invoke();
        messageWriter.EndMessage();
    }

    public static void StartRpcMessage(MessageWriter messageWriter, NetId netId, RpcCalls rpcCall, Action? action = null)
    {
        messageWriter.StartMessage((byte)GameDataTypes.RpcFlag);
        messageWriter.WritePacked(netId);
        messageWriter.Write((byte)rpcCall);
        action?.Invoke();
        messageWriter.EndMessage();
    }

    public static void WriteRpcMurderPlayer(MessageWriter messageWriter, PlayerControl killer, PlayerControl target, params ClientId[] targetClientIds)
    {
        StartMessage(messageWriter, () =>
        {
            StartRpcMessage(messageWriter, killer.NetId, RpcCalls.MurderPlayer, () =>
            {
                messageWriter.WriteNetObject(target);
                messageWriter.Write((int)MurderResultFlags.DecisionByHost);
            });
        }, targetClientIds);
    }

    public static void WriteRpcExilePlayer(MessageWriter messageWriter, PlayerControl player, params ClientId[] targetClientIds)
    {
        StartMessage(messageWriter, () =>
        {
            StartRpcMessage(messageWriter, player.NetId, RpcCalls.Exiled);
        }, targetClientIds);
    }

    public static void WriteRpcProtectPlayer(MessageWriter messageWriter, PlayerControl player, params ClientId[] targetClientIds)
    {
        StartMessage(messageWriter, () =>
        {
            StartRpcMessage(messageWriter, player.NetId, RpcCalls.ProtectPlayer, () =>
            {
                messageWriter.WriteNetObject(player);
                messageWriter.Write(player.CurrentOutfit.ColorId);
            });
        }, targetClientIds);
    }

    public static void WriteRpcRevivePlayer(MessageWriter messageWriter, PlayerControl player, params ClientId[] targetClientIds)
    {
        StartMessage(messageWriter, () =>
        {
            StartRpcMessage(messageWriter, player.NetId, RpcCalls.SetRole, () =>
            {
                messageWriter.Write((ushort)player.Data.RoleType);
                messageWriter.Write(true);
            });
        }, targetClientIds);
    }

    public static void WriteRpcSetName(MessageWriter messageWriter, PlayerControl player, string name, params ClientId[] targetClientIds)
    {
        StartMessage(messageWriter, () =>
        {
            StartRpcMessage(messageWriter, player.NetId, RpcCalls.SetName, () =>
            {
                messageWriter.Write(player.Data.NetId);
                messageWriter.Write(name);
            });
        }, targetClientIds);
    }

    public static void WriteRpcSendChat(MessageWriter messageWriter, PlayerControl player, string msg, params ClientId[] targetClientIds)
    {
        StartMessage(messageWriter, () =>
        {
            StartRpcMessage(messageWriter, player.NetId, RpcCalls.SendChat, () =>
            {
                messageWriter.Write(msg);
            });
        }, targetClientIds);
    }

    public static void WriteSpawnCustomInnerNetObject(MessageWriter messageWriter, CustomInnerNetObject customInnerNetObject)
    {
        customInnerNetObject.InnerNetObject.OwnerId = -2;
        customInnerNetObject.InnerNetObject.SpawnFlags = SpawnFlags.None;
        if (customInnerNetObject.InnerNetObject.NetId == 0)
        {
            uint netIdCnt = AmongUsClient.Instance.NetIdCnt;
            AmongUsClient.Instance.NetIdCnt = netIdCnt + 1U;
            customInnerNetObject.InnerNetObject.NetId = netIdCnt;
            InnerNetObjectCollection innerNetObjectCollection = AmongUsClient.Instance.allObjects;
            lock (innerNetObjectCollection)
            {
                AmongUsClient.Instance.allObjects.TryAddNetObject(customInnerNetObject.InnerNetObject);
            }
        }

        StartMessage(messageWriter, () =>
        {
            StartGameDataMessage(messageWriter, GameDataTypes.SpawnFlag, () =>
            {
                messageWriter.WritePacked(customInnerNetObject.InnerNetObject.SpawnId);
                messageWriter.WritePacked(customInnerNetObject.InnerNetObject.OwnerId);
                messageWriter.Write((byte)customInnerNetObject.InnerNetObject.SpawnFlags);
                messageWriter.WritePacked(0);
            });
        });
    }

    public static void SendNewMessage(Action<MessageWriter> action)
    {
        var messageWriter = MessageWriter.Get(SendOption.Reliable);
        action(messageWriter);
        SendAndRecycle(messageWriter);
    }

    public static void SendAndRecycle(MessageWriter messageWriter)
    {
        Send(messageWriter);
        messageWriter.Recycle();
    }

    public static void Send(MessageWriter messageWriter)
    {
        AmongUsClient.Instance.SendOrDisconnect(messageWriter);
    }
}
