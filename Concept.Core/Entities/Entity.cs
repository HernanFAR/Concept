using Concept.Core.Characteristics;
using Concept.Core.Characteristics.Comparers;
using Concept.Core.Characteristics.YieldStrategies;
using Concept.Core.Relations;
using System.Collections.Generic;

namespace Concept.Core.Entities;

// TODO: An Entity might be mistaken by other, so the key is not a way to identify the entity, but to compare it with other entities as Characteristics, we need to create a IEntityComparer, and use it to compare entities

/// <summary>
/// An entity is a thing that exists in a specific context of a world. Examples: Person, place, thing,
/// identity, society.
/// </summary>
/// <remarks>
/// Its identity is reference-based, so only same instances are considered equal when performing true
/// comparision
/// </remarks>
/// <param name="name">The name of the entity</param>
/// <param name="relations">The relations of this Entity with others</param>
public abstract class Entity(
    string? name,
    IRelationDictionary relations)
{
    /// <summary>
    /// The name of the entity
    /// </summary>
    public string? Name { get; } = name;

    /// TODO: If the entity can't interact with others, then it cannot have relations, only characteristics
    /// <summary>
    /// The relations of this Entity with others
    /// </summary>
    public IRelationDictionary Relations { get; } = relations;

    /// <summary>
    /// The personality traits of this entity
    /// </summary>
    public virtual IEnumerable<Characteristic> Characteristics => CharacteristicYieldStrategy;

    /// <summary>
    /// Specifies if this entity can start an interaction with others, or if it can only be interacted with.
    /// </summary>
    /// <remarks>A false value implies that the entity is not alive</remarks>
    public abstract bool CanInteractWithOthers { get; }

    /// <summary>
    /// The strategy to yield the characteristics of this entity
    /// </summary>
    protected abstract CharacteristicYieldStrategy CharacteristicYieldStrategy { get; }

    /// <summary>
    /// The comparer to use when it does not have a relation with another entity
    /// </summary>
    public abstract CharacteristicComparer DefaultComparer { get; }

    /// <summary>
    /// Gets the <see cref="RelationContext"/> of this entity with another one, if any
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> to get the <see cref="RelationContext"/></param>
    /// <returns>The <see cref="RelationContext"/> related to the entity, if any </returns>
    public RelationContext? GetRelationContext(Entity entity)
    {
        // TODO: When we create the IEntityComparer, use it here.
        // Currently, we are using the reference to compare entities.
        return Relations.TryGetValue(entity, out var context)
            ? context
            : null;
    }
}
