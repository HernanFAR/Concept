namespace Concept.Core.Characteristics;

/// <summary>
/// Minimal state shape for a characteristic represented by a single value.
/// </summary>
public readonly record struct ValueCharacteristicState<TValue>(TValue Value) : ICharacteristicState;
