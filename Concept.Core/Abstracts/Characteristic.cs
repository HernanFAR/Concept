using System;

namespace Concept.Abstracts;

public abstract class Characteristic(byte value) : IEquatable<Characteristic>
{
    public abstract string Name { get; }
    public abstract string Description { get; }

    public byte Value { get; } = value;

    public override string ToString() => $"{Name}: {Value}";

    public bool Equals(Characteristic? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Characteristic)obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public static bool operator ==(Characteristic? left, Characteristic? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Characteristic? left, Characteristic? right)
    {
        return !Equals(left, right);
    }
}
