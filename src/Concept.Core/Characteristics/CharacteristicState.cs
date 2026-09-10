namespace Concept.Core.Characteristics;

public readonly record struct CharacteristicState<TScalar>
    where TScalar : IComparable<TScalar>
{
    public CharacteristicState(TScalar minimum, TScalar value, TScalar maximum)
    {
        if (minimum.CompareTo(maximum) > 0)
            throw new ArgumentOutOfRangeException(nameof(minimum), "Minimum cannot exceed maximum.");
        if (value.CompareTo(minimum) < 0 || value.CompareTo(maximum) > 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Value must be inside the inclusive minimum/maximum bounds.");

        Minimum = minimum;
        Value = value;
        Maximum = maximum;
    }

    public TScalar Minimum { get; }
    public TScalar Value { get; }
    public TScalar Maximum { get; }

    public CharacteristicState<TScalar> WithValue(TScalar value) => new(Minimum, value, Maximum);
}
