namespace SkeldApi.Structs;

public readonly struct Gid(uint value) : IEquatable<Gid>, IComparable<Gid>
{
    public uint Value { get; } = value;

    public static implicit operator uint(Gid id) => id.Value;
    public static implicit operator Gid(uint value) => new(value);

    public bool Equals(Gid other) => Value == other.Value;
    public override bool Equals(object? obj) => obj is Gid other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();
    public int CompareTo(Gid other) => Value.CompareTo(other.Value);

    public static bool operator ==(Gid left, Gid right) => left.Equals(right);
    public static bool operator !=(Gid left, Gid right) => !left.Equals(right);

    public override string ToString() => Value.ToString();
}