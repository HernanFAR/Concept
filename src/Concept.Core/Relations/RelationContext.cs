using Concept.Core.Characteristics.Comparers;
using Concept.Core.Nodes;

namespace Concept.Core.Relations;

/// <summary>
/// Represents key elements of relation between two <see cref="Node"/> instances
/// </summary>
/// <param name="with">Related <see cref="Node"/></param>
/// <param name="kind">The kind of relation</param>
/// <param name="charComparer">
/// A comparer used when dealing with characteristics of the related <see cref="Node"/>
/// </param>
public readonly struct RelationContext(Node with,
    RelationKind kind, byte relevance, CharacteristicComparer charComparer)
{
    /// <summary>
    /// The related <see cref="Node"/>
    /// </summary>
    public Node With { get; } = with;

    /// <summary>
    /// The kind of relation
    /// </summary>
    public RelationKind Kind { get; } = kind;

    /// <summary>
    /// The relevance of the relation
    /// </summary>
    public byte Relevance { get; } = relevance;

    /// <summary>
    /// The comparer to use when comparing characteristics of the related <see cref="Node"/>
    /// </summary>
    public CharacteristicComparer CharComparer { get; } = charComparer;

    /// <summary>
    /// Deconstructs the <see cref="RelationContext"/> into its components
    /// </summary>
    /// <param name="kind">
    /// The kind of relation
    /// </param>
    /// <param name="relevance">
    /// The relevance of the relation
    /// </param>
    /// <param name="charComparer">
    /// The comparer to use when comparing characteristics of the related <see cref="Node"/>
    /// </param>
    public void Deconstruct(out RelationKind kind, out byte relevance, out CharacteristicComparer charComparer)
    {
        kind = Kind;
        relevance = Relevance;
        charComparer = CharComparer;
    }
}
