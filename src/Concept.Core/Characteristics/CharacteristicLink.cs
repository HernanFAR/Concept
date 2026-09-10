namespace Concept.Core.Characteristics;

public sealed record CharacteristicLink(
    CharacteristicId Source,
    CharacteristicId Target,
    CharacteristicLinkKind Kind,
    bool IsDirected = false)
{
    public bool Connects(CharacteristicId characteristic) =>
        Source == characteristic || Target == characteristic;
}
