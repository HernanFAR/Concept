using Concept.Core.Entities;
using System.Collections.Generic;

namespace Concept.Core.Characteristics.Comparers;

/// <summary>
/// Represents a custom to compare two <see cref="Characteristic"/> instances
/// </summary>
public abstract class CharacteristicComparer : IEqualityComparer<Characteristic>
{
    /// <summary>
    /// Context of use, that can alter the comparision output
    /// </summary>
    /// <param name="pointOfView">An optional <see cref="Entity"/> that is analyzing comparision</param>
    public class ComparisionContext(Entity? pointOfView)
    {
        /// <summary>
        /// An optional <see cref="Entity"/> that is analyzing the comparision
        /// </summary>
        public Entity? PointOfView { get; } = pointOfView;

        public void Deconstruct(out Entity? pointOfView)
        {
            pointOfView = PointOfView;
        }
    }

    /// <summary>
    /// The context of use
    /// </summary>
    /// <remarks>If any, might alter the strategy</remarks>
    public ComparisionContext? Context { get; set; }

    public abstract bool Equals(Characteristic x, Characteristic y);

    public abstract int GetHashCode(Characteristic obj);
}
