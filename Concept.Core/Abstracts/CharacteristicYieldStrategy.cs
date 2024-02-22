using System.Collections;
using System.Collections.Generic;

namespace Concept.Core.Abstracts;


public abstract class CharacteristicYieldStrategy(Entity YieldFrom) : IEnumerable<Characteristic>
{
    public record StrategyContext(Entity? PointOfView);

    protected Entity YieldFrom { get; } = YieldFrom;

    public StrategyContext? Context { get; set; } 

    public abstract IEnumerator<Characteristic> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
