namespace SkeldApi;

public partial class SkeldPlugin
{
    public static class Constants
    {
        internal static class Chainloader
        {
            internal const int CHAINLOADER_SKELDMOD_SELF_PRIORITY = int.MaxValue;
            internal const int CHAINLOADER_SKELDMOD_PRIORITY = 6000;
            internal const int CHAINLOADER_COMMAND_PRIORITY = 5000;
            internal const int CHAINLOADER_GAMEMODE_PRIORITY = 4000;
            internal const int CHAINLOADER_CUSTOM_ROLE_PRIORITY = 3000;
            internal const int CHAINLOADER_ROLE_MODIFIER_PRIORITY = 2000;
            internal const int CHAINLOADER_CUSTOM_INNERNET_OBJECT_PRIORITY = 1000;
            internal const int CHAINLOADER_HOOK_PRIORITY = 0;
        }
    }
}