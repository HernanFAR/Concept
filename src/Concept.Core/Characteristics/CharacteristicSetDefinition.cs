namespace Concept.Core.Characteristics;

public sealed class CharacteristicSetDefinition
{
    private readonly IReadOnlyDictionary<CharacteristicId, ICharacteristicDefinition> _characteristics;
    private readonly IReadOnlyList<CharacteristicLink> _links;

    public CharacteristicSetDefinition(
        CharacteristicSetId id,
        string name,
        IEnumerable<ICharacteristicDefinition> characteristics,
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

        var foreign = definitions.Where(x => x.Key.SetId != id).Select(x => x.Key).ToArray();
        if (foreign.Length > 0)
            throw new ArgumentException(
                $"Every characteristic definition must belong to set '{id}'. Foreign keys: {string.Join(", ", foreign)}.",
                nameof(characteristics));

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
    public IReadOnlyCollection<ICharacteristicDefinition> Characteristics => _characteristics.Values;
    public IReadOnlyList<CharacteristicLink> Links => _links;

    public ICharacteristicDefinition this[CharacteristicId id] => _characteristics[id];

    public bool Contains<TState>(CharacteristicDefinition<TState> characteristic)
        where TState : ICharacteristicState =>
        characteristic.Key.SetId == Id &&
        _characteristics.TryGetValue(characteristic.Id, out var candidate) &&
        candidate.Key == characteristic.Key &&
        candidate.StateType == characteristic.StateType;
}
