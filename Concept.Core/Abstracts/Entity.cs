using System;
using System.Collections.Generic;
using Concept.Core.Structs;

namespace Concept.Core.Abstracts;

public abstract class Entity(
    string? name,
    IEnumerable<EntityAndRelation> relationsWithOthers)
{
    public Guid Id { get; } = Guid.NewGuid();

    public string? Name { get; } = name;
    public IEnumerable<EntityAndRelation> RelationsWithOthers { get; } = relationsWithOthers;
    public abstract IEnumerable<CharAndComparer> Characteristics { get; }

}
