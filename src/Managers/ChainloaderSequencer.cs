using SkeldApi.Structs;
using System.Security.Cryptography;
using System.Text;

namespace SkeldApi.Managers;

internal static class ChainloaderSequencer
{
    internal static Guid ChainloaderGuid { get; private set; } = Guid.Empty;
    private static readonly Dictionary<string, Action<Gid>> SequencedActions = [];
    private static readonly Dictionary<string, int> ActionPriorities = [];
    private static bool _hasRun;
    private static uint _nextGid;

    internal static Gid GetNextGlobalIdentifier() => new(++_nextGid);
    internal static void SequenceAction(string actionGuid, Action<Gid> action, int priority = 0)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (_hasRun)
        {
            SkeldPlugin.Logger.LogWarning(
                $"Cannot register action '{actionGuid}': sequence has already run.");
            return;
        }

        if (!SequencedActions.TryAdd(actionGuid, action))
        {
            SkeldPlugin.Logger.LogWarning(
                $"Action with identifier '{actionGuid}' is already registered. Skipping duplicate.");
            return;
        }

        ActionPriorities[actionGuid] = priority;
    }

    internal static void RunSequenced()
    {
        if (_hasRun)
        {
            SkeldPlugin.Logger.LogWarning("RunSequenced was already executed.");
            return;
        }

        _hasRun = true;

        var ordered = SequencedActions
            .OrderByDescending(kvp => ActionPriorities[kvp.Key])
            .ThenBy(kvp => kvp.Key, StringComparer.Ordinal)
            .ToList();

        SequencedActions.Clear();
        ActionPriorities.Clear();

        ChainloaderGuid = ComputeChainloaderId(ordered.Select(kvp => kvp.Key));

        SkeldPlugin.Logger.LogInfo($"ChainloaderSequencer `{ChainloaderGuid.ToString()}` Starting");

        foreach (var (guid, action) in ordered)
        {
            try
            {
                action(GetNextGlobalIdentifier());
            }
            catch (Exception ex)
            {
                SkeldPlugin.Logger.LogError($"'{guid}' Sequence threw an exception: {ex}");
            }
        }

        SkeldPlugin.Logger.LogInfo($"ChainloaderSequencer Finished!");
    }

    private static Guid ComputeChainloaderId(IEnumerable<string> orderedIds)
    {
        using var sha = SHA256.Create();
        using var ms = new MemoryStream();

        foreach (var id in orderedIds)
        {
            var bytes = Encoding.UTF8.GetBytes(id);
            ms.WriteByte((byte)(bytes.Length & 0xFF));
            ms.WriteByte((byte)((bytes.Length >> 8) & 0xFF));
            ms.Write(bytes, 0, bytes.Length);
        }

        var hash = sha.ComputeHash(ms.ToArray());

        var guidBytes = new byte[16];
        Array.Copy(hash, guidBytes, 16);

        guidBytes[7] = (byte)((guidBytes[7] & 0x0F) | 0x50);
        guidBytes[8] = (byte)((guidBytes[8] & 0x3F) | 0x80);

        return new Guid(guidBytes);
    }
}