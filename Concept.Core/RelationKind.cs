using System.Collections.Generic;

namespace Concept;

public sealed class RelationKind(string name, string description, sbyte value,
    IEnumerable<RelationKind> derivesFrom)
{
    public string Name { get; } = name;
    public string Description { get; } = description;
    public sbyte Value { get; } = value;
    public IEnumerable<RelationKind> DerivesFrom { get; } = derivesFrom;
    
    public static RelationKind Blank 
        => new ("Vacío", "Existe relación, pero no hay algo que destacar", 0, []);
}
