namespace Concept.Core.Characteristics;

/// <summary>
/// State shape for a comparable value constrained to an inclusive range.
/// </summary>
public readonly record struct BoundedCharacteristicState<TValue> : ICharacteristicState
    where TValue : IComparable<TValue>
{
    public BoundedCharacteristicState(TValue minimum, TValue value, TValue maximum)
    {
        if (minimum.CompareTo(maximum) > 0)
            throw new ArgumentOutOfRangeException(nameof(minimum), "Minimum cannot exceed maximum.");
        if (value.CompareTo(minimum) < 0 || value.CompareTo(maximum) > 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Value must be inside the inclusive minimum/maximum bounds.");

        Minimum = minimum;
        Value = value;
        Maximum = maximum;
    }

    public TValue Minimum { get; }
    public TValue Value { get; }
    public TValue Maximum { get; }

    public BoundedCharacteristicState<TValue> WithValue(TValue value) => new(Minimum, value, Maximum);
}
