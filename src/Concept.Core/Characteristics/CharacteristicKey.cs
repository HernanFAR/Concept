namespace Concept.Core.Characteristics;

/// <summary>
/// Stable identity of a characteristic within a characteristic set.
/// Local characteristic ids may be reused by different sets.
/// </summary>
public readonly record struct CharacteristicKey(
    CharacteristicSetId SetId,
    CharacteristicId CharacteristicId)
{
    public override string ToString() => $"{SetId}/{CharacteristicId}";
}
