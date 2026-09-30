using HarmonyLib;
using Hazel;

namespace SkeldApi.Patches.Gameplay.Network;

[HarmonyPatch]
internal static class OptionsSyncPatch
{
    [HarmonyPatch(typeof(GameManager), nameof(GameManager.Serialize))]
    [HarmonyPrefix]
    private static bool GameManager_Serialize_Prefix(GameManager __instance, MessageWriter writer, bool initialState, ref bool __result)
    {
        bool didSerializeComp = false;
        for (int i = 0; i < __instance.LogicComponents.Count; i++)
        {
            GameLogicComponent gameLogicComponent = __instance.LogicComponents[i];
            if (gameLogicComponent.TryCast<LogicOptions>() != null && !initialState && __instance.GameHasStarted)
                continue;

            if (initialState || gameLogicComponent.IsDirty)
            {
                didSerializeComp = true;
                writer.StartMessage((byte)i);
                gameLogicComponent.Serialize(writer);
                writer.EndMessage();
            }
        }

        __result = didSerializeComp;
        return false;
    }
}
