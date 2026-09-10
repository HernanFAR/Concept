namespace Concept.Core.Characteristics;

public interface ICharacteristicDefinition
{
    CharacteristicId Id { get; }
    string Name { get; }
    Type StateType { get; }

    bool Accepts(ICharacteristicState state);
}

public sealed record CharacteristicDefinition<TState> : ICharacteristicDefinition
    where TState : ICharacteristicState
{
    public CharacteristicDefinition(CharacteristicId id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Characteristic name cannot be empty.", nameof(name));

        Id = id;
        Name = name;
    }

    public CharacteristicId Id { get; }
    public string Name { get; }
    public Type StateType => typeof(TState);

    public bool Accepts(ICharacteristicState state) => state is TState;
}
