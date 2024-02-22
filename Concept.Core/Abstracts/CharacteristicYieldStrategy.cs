using System.Collections;
using System.Collections.Generic;

namespace Concept.Core.Abstracts;

public record StrategyContext(Entity PointOfView);

public abstract class CharacteristicYieldStrategy(Entity fromEntity) : IEnumerable<Characteristic>
{
    protected Entity FromEntity { get; } = fromEntity;
    protected StrategyContext? Context { get; private set; } 

    public void SetContext(StrategyContext context)
    {
        Context = context;
    }

    public abstract IEnumerator<Characteristic> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
