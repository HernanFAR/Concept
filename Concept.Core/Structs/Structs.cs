using Concept.Core.Interfaces;

namespace Concept.Core.Structs;

public readonly record struct RelationContext(RelationKind Kind, ICharacteristicComparer CharComparer);
