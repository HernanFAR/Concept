namespace Concept.Core.Characteristics;

public sealed class CharacteristicSetState
{
    private readonly IReadOnlyDictionary<CharacteristicId, ICharacteristicState> _states;

    public CharacteristicSetState(
        CharacteristicSetDefinition definition,
        IReadOnlyDictionary<CharacteristicId, ICharacteristicState> states)
    {
        Definition = definition ?? throw new ArgumentNullException(nameof(definition));
        ArgumentNullException.ThrowIfNull(states);

        var expected = definition.Characteristics.Select(x => x.Id).ToHashSet();
        var actual = states.Keys.ToHashSet();

        if (!expected.SetEquals(actual))
            throw new ArgumentException("State must define exactly one value for every characteristic in the set.", nameof(states));

        _states = new Dictionary<CharacteristicId, ICharacteristicState>(states);
    }

    public CharacteristicSetDefinition Definition { get; }
    public IReadOnlyDictionary<CharacteristicId, ICharacteristicState> States => _states;

    public ICharacteristicState this[CharacteristicId id] => _states[id];

    public bool TryGet<TState>(CharacteristicId id, out TState state)
        where TState : ICharacteristicState
    {
        if (_states.TryGetValue(id, out var candidate) && candidate is TState typed)
        {
            state = typed;
            return true;
        }

        state = default!;
        return false;
    }
}
