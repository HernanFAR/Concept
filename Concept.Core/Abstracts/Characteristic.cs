using System;

namespace Concept.Core.Abstracts;

/// <summary>
/// Represents a trait of personality of an <see cref="Entity" />.
/// </summary>
/// <param name="value">The value of the trait in the entity</param>
public abstract class Characteristic(byte value) : IEquatable<Characteristic>
{
    /// <summary>
    /// The name of the characteristic
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// A description of the characteristic
    /// </summary>
    public abstract string Description { get; }

    /// <summary>
    /// A value that represents the characteristic
    /// </summary>
    /// <remarks>A high value implies that is this an important trait, a low one, that is an unimportant trait</remarks>
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
