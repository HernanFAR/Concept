using System;

namespace Concept.Core.Characteristics.Comparers;

/// <summary>
/// A characteristic comparer that uses the name to compare two characteristics
/// </summary>
/// <remarks>
/// Performs a "true comparison", because is based on the name of the characteristic
/// </remarks>
public class TrueCharacteristicComparer : CharacteristicComparer
{
    public static CharacteristicComparer Instance { get; } = new TrueCharacteristicComparer();

    private TrueCharacteristicComparer() { }

    public bool Equals(Characteristic? x, Characteristic? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null) return false;
        if (y is null) return false;
        if (x.GetType() != y.GetType()) return false;

        return x.Name == y.Name;
    }

    public int GetHashCode(Characteristic obj)
    {
        return HashCode.Combine(obj.Name, obj.Description, obj.Value);
    }
}
