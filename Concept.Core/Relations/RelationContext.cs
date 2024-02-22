using Concept.Core.Abstracts;
using Concept.Core.Interfaces;

namespace Concept.Core.Relations;

/// <summary>
/// An object that represents characteristics of relation between two entities
/// </summary>
/// <param name="kind">The kind of relation</param>
/// <param name="charComparer">
/// A comparer used when dealing with characteristics of the related <see cref="Entity"/>
/// </param>
public readonly struct RelationContext(RelationKind kind, ICharacteristicComparer charComparer)
{
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
        kind = this.Kind;
        charComparer = this.CharComparer;
    }
}
