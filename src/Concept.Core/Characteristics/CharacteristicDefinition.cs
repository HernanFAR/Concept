namespace Concept.Core.Characteristics;

public interface ICharacteristicDefinition
{
    CharacteristicKey Key { get; }
    CharacteristicId Id { get; }
    string Name { get; }
    Type StateType { get; }

    bool Accepts(ICharacteristicState state);
}

public sealed record CharacteristicDefinition<TState> : ICharacteristicDefinition
    where TState : ICharacteristicState
{
    public CharacteristicDefinition(CharacteristicKey key, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Characteristic name cannot be empty.", nameof(name));

        Key = key;
        Name = name;
    }

    public CharacteristicKey Key { get; }
    public CharacteristicId Id => Key.CharacteristicId;
    public string Name { get; }
    public Type StateType => typeof(TState);

    public bool Accepts(ICharacteristicState state) => state is TState;
}
