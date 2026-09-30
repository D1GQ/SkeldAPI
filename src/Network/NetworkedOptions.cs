using AmongUs.Data;
using AmongUs.GameOptions;
using AmongUs.InnerNet.GameDataMessages;
using Hazel;
using InnerNet;
using SkeldApi.Managers;
using SkeldApi.Structs;

namespace SkeldApi.Network;

internal sealed class NetworkedOptions
{
    private readonly GameOptionsFactory _gameOptionsFactory = new(NullLogger.Instance.Cast<ILogger>());
    private GameOptions _globalGameOptions = default;
    private GameOptions _globalOverrideGameOptions = default;
    private readonly Dictionary<PlayerId, GameOptions> _playerOverrideGameOptions = [];

    internal void Initialize()
    {
        _globalGameOptions = new GameOptions(_gameOptionsFactory.FromBytes(DataManager.Settings.Multiplayer.rawNormalHostOptions));
        _globalOverrideGameOptions = new GameOptions(_gameOptionsFactory.FromBytes(_gameOptionsFactory.ToBytes(_globalGameOptions.Options, false)));
        for (byte i = 0; i < _globalGameOptions.Options.MaxPlayers; i++)
        {
            _playerOverrideGameOptions[i] = new GameOptions(_gameOptionsFactory.FromBytes(_gameOptionsFactory.ToBytes(_globalGameOptions.Options, false)));
        }
    }

    internal void ResetOverrides()
    {
        _globalOverrideGameOptions = new GameOptions(_gameOptionsFactory.FromBytes(_gameOptionsFactory.ToBytes(_globalGameOptions.Options, false)));
        _playerOverrideGameOptions.Clear();
        for (byte i = 0; i < _globalGameOptions.Options.MaxPlayers; i++)
        {
            _playerOverrideGameOptions[i] = new GameOptions(_gameOptionsFactory.FromBytes(_gameOptionsFactory.ToBytes(_globalGameOptions.Options, false)));
        }
    }

    internal GameOptions GetGameOptions()
    {
        return _globalGameOptions;
    }

    internal GameOptions GetTempGameOptions()
    {
        return _globalOverrideGameOptions;
    }

    internal GameOptions GetPlayerGameOptions(PlayerControl player)
    {
        return _playerOverrideGameOptions[player.PlayerId];
    }

    internal void SyncOptions()
    {
        MessageWriter messageWriter = MessageWriter.Get();

        // Pack base game options
        HostManager.StartMessage(messageWriter, () =>
        {
            StartSyncOptionsMessage(messageWriter);
        });

        // Send individual overrides
        foreach (var player in PlayerControl.AllPlayerControls)
        {
            HostManager.StartMessage(messageWriter, () =>
            {
                StartSyncOptionsMessage(messageWriter, player.PlayerId);
            }, player.Data.ClientId);
        }

        HostManager.SendAndRecycle(messageWriter);
    }

    internal void StartSyncOptionsMessage(MessageWriter messageWriter, PlayerId? playerId = null)
    {
        messageWriter.StartMessage((byte)GameDataTypes.DataFlag);
        messageWriter.WritePacked(GameManager.Instance.NetId);

        var logicComponents = GameManager.Instance.LogicComponents;
        for (int i = 0; i < logicComponents.Count; i++)
        {
            var logicComp = logicComponents[i];
            if (logicComp.TryCast<LogicOptions>() != null)
            {
                messageWriter.StartMessage((byte)i);
                if (playerId == null)
                {
                    messageWriter.WriteBytesAndSize(GetOverwrittenOptionsBytes());
                }
                else
                {
                    messageWriter.WriteBytesAndSize(GetOverwrittenOptionsBytes(playerId.Value));
                }

                messageWriter.EndMessage();

                break;
            }
        }

        messageWriter.EndMessage();
    }

    internal byte[] GetOverwrittenOptionsBytes()
    {
        var globalGameOptions = new GameOptions(_gameOptionsFactory.FromBytes(DataManager.Settings.Multiplayer.rawNormalHostOptions));
        globalGameOptions.OverrideFrom(_globalOverrideGameOptions.Options);
        return _gameOptionsFactory.ToBytes(globalGameOptions.Options, false);
    }

    internal byte[] GetOverwrittenOptionsBytes(PlayerId playerId)
    {
        if (!_playerOverrideGameOptions.TryGetValue(playerId, out var playerOptions))
        {
            return GetOverwrittenOptionsBytes();
        }

        var globalGameOptions = new GameOptions(_gameOptionsFactory.FromBytes(DataManager.Settings.Multiplayer.rawNormalHostOptions));
        globalGameOptions.OverrideFrom(_globalOverrideGameOptions.Options);
        globalGameOptions.OverrideFrom(playerOptions.Options);
        return _gameOptionsFactory.ToBytes(globalGameOptions.Options, false);
    }
}
