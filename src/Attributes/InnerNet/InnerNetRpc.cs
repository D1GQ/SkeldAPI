using InnerNet;
using SkeldApi.Attributes.Hook;
using SkeldApi.Enums.InnerNet;
using SkeldApi.Interfaces;
using SkeldApi.Managers;
using SkeldApi.Structs;

namespace SkeldApi.Attributes.InnerNet;

[AttributeUsage(AttributeTargets.Method)]
public sealed class InnerNetRpc(LocalHandling localHandling = LocalHandling.None) : MethodHookAttribute<InnerNetRpc, InnerNetObject>, IUniquelyIdentifiable
{
    public string GUID { get; internal set; } = string.Empty;
    public Gid GID { get; internal set; }
    public readonly LocalHandling LocalHandling = localHandling;

    internal static void RegisterFrom(SkeldPlugin.SkeldMod skeldMod)
    {
        var entries = GetMethodsFrom(skeldMod.Assembly);
        foreach (var entry in entries)
        {
            InnerNetManager.RegisterInnerNetRpc(skeldMod, entry);
            HookMethod(skeldMod, entry);
        }
    }

    protected override object? Hook(HookEntry hookEntry, InnerNetObject? instance, object[] args, OriginalDelegate original)
    {
        return original(instance, args);
    }
}
