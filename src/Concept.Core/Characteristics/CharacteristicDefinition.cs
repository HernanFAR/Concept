namespace Concept.Core.Characteristics;

public sealed record CharacteristicDefinition(CharacteristicId Id, string Name)
{
    public string Name { get; } = string.IsNullOrWhiteSpace(Name)
        ? throw new ArgumentException("Characteristic name cannot be empty.", nameof(Name))
        : Name;
}
