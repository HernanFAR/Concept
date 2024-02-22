using Concept.Core.Interfaces;
using Concept.Core.Structs;
using System;
using System.Collections.Generic;

namespace Concept.Core.Abstracts;

// An Entity might be mistaken by other, so the key is not a way to identify the entity,
// but to compare it with other entities.
// As Characteristics, we need to create a IEntityComparer, and use it to compare entities
public abstract class Entity(
    string? name,
    Dictionary<Entity, RelationContext> relations)
{
    public Guid Id { get; } = Guid.NewGuid();
    public string? Name { get; } = name;
    public Dictionary<Entity, RelationContext> Relations { get; } = relations;
    public virtual IEnumerable<Characteristic> Characteristics => CharacteristicYieldStrategy;

    protected abstract CharacteristicYieldStrategy CharacteristicYieldStrategy { get; }
    protected abstract ICharacteristicComparer DefaultComparer { get; }

    public RelationContext GetRelationContext(Entity entity)
    {
        // TODO: When we create the IEntityComparer, use it here.
        // Currently, we are using the reference to compare entities.
        return Relations.TryGetValue(entity, out var context)
            ? context
            : new RelationContext(RelationKind.Blank, DefaultComparer);
    }
}
