using System.Collections;
using System.Collections.Generic;
using Concept.Core.Nodes;

namespace Concept.Core.Characteristics.YieldStrategies;

/// <summary>
/// Represents a way to yield characteristics from an entity
/// </summary>
/// <param name="yieldFrom"><see cref="Node"/> to yield characteristics</param>
public abstract class CharacteristicYieldStrategy(Node yieldFrom) : IEnumerable<Characteristic>
{
    /// <summary>
    /// The <see cref="Node"/> to yield characteristics from
    /// </summary>
    protected Node YieldFrom { get; } = yieldFrom;

    /// <summary>
    /// The context of execution of the characteristic yield strategy
    /// </summary>
    /// <remarks>If any, might alter strategy output</remarks>
    public ExecutionContext? Context { get; set; }

    /// <summary>
    /// Yields characteristics from the <see cref="YieldFrom"/> <see cref="Node"/> instance
    /// </summary>
    /// <returns>
    /// A sequence of <see cref="Characteristic"/> instances
    /// </returns>
    public abstract IEnumerator<Characteristic> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
