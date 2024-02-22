using System.Collections.Generic;

namespace Concept.Core.Characteristics.Comparers;

public abstract class CharacteristicComparer : IEqualityComparer<Characteristic>
{
    public abstract bool Equals(Characteristic x, Characteristic y);

    public abstract int GetHashCode(Characteristic obj);
}
