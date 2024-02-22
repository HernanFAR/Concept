using Concept.Core.Abstracts;
using Concept.Core.Interfaces;

namespace Concept.Core.Structs;

/// <summary>
/// An object that represents characteristics of relation between two entities
/// </summary>
/// <param name="Kind">The kind of relation</param>
/// <param name="CharComparer">The comparer to use when the <see cref="Entity"/> holder needs to compare characteristics of the related <see cref="Entity"/></param>
public readonly record struct RelationContext(RelationKind Kind, ICharacteristicComparer CharComparer);
