using Concept.Structs;
using System;
using System.Collections.Generic;

namespace Concept.Abstracts;

public abstract class Entity(
    string? name,
    IEnumerable<EntityAndRelation> relationsWithOthers)
{
    public Guid Id { get; } = Guid.NewGuid();

    public string? Name { get; } = name;
    public IEnumerable<EntityAndRelation> RelationsWithOthers { get; } = relationsWithOthers;
    public abstract IEnumerable<CharAndComparer> Characteristics { get; }

}
