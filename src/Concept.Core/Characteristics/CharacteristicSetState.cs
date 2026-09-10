namespace Concept.Core.Characteristics;

public sealed class CharacteristicSetState<TScalar> : ICharacteristicSetState
    where TScalar : IComparable<TScalar>
{
    private readonly IReadOnlyDictionary<CharacteristicId, CharacteristicState<TScalar>> _states;

    public CharacteristicSetState(
        CharacteristicSetDefinition definition,
        IReadOnlyDictionary<CharacteristicId, CharacteristicState<TScalar>> states)
    {
        Definition = definition ?? throw new ArgumentNullException(nameof(definition));
        ArgumentNullException.ThrowIfNull(states);

        var expected = definition.Characteristics.Select(x => x.Id).ToHashSet();
        var actual = states.Keys.ToHashSet();

        if (!expected.SetEquals(actual))
            throw new ArgumentException("State must define exactly one value for every characteristic in the set.", nameof(states));

        _states = new Dictionary<CharacteristicId, CharacteristicState<TScalar>>(states);
    }

    public CharacteristicSetDefinition Definition { get; }
    public Type ScalarType => typeof(TScalar);
    public IReadOnlyDictionary<CharacteristicId, CharacteristicState<TScalar>> States => _states;

    public CharacteristicState<TScalar> this[CharacteristicId id] => _states[id];
}
