using System;
using Concept.Core.Abstracts;
using Concept.Core.Interfaces;

namespace Concept.Core.Characteristics.Comparers;

public class TrueEqualityCharacteristicComparer : ICharacteristicComparer
{
    public static ICharacteristicComparer Instance { get; } = new TrueEqualityCharacteristicComparer();
    private TrueEqualityCharacteristicComparer() { }

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
