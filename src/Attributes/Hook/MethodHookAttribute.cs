using MonoMod.RuntimeDetour;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace SkeldApi.Attributes.Hook;

public abstract class MethodHookAttribute<T, IT> : Attribute where T : MethodHookAttribute<T, IT> where IT : class
{
    public delegate object? OriginalDelegate(IT? instance, object[] args);

    private static readonly MethodInfo DispatchMethod =
        typeof(MethodHookAttribute<T, IT>).GetMethod(nameof(Dispatch), BindingFlags.NonPublic | BindingFlags.Static)!;

    private static readonly Dictionary<MethodInfo, HookState> HooksByMethod = [];
    private static readonly List<HookState> HooksById = [];

    private sealed class HookState(HookEntry hookEntry, Delegate stub, IDetour detour, OriginalDelegate original)
    {
        public readonly HookEntry HookEntry = hookEntry;
        public readonly Delegate Stub = stub;
        public readonly IDetour Detour = detour;
        public readonly OriginalDelegate Original = original;
    }

    public static HookEntry[] GetMethodsFrom(Assembly assembly)
    {
        var entries = new List<HookEntry>();

        foreach (var type in assembly.GetTypes())
        {
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (var method in methods)
            {
                var attr = method.GetCustomAttribute<T>();
                if (attr is not null)
                    entries.Add(new HookEntry(type, method, attr));
            }
        }

        return [.. entries];
    }

    public static void HookMethod(SkeldPlugin.SkeldMod skeldMod, HookEntry entry)
    {
        var target = entry.MethodInfo;

        if (GetUnsupportedReason(target, out string reason))
        {
            SkeldPlugin.Logger.LogWarning($"{typeof(T).Name} Failed to hook {target.DeclaringType!.Name}.{target.Name} from {skeldMod.PluginInfo.Metadata.Name}: {reason}");
            return;
        }

        if (HooksByMethod.ContainsKey(target))
            return;

        var stub = BuildStub(target, HooksById.Count);
        var detour = new Detour(target, stub.Method);

        try
        {
            var trampoline = (DynamicMethod)detour.GenerateTrampoline();
            var state = new HookState(entry, stub, detour, BuildOriginalAdapter(trampoline, target));

            HooksById.Add(state);
            HooksByMethod.Add(target, state);
        }
        catch
        {
            detour.Dispose();
            throw;
        }
    }

    public static OriginalDelegate? GetOriginal(HookEntry entry) =>
        HooksByMethod.TryGetValue(entry.MethodInfo, out var state) ? state.Original : null;

    internal static object? Dispatch(int id, IT? instance, object[] args)
    {
        var state = HooksById[id];
        return state.HookEntry.Attribute.Hook(state.HookEntry, instance, args, state.Original);
    }

    private static bool GetUnsupportedReason(MethodInfo method, out string reason)
    {
        if (method.IsGenericMethod)
        {
            reason = "Method cannot be generic!";
            return true;
        }

        if (!method.IsStatic && method.DeclaringType!.IsValueType)
        {
            reason = "Instance methods on value types are unsupported!";
            return true;
        }

        foreach (var p in method.GetParameters())
        {
            if (p.ParameterType.IsPointer)
            {
                reason = $"Parameter '{p.Name}' is a pointer, which is unsupported!";
                return true;
            }

            if (p.ParameterType.IsByRef)
            {
                reason = $"Parameter '{p.Name}' uses the unsupported '{(p.IsOut ? "out" : p.IsIn ? "in" : "ref")}' modifier!";
                return true;
            }
        }

        reason = string.Empty;
        return false;
    }

    private static void EmitCast(ILGenerator il, Type type) =>
        il.Emit(type.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass, type);

    private static Delegate BuildStub(MethodInfo target, int id)
    {
        Type[] paramTypes = [.. target.GetParameters().Select(p => p.ParameterType)];
        Type[] signature = target.IsStatic ? paramTypes : [target.DeclaringType!, .. paramTypes];
        var offset = signature.Length - paramTypes.Length;

        var method = new DynamicMethod(
            $"Hook_{target.DeclaringType?.Name}_{target.Name}",
            target.ReturnType,
            signature,
            typeof(MethodHookAttribute<T, IT>),
            skipVisibility: true);

        var il = method.GetILGenerator();
        var args = il.DeclareLocal(typeof(object[]));

        il.Emit(OpCodes.Ldc_I4, paramTypes.Length);
        il.Emit(OpCodes.Newarr, typeof(object));
        il.Emit(OpCodes.Stloc, args);

        for (var i = 0; i < paramTypes.Length; i++)
        {
            il.Emit(OpCodes.Ldloc, args);
            il.Emit(OpCodes.Ldc_I4, i);
            il.Emit(OpCodes.Ldarg, (short)(i + offset));

            if (paramTypes[i].IsValueType)
                il.Emit(OpCodes.Box, paramTypes[i]);

            il.Emit(OpCodes.Stelem_Ref);
        }

        il.Emit(OpCodes.Ldc_I4, id);

        if (target.IsStatic)
            il.Emit(OpCodes.Ldnull);
        else
        {
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Castclass, typeof(IT));
        }

        il.Emit(OpCodes.Ldloc, args);
        il.Emit(OpCodes.Call, DispatchMethod);

        if (target.ReturnType == typeof(void))
            il.Emit(OpCodes.Pop);
        else
            EmitCast(il, target.ReturnType);

        il.Emit(OpCodes.Ret);

        return method.CreateDelegate(Expression.GetDelegateType([.. signature, target.ReturnType]));
    }

    private static OriginalDelegate BuildOriginalAdapter(DynamicMethod trampoline, MethodInfo target)
    {
        var parameters = target.GetParameters();

        var method = new DynamicMethod(
            $"Original_{target.DeclaringType?.Name}_{target.Name}",
            typeof(object),
            [typeof(IT), typeof(object[])]);

        var il = method.GetILGenerator();

        if (!target.IsStatic)
        {
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Castclass, target.DeclaringType!);
        }

        for (var i = 0; i < parameters.Length; i++)
        {
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldc_I4, i);
            il.Emit(OpCodes.Ldelem_Ref);
            EmitCast(il, parameters[i].ParameterType);
        }

        il.Emit(OpCodes.Call, trampoline);

        if (target.ReturnType == typeof(void))
            il.Emit(OpCodes.Ldnull);
        else if (target.ReturnType.IsValueType)
            il.Emit(OpCodes.Box, target.ReturnType);

        il.Emit(OpCodes.Ret);
        return method.CreateDelegate<OriginalDelegate>();
    }

    protected abstract object? Hook(HookEntry hookEntry, IT? instance, object[] args, OriginalDelegate original);

    public sealed class HookEntry(Type type, MethodInfo methodInfo, T attribute)
    {
        public readonly Type Type = type;
        public readonly MethodInfo MethodInfo = methodInfo;
        public readonly T Attribute = attribute;
        public OriginalDelegate? Original => GetOriginal(this);
    }
}