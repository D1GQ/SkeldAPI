namespace SkeldApi.Utilities;

public static class Utils
{
    internal static void CompactIds(uint[] ids)
    {
        uint[] sorted = (uint[])ids.Clone();
        Array.Sort(sorted);

        for (int i = 0; i < ids.Length; i++)
        {
            ids[i] = (uint)(Array.IndexOf(sorted, ids[i]) + 1);
        }
    }
}