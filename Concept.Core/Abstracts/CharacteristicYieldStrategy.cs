using System.Collections;
using System.Collections.Generic;

namespace Concept.Core.Abstracts;

public abstract class CharacteristicYieldStrategy(Entity entity) : IEnumerable<Characteristic>
{
    protected Entity Entity { get; } = entity;

    public abstract IEnumerator<Characteristic> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
