using HarmonyLib;

namespace SkeldApi.Patches.Client.Net;

[HarmonyPatch]
internal sealed class ModdedProtocolPatch
{
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

        // Looking into automatically generating a guid based off of mods and their versions

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
