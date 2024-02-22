using Concept.Abstracts;
using Concept.Structs;

namespace Concept;

public abstract class Individual(string? name, params EntityAndRelation[] relationsWithOthers) : Entity(name, relationsWithOthers);
