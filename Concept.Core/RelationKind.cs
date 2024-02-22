namespace Concept.Core;

/// <summary>
/// A representation of a kind of relation between two entities
/// </summary>
public sealed class RelationKind
{
    /// <summary>
    /// Instances a relation kind
    /// </summary>
    /// <param name="name">A representing name</param>
    /// <param name="description">A description</param>
    /// <param name="value">An impact value</param>
    /// <param name="derivesFrom">The others relation kinds that this relation derives from</param>
    private RelationKind(string name, string description, sbyte value,
        RelationKind[] derivesFrom)
    {
        Name = name;
        Description = description;
        Value = value;
        DerivesFrom = derivesFrom;
    }

    /// <summary>
    /// The name of the relation kind
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The description of the relation kind
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// The impact of the relation
    /// </summary>
    public sbyte Value { get; }

    /// <summary>
    /// The others relation kinds that this relation kind derives from
    /// </summary>
    public RelationKind[] DerivesFrom { get; }

    /// <summary>
    /// A <see cref="RelationKind"/> that has no impact
    /// </summary>
    public static RelationKind Blank
        => new("Vacío", "Existe relación, pero no hay algo que destacar", 0, []);
}
