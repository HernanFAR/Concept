using System;

namespace Concept.Core.Characteristics.Comparers;

/// <summary>
/// A <see cref="CharacteristicComparer"/> implementation that uses the name to compare
/// two characteristics
/// </summary>
/// <remarks>
/// Performs a "true comparison", because is based on the name of the characteristic
/// </remarks>
public sealed class TrueCharacteristicComparer : CharacteristicComparer
{
    /// <summary>
    /// The only instance of this class
    /// </summary>
    public static CharacteristicComparer Instance { get; } = new TrueCharacteristicComparer();

    TrueCharacteristicComparer() { }

    /// <inheritdoc />
    public override bool Equals(Characteristic? left, Characteristic? right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (right is null) return false;
        if (left is null) return false;
        if (right.GetType() != left.GetType()) return false;

        return right.Name == left.Name;
    }

    /// <inheritdoc />
    public override int GetHashCode(Characteristic obj)
    {
        return HashCode.Combine(obj.Name);
    }
}
