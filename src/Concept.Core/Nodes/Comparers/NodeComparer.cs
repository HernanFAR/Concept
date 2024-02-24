using System.Collections.Generic;

namespace Concept.Core.Nodes.Comparers;

/// <summary>
/// Represents a special way to compare two <see cref="Node"/> instances
/// </summary>
public abstract class NodeComparer : IEqualityComparer<Node>
{
    /// <summary>
    /// The context of comparision
    /// </summary>
    /// <remarks>If any, might alter the comparision</remarks>
    public ExecutionContext? Context { get; set; }

    /// <summary>
    /// Determines if two <see cref="Node"/> instances are equal, based on
    /// the current context
    /// </summary>
    /// <param name="left">Node</param>
    /// <param name="right">Node</param>
    /// <returns>True if are equals in the current context, false if not</returns>>
    public abstract bool Equals(Node right, Node left);

    /// <summary>
    /// Gets the hash code of a <see cref="Node"/> instance
    /// </summary>
    public abstract int GetHashCode(Node obj);
}
