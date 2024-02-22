using Concept.Core.Abstracts;
using Concept.Core.Structs;

namespace Concept.Core;

public abstract class Individual(string? name, params EntityAndRelation[] relationsWithOthers) : Entity(name, relationsWithOthers);
