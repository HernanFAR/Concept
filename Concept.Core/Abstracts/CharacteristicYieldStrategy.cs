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
    /// Context of use, that can alter the strategy output
    /// </summary>
    /// <param name="pointOfView">An optional <see cref="Entity"/> that is analyzing the characteristics of <see cref="YieldFrom"/> </param>
    public class StrategyContext(Entity? pointOfView)
    {
        /// <summary>
        /// An optional <see cref="Entity"/> that is analyzing the characteristics of <see cref="YieldFrom"/>
        /// </summary>
        public Entity? PointOfView { get; } = pointOfView;

        public void Deconstruct(out Entity? pointOfView)
        {
            pointOfView = this.PointOfView;
        }
    }

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
