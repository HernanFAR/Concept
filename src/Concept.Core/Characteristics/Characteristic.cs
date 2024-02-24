using Concept.Core.Nodes;
using System;

namespace Concept.Core.Characteristics;

/// <summary>
/// Essentially represents a custom part of a <see cref="Node" />, like a feature, a trait of personality, etc.
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

    /// <inheritdoc />
    public override string ToString() => $"{Name}: {Value}";

    /// <summary>
    /// Performs a true comparison between two <see cref="Characteristic"/> instances
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Characteristic? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return Name == other.Name;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;

        return Equals((Characteristic)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    /// <summary>
    /// Determines if two <see cref="Characteristic"/> instances are equal
    /// </summary>
    /// <param name="left">Left characteristic</param>
    /// <param name="right">Right characteristic</param>
    /// <returns>True if equals, false if not</returns>
    public static bool operator ==(Characteristic? left, Characteristic? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Determines if two <see cref="Characteristic"/> instances are not equal
    /// </summary>
    /// <param name="left">Left characteristic</param>
    /// <param name="right">Right characteristic</param>
    /// <returns>False if equals, true if not</returns>
    public static bool operator !=(Characteristic? left, Characteristic? right)
    {
        return !Equals(left, right);
    }
}
