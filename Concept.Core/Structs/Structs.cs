using Concept.Abstracts;
using Concept.Interfaces;

namespace Concept.Structs;

public readonly record struct CharAndComparer(Characteristic Char, ICharacteristicComparer Comparer);
public readonly record struct EntityAndRelation(Entity Entity, RelationKind Kind);
