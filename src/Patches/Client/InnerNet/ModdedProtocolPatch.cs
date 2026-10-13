using HarmonyLib;

namespace SkeldApi.Patches.Client.InnerNet;

[HarmonyPatch]
internal sealed class ModdedProtocolPatch
{
    internal static bool TryToGenerateGuid(out Guid guid)
    {
        guid = default;
        var mods = SkeldPlugin.SkeldMods.OrderBy(mod => mod.GetGuid()).ToList();
        if (mods.Count == 0)
            return false;

        using var sha = System.Security.Cryptography.SHA256.Create();
        var bytes = new byte[16];
        var input = string.Join("|", mods.Select(m => m.GetGuid()));
        var hash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
        Array.Copy(hash, bytes, 16);
        bytes[7] = (byte)(bytes[7] & 0x0F | 0x40);
        bytes[8] = (byte)(bytes[8] & 0x3F | 0x80);
        guid = new Guid(bytes);
        return true;
    }

    [HarmonyPatch(typeof(CurrentModRegistration), nameof(CurrentModRegistration.TryGetModRegistrationGuid))]
    [HarmonyPrefix]
    private static bool CurrentModRegistration_TryGetModRegistrationGuid_Prefix(out Guid guid, ref bool __result)
    {
        if (AmongUsClient.Instance.NetworkMode == NetworkModes.LocalGame)
        {
            guid = default;
            __result = false;
            return false;
        }

        if (TryToGenerateGuid(out var id))
        {
            guid = id;
            __result = true;
            return false;
        }

        guid = default;
        __result = false;
        return false;
    }

    [HarmonyPatch(typeof(Constants), nameof(Constants.GetBroadcastVersion))]
    [HarmonyPostfix]
    private static void Constants_GetBroadcastVersion_Postfix(ref int __result)
    {
        if (AmongUsClient.Instance.NetworkMode == NetworkModes.OnlineGame)
        {
            var revision = __result % 50;
            if (revision < 25)
            {
                __result += 25;
            }
        }
    }

    [HarmonyPatch(typeof(Constants), nameof(Constants.IsVersionModded))]
    [HarmonyPostfix]
    private static void Constants_VersionModded_Postfix(ref bool __result)
    {
        __result = true;
    }
}
