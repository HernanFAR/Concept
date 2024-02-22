using Concept.Core.Entities;

namespace Concept.Core.Relations;

/// <summary>
/// Represents key elements of relation between two <see cref="Entity"/> instances
/// </summary>
/// <param name="with">Related <see cref="Entity"/></param>
/// <param name="kind">The kind of relation</param>
/// <param name="charComparer">
/// A comparer used when dealing with characteristics of the related <see cref="Entity"/>
/// </param>
public readonly struct RelationContext(Entity with, RelationKind kind, ICharacteristicComparer charComparer)
{
    /// <summary>
    /// The related <see cref="Entity"/>
    /// </summary>
    public Entity With { get; } = with;

    /// <summary>
    /// The kind of relation
    /// </summary>
    public RelationKind Kind { get; } = kind;

    /// <summary>
    /// The comparer to use when comparing characteristics of the related <see cref="Entity"/>
    /// </summary>
    public ICharacteristicComparer CharComparer { get; } = charComparer;

    public void Deconstruct(out RelationKind kind, out ICharacteristicComparer charComparer)
    {
        kind = Kind;
        charComparer = CharComparer;
    }
}
