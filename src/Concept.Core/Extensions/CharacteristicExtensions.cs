using Concept.Core.Characteristics;
using System;

namespace Concept.Core.Extensions;

/// <summary>
/// Extension methods for <see cref="Characteristic"/> instances
/// </summary>
public static class CharacteristicExtensions
{
    /// <summary>
    /// Creates an instance of <typeparamref name="T" /> but with the specified value.
    /// </summary>
    /// <typeparam name="T">Characteristic to recreate</typeparam>
    /// <param name="_">Characteristic to recreate</param>
    /// <param name="value">New value </param>
    /// <returns>An instance of <typeparamref name="T" /></returns>
    public static T WithValue<T>(this T _, byte value)
        where T : Characteristic
    {
        return (T)Activator.CreateInstance(typeof(T), value);
    }
}