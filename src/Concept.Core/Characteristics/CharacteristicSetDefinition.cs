namespace Concept.Core.Characteristics;

public sealed class CharacteristicSetDefinition
{
    private readonly IReadOnlyDictionary<CharacteristicId, CharacteristicDefinition> _characteristics;
    private readonly IReadOnlyList<CharacteristicLink> _links;

    public CharacteristicSetDefinition(
        CharacteristicSetId id,
        string name,
        IEnumerable<CharacteristicDefinition> characteristics,
        IEnumerable<CharacteristicLink>? links = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Characteristic set name cannot be empty.", nameof(name));

        Id = id;
        Name = name;

        var definitions = characteristics?.ToArray()
            ?? throw new ArgumentNullException(nameof(characteristics));

        if (definitions.Length == 0)
            throw new ArgumentException("A characteristic set must contain at least one characteristic.", nameof(characteristics));

        var duplicates = definitions.GroupBy(x => x.Id).Where(x => x.Count() > 1).Select(x => x.Key).ToArray();
        if (duplicates.Length > 0)
            throw new ArgumentException($"Duplicate characteristic ids: {string.Join(", ", duplicates)}.", nameof(characteristics));

        _characteristics = definitions.ToDictionary(x => x.Id);
        _links = (links ?? []).ToArray();

        foreach (var link in _links)
        {
            if (!_characteristics.ContainsKey(link.Source) || !_characteristics.ContainsKey(link.Target))
                throw new ArgumentException("Every characteristic link must reference characteristics from the same set.", nameof(links));
        }
    }

    public CharacteristicSetId Id { get; }
    public string Name { get; }
    public IReadOnlyCollection<CharacteristicDefinition> Characteristics => _characteristics.Values;
    public IReadOnlyList<CharacteristicLink> Links => _links;

    public CharacteristicDefinition this[CharacteristicId id] => _characteristics[id];
}
