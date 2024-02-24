using System;
using System.Collections;
using System.Collections.Generic;
using Concept.Core.Nodes;

namespace Concept.Core.Relations;

/// <summary>
/// Represents a dictionary of relations between a <see cref="Node"/> instance and other <see cref="Node"/> instances
/// </summary>
/// <param name="relatedWith">
/// The <see cref="Node"/> that is related with the others
/// </param>
public sealed class RelationDictionary(Node relatedWith) : IDictionary<Node, RelationContext>
{
    private readonly IDictionary<Node, RelationContext> _internalDictionary = new Dictionary<Node, RelationContext>();

    /// <summary>
    /// The <see cref="Node"/> that is related with the others
    /// </summary>
    public Node RelatedWith { get; } = relatedWith;

    /// <inheritdoc />
    public IEnumerator<KeyValuePair<Node, RelationContext>> GetEnumerator()
    {
        return _internalDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_internalDictionary).GetEnumerator();
    }

    /// <inheritdoc />
    public void Add(KeyValuePair<Node, RelationContext> item)
    {
        _internalDictionary.Add(item);
    }

    /// <inheritdoc />
    public void Clear()
    {
        _internalDictionary.Clear();
    }

    /// <inheritdoc />
    public bool Contains(KeyValuePair<Node, RelationContext> item)
    {
        return _internalDictionary.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(KeyValuePair<Node, RelationContext>[] array, int arrayIndex)
    {
        _internalDictionary.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    public bool Remove(KeyValuePair<Node, RelationContext> item)
    {
        return _internalDictionary.Remove(item);
    }

    /// <inheritdoc />
    public int Count => _internalDictionary.Count;

    /// <inheritdoc />
    public bool IsReadOnly => _internalDictionary.IsReadOnly;

    /// <inheritdoc />
    public void Add(Node key, RelationContext value)
    {
        _internalDictionary.Add(key, value);
    }
    
    /// <summary>
    /// Adds a relation to the dictionary
    /// </summary>
    /// <param name="value">
    /// The <see cref="RelationContext"/> to add
    /// </param>
    public void Add(RelationContext value)
    {
        _internalDictionary.Add(value.With, value);
    }

    /// <inheritdoc />
    public bool ContainsKey(Node key)
    {
        return _internalDictionary.ContainsKey(key);
    }

    /// <inheritdoc />
    public bool Remove(Node key)
    {
        return _internalDictionary.Remove(key);
    }

    /// <inheritdoc />
    public bool TryGetValue(Node key, out RelationContext value)
    {
        return _internalDictionary.TryGetValue(key, out value);
    }

    /// <inheritdoc />
    public RelationContext this[Node key]
    {
        get => _internalDictionary[key];
        set
        {
            if (key != value.With)
            {
                throw new ArgumentException("The key and the value must be the same entity");
            }

            _internalDictionary[key] = value;
        }
    }

    /// <inheritdoc />
    public ICollection<Node> Keys => _internalDictionary.Keys;

    /// <inheritdoc />
    public ICollection<RelationContext> Values => _internalDictionary.Values;

}
