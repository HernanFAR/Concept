using System.Collections.Generic;

namespace Concept.Core.Entities.Comparers;

/// <summary>
/// Represents a custom way to compare two <see cref="Entity"/> instances
/// </summary>
public abstract class EntityComparer : IEqualityComparer<Entity>
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
    /// The context of comparision
    /// </summary>
    /// <remarks>If any, might alter the comparision</remarks>
    public ComparisionContext? Context { get; set; }

    public abstract bool Equals(Entity x, Entity y);

    public abstract int GetHashCode(Entity obj);
}
