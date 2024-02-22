using System;
using Concept.Core.Entities;

namespace Concept.Core.Characteristics;

/// <summary>
/// Essentially represents a custom part of an <see cref="Entity" /> of a given context.
/// Examples: feature, trait of personality, etc.
/// </summary>
/// <remarks>
/// Its identity is not reference-based, is value-based on <see cref="Name" />, so different
/// instances with the same name are considered equal when performing true comparision
/// </remarks>
/// <param name="value">The value of the trait in the entity</param>
public abstract class Characteristic(byte value) : IEquatable<Characteristic>
{
    /// <summary>
    /// Identity name of the characteristic, unique in the whole context of the entity
    /// </summary>
    /// <remarks>Used when performing true comparision</remarks>
    public abstract string Name { get; }

    /// <summary>
    /// A description of the characteristic
    /// </summary>
    public abstract string Description { get; }

    /// <summary>
    /// A value that represents the characteristic
    /// </summary>
    /// <remarks>
    /// A high value implies that is this an important part, a low one, that is an
    /// unimportant part
    /// </remarks>
    public byte Value { get; } = value;

    public override string ToString() => $"{Name}: {Value}";

    public bool Equals(Characteristic? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;

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
