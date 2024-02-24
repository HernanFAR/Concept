using Concept.Core.Characteristics;
using Concept.Core.Nodes;

namespace Concept.Core;

/// <summary>
/// A specific context of use, that might alter an interaction between <see cref="Node"/> or
/// <see cref="Characteristic"/> instances
/// </summary>
/// <param name="pointOfView">An optional <see cref="Node"/> that is analyzing comparision</param>
public class ExecutionContext(Node? pointOfView)
{
    /// <summary>
    /// An optional <see cref="Node"/> that is analyzing the comparision
    /// </summary>
    public Node? PointOfView { get; } = pointOfView;

    /// <summary>
    /// Deconstructs the instance
    /// </summary>
    /// <param name="pointOfView">
    /// The point of view of this comparison
    /// </param>
    public void Deconstruct(out Node? pointOfView)
    {
        pointOfView = PointOfView;
    }
}
