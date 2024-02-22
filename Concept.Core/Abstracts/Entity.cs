using Concept.Core.Interfaces;
using Concept.Core.Structs;
using System;
using System.Collections.Generic;

namespace Concept.Core.Abstracts;

// TODO: An Entity might be mistaken by other, so the key is not a way to identify the entity, but to compare it with other entities as Characteristics, we need to create a IEntityComparer, and use it to compare entities

/// <summary>
/// An entity is a thing that exists in the world, it can be a person, a place, a thing, an idea, a society.
/// </summary>
/// <remarks>The entity can be a pure one (like a person), but it might be also emergent entity.</remarks>
/// <param name="name">The name of the entity</param>
/// <param name="relations">The relations of this Entity with others</param>
public abstract class Entity(
    string? name,
    Dictionary<Entity, RelationContext> relations)
{
    /// <summary>
    /// The name of the entity
    /// </summary>
    public string? Name { get; } = name;

    /// TODO: If the entity can't interact with others, then it cannot have relations, only characteristics
    /// <summary>
    /// The relations of this Entity with others
    /// </summary>
    public Dictionary<Entity, RelationContext> Relations { get; } = relations;

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
    protected abstract ICharacteristicComparer DefaultComparer { get; }

    /// <summary>
    /// Gets the <see cref="RelationContext"/> of this entity with another one
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> to get the <see cref="RelationContext"/></param>
    /// <returns>The <see cref="RelationContext"/> related to the entity </returns>
    public RelationContext GetRelationContext(Entity entity)
    {
        // TODO: When we create the IEntityComparer, use it here.
        // Currently, we are using the reference to compare entities.
        return Relations.TryGetValue(entity, out var context)
            ? context
            : new RelationContext(RelationKind.Blank, DefaultComparer);
    }
}
