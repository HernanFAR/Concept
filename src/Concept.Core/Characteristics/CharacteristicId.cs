namespace Concept.Core.Characteristics;

public readonly record struct CharacteristicId
{
    public CharacteristicId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Characteristic id cannot be empty.", nameof(value));

        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}
