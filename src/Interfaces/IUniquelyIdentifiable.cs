using SkeldApi.Structs;

namespace SkeldApi.Interfaces;

public interface IUniquelyIdentifiable
{
    string GUID { get; }
    Gid GID { get; }
}
