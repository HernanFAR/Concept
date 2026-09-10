namespace Concept.Core.Characteristics;

public interface ICharacteristicSetState
{
    CharacteristicSetDefinition Definition { get; }
    Type ScalarType { get; }
}
