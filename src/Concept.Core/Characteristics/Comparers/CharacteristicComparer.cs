using System.Collections.Generic;

namespace Concept.Core.Characteristics.Comparers;

/// <summary>
/// Represents a custom to compare two <see cref="Characteristic"/> instances
/// </summary>
public abstract class CharacteristicComparer : IEqualityComparer<Characteristic>
{
    /// <summary>
    /// The context of execution of the equality comparison
    /// </summary>
    /// <remarks>If any, might alter strategy output</remarks>
    public ExecutionContext? Context { get; set; }

    /// <summary>
    /// Determines if two <see cref="Characteristic"/> instances are equal, based on
    /// the current context
    /// </summary>
    /// <param name="left">Characteristic</param>
    /// <param name="right">Characteristic</param>
    /// <returns>True if are equals in the current context, false if not</returns>
    public abstract bool Equals(Characteristic? left, Characteristic? right);

    /// <summary>
    /// Gets the hash code of a <see cref="Characteristic"/> instance
    /// </summary>
    /// <param name="obj">Characteristic</param>
    /// <returns>
    /// Hash code of the instance
    /// </returns>
    public abstract int GetHashCode(Characteristic obj);
}
