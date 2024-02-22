using Concept.Core.Abstracts;
using Concept.Core.Interfaces;

namespace Concept.Core.Structs;

public readonly record struct CharAndComparer(Characteristic Char, ICharacteristicComparer Comparer);
public readonly record struct EntityAndRelation(Entity Entity, RelationKind Kind);
