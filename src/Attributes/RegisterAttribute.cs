using System.Reflection;

namespace SkeldApi.Attributes;

public abstract class RegisterAttribute<T, RT> : Attribute where T : class where RT : RegisterAttribute<T, RT>
{
    public static RegisterEntry[] GetRegisterTypesFrom(Assembly assembly)
    {
        List<RegisterEntry> entries = [];
        var types = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && typeof(T).IsAssignableFrom(t));
        foreach (var type in types)
        {
            var customAttr = type.GetCustomAttribute<RT>();
            if (customAttr != null)
            {
                entries.Add(new(type, customAttr));
            }
        }

        return [.. entries];
    }

    public class RegisterEntry(Type type, RT attribute)
    {
        public readonly Type Type = type;
        public readonly RT Attribute = attribute;
    }
}
