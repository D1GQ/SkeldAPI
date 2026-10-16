using SkeldApi.Managers;
using SkeldApi.Network.InnerNet;

namespace SkeldApi.Attributes.Register;

[AttributeUsage(AttributeTargets.Class)]
public sealed class RegisterCustomInnerNetObjectAttribute : RegisterAttribute<CustomInnerNetObject, RegisterCustomInnerNetObjectAttribute>
{
    internal static void RegisterFrom(SkeldPlugin.SkeldMod skeldMod)
    {
        var entries = GetRegisterTypesFrom(skeldMod.Assembly);
        foreach (var entry in entries)
        {
            InnerNetManager.RegisterCustomInnerNetObject(skeldMod, entry.Type);
        }
    }
}
