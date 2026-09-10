namespace Concept.Core.Characteristics;

public readonly record struct CharacteristicSetId
{
    public CharacteristicSetId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Characteristic set id cannot be empty.", nameof(value));

        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}
