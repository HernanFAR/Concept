namespace Concept.Core.Characteristics;

public readonly record struct CharacteristicLinkKind
{
    public CharacteristicLinkKind(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Characteristic link kind cannot be empty.", nameof(value));

        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}
