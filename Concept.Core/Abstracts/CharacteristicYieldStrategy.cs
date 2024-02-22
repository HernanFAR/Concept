using System.Collections;
using System.Collections.Generic;

namespace Concept.Core.Abstracts;

/// <summary>
/// Represents a way to yield characteristics from an entity
/// </summary>
/// <param name="yieldFrom">Entity to yield characteristics </param>
public abstract class CharacteristicYieldStrategy(Entity yieldFrom) : IEnumerable<Characteristic>
{
    /// <summary>
    /// A context in which the strategy is being used, that might alter the strategy
    /// </summary>
    /// <param name="PointOfView">An <see cref="Entity"/> that is analyzing the characteristics of <see cref="YieldFrom"/> </param>
    public record StrategyContext(Entity? PointOfView);

    /// <summary>
    /// A reference to the entity that the strategy is yielding characteristics from
    /// </summary>
    protected Entity YieldFrom { get; } = yieldFrom;

    /// <summary>
    /// The context of use
    /// </summary>
    /// <remarks>If any, might alter the strategy</remarks>
    public StrategyContext? Context { get; set; } 
    
    public abstract IEnumerator<Characteristic> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
