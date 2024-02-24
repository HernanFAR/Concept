using Concept.Core.Characteristics;
using Concept.Core.Characteristics.Comparers;
using Concept.Core.Characteristics.YieldStrategies;
using Concept.Core.Relations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Concept.Core.Nodes;

/// <summary>
/// A concept that exists in a specific context of a world. Examples: Person, place, thing,
/// identity, society.
/// </summary>
/// <remarks>
/// Its identity is reference-based, so only same instances are considered equal when performing true
/// comparision
/// </remarks>
public abstract class Node
{
    /// <summary>
    /// Creates a new instance of <see cref="Node"/>
    /// </summary>
    /// <param name="name">The name of the node</param>
    protected Node(string? name)
    {
        Name = name;
        Relations = new RelationDictionary(this);
    }

    /// <summary>
    /// The name of the node
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// The relations of this Node with others
    /// </summary>
    public RelationDictionary? Relations { get; }

    /// <summary>
    /// The personality traits of this node
    /// </summary>
    public virtual IEnumerable<Characteristic> Characteristics => CharacteristicYieldStrategy;

    /// <summary>
    /// Specifies if this node can start an interaction with others, or if it can only be interacted with.
    /// </summary>
    /// <remarks>
    /// A false value implies that the node, might not be "alive".
    /// </remarks>
    [MemberNotNullWhen(true, nameof(Relations))]
    public abstract bool CanInteractWithOthers { get; }

    /// <summary>
    /// The strategy to yield the characteristics of this node
    /// </summary>
    protected abstract CharacteristicYieldStrategy CharacteristicYieldStrategy { get; }

    /// <summary>
    /// The comparer to use when it does not have a relation with another node
    /// </summary>
    public abstract CharacteristicComparer DefaultComparer { get; }

    /// <summary>
    /// Gets the <see cref="RelationContext"/> of this node with another one, if any
    /// </summary>
    /// <param name="node">The <see cref="Node"/> to get the <see cref="RelationContext"/></param>
    /// <returns>The <see cref="RelationContext"/> related to the node, if any </returns>
    public RelationContext? GetRelationContext(Node node)
    {
        if (CanInteractWithOthers is false)
        {
            throw new InvalidOperationException("This node cannot interact with others");
        }

        return Relations.TryGetValue(node, out RelationContext context)
            ? context
            : null;
    }
}
