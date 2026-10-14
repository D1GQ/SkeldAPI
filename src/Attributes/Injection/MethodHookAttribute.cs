using MonoMod.RuntimeDetour;
using System.Reflection;
using System.Reflection.Emit;

namespace SkeldApi.Attributes.Injection;

public abstract class MethodHookAttribute<T, IT> : Attribute where T : MethodHookAttribute<T, IT> where IT : class
{
    public delegate object? OriginalDelegate(IT? instance, object[] args);

    private sealed class HookState(HookEntry hookEntry, Delegate stub, IDetour detour, OriginalDelegate original)
    {
        public readonly HookEntry HookEntry = hookEntry;
        public readonly Delegate Stub = stub;
        public readonly IDetour Detour = detour;
        public readonly OriginalDelegate Original = original;
    }

    private static readonly Dictionary<MethodInfo, HookState> Hooks = [];

    public static HookEntry[] GetMethodsFrom(Assembly assembly)
    {
        var entries = new List<HookEntry>();
        var types = assembly.GetTypes();
        foreach (var type in types)
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

    public static void HookMethod(HookEntry entry)
    {
        var target = entry.MethodInfo;
        if (Hooks.ContainsKey(target))
            return;

        var stub = BuildStub(target);
        var detour = new Detour(target, stub.Method);

        try
        {
            var trampoline = (DynamicMethod)detour.GenerateTrampoline();
            var original = BuildOriginalAdapter(trampoline, target);

            Hooks.Add(target, new HookState(entry, stub, detour, original));
        }
        catch
        {
            detour.Dispose();
            throw;
        }
    }

    public static OriginalDelegate? GetOriginal(HookEntry entry) => Hooks.TryGetValue(entry.MethodInfo, out var state) ? state.Original : null;

    internal static object? Dispatch(MethodInfo target, IT? instance, object[] args)
    {
        var state = Hooks[target];
        return state.HookEntry.Attribute.Hook(state.HookEntry, instance, args, state.Original);
    }

    private static Delegate BuildStub(MethodInfo target)
    {
        var parameters = target.GetParameters();
        var offset = target.IsStatic ? 0 : 1;
        var signature = target.IsStatic
            ? parameters.Select(p => p.ParameterType).ToArray()
            : [target.DeclaringType!, .. parameters.Select(p => p.ParameterType)];

        var method = new DynamicMethod(
            $"Hook_{target.DeclaringType?.Name}_{target.Name}",
            target.ReturnType,
            signature);

        var il = method.GetILGenerator();
        var args = il.DeclareLocal(typeof(object[]));

        il.Emit(OpCodes.Ldc_I4, parameters.Length);
        il.Emit(OpCodes.Newarr, typeof(object));
        il.Emit(OpCodes.Stloc, args);

        for (var i = 0; i < parameters.Length; i++)
        {
            var type = parameters[i].ParameterType;
            if (type.IsByRef || type.IsPointer)
                throw new NotSupportedException($"Hook arguments of type {type} are not supported: {target}");

            il.Emit(OpCodes.Ldloc, args);
            il.Emit(OpCodes.Ldc_I4, i);
            il.Emit(OpCodes.Ldarg, i + offset);
            if (type.IsValueType)
                il.Emit(OpCodes.Box, type);
            il.Emit(OpCodes.Stelem_Ref);
        }

        il.Emit(OpCodes.Ldtoken, target);
        il.Emit(OpCodes.Call, typeof(MethodBase).GetMethod(
            nameof(MethodBase.GetMethodFromHandle),
            [typeof(RuntimeMethodHandle)])!);
        il.Emit(OpCodes.Castclass, typeof(MethodInfo));

        if (target.IsStatic)
            il.Emit(OpCodes.Ldnull);
        else
        {
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Castclass, typeof(IT));
        }

        il.Emit(OpCodes.Ldloc, args);
        il.Emit(OpCodes.Call, typeof(MethodHookAttribute<T, IT>).GetMethod(
            nameof(Dispatch), BindingFlags.NonPublic | BindingFlags.Static)!);

        if (target.ReturnType == typeof(void))
            il.Emit(OpCodes.Pop);
        else if (target.ReturnType.IsValueType)
            il.Emit(OpCodes.Unbox_Any, target.ReturnType);
        else
            il.Emit(OpCodes.Castclass, target.ReturnType);

        il.Emit(OpCodes.Ret);

        return method.CreateDelegate(CreateDelegateType(target.ReturnType, signature));
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
            var type = parameters[i].ParameterType;
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldc_I4, i);
            il.Emit(OpCodes.Ldelem_Ref);
            il.Emit(type.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass, type);
        }

        il.Emit(OpCodes.Call, trampoline);

        if (target.ReturnType == typeof(void))
            il.Emit(OpCodes.Ldnull);
        else if (target.ReturnType.IsValueType)
            il.Emit(OpCodes.Box, target.ReturnType);

        il.Emit(OpCodes.Ret);
        return method.CreateDelegate<OriginalDelegate>();
    }

    private static Type CreateDelegateType(Type returnType, Type[] parameters)
    {
        var assembly = AssemblyBuilder.DefineDynamicAssembly(
            new AssemblyName($"SkeldHook_{Guid.NewGuid():N}"), AssemblyBuilderAccess.Run);
        var builder = assembly.DefineDynamicModule("Hooks").DefineType(
            "HookDelegate", TypeAttributes.Public | TypeAttributes.Sealed, typeof(MulticastDelegate));

        builder.DefineConstructor(
            MethodAttributes.Public | MethodAttributes.RTSpecialName |
            MethodAttributes.HideBySig | MethodAttributes.SpecialName,
            CallingConventions.Standard, [typeof(object), typeof(IntPtr)])
            .SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);

        builder.DefineMethod("Invoke",
            MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.NewSlot |
            MethodAttributes.HideBySig, returnType, parameters)
            .SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);

        return builder.CreateType()!;
    }

    protected abstract object? Hook(HookEntry hookEntry, IT? instance, object[] args, OriginalDelegate original);

    public sealed class HookEntry(Type type, MethodInfo methodInfo, T attribute)
    {
        public readonly Type Type = type;
        public readonly MethodInfo MethodInfo = methodInfo;
        public readonly T Attribute = attribute;
    }
}