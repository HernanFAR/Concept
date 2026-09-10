using Concept.Core.Characteristics;

namespace Concept.Core.Nodes;

public sealed class Node
{
    private readonly IReadOnlyDictionary<CharacteristicSetId, ICharacteristicSetState> _sets;

    public Node(NodeId id, IEnumerable<ICharacteristicSetState>? characteristicSets = null)
    {
        Id = id;
        var sets = (characteristicSets ?? []).ToArray();

        var duplicates = sets.GroupBy(x => x.Definition.Id).Where(x => x.Count() > 1).Select(x => x.Key).ToArray();
        if (duplicates.Length > 0)
            throw new ArgumentException($"Duplicate characteristic set ids: {string.Join(", ", duplicates)}.", nameof(characteristicSets));

        _sets = sets.ToDictionary(x => x.Definition.Id);
    }

    public NodeId Id { get; }
    public IReadOnlyCollection<ICharacteristicSetState> CharacteristicSets => _sets.Values;

    public bool TryGetCharacteristicSet<TScalar>(
        CharacteristicSetId id,
        out CharacteristicSetState<TScalar>? state)
        where TScalar : IComparable<TScalar>
    {
        if (_sets.TryGetValue(id, out var candidate) && candidate is CharacteristicSetState<TScalar> typed)
        {
            state = typed;
            return true;
        }

        state = null;
        return false;
    }
}
