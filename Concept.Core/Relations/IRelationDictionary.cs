using System.Collections.Generic;
using Concept.Core.Entities;

namespace Concept.Core.Relations;

public interface IRelationDictionary : IDictionary<Entity, RelationContext>
{
}
