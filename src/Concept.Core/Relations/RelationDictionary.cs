using System;
using System.Collections;
using System.Collections.Generic;
using Concept.Core.Entities;

namespace Concept.Core.Relations;

public sealed class RelationDictionary(Entity relatedWith) : IDictionary<Entity, RelationContext>
{
    private readonly IDictionary<Entity, RelationContext> _internalDictionary = new Dictionary<Entity, RelationContext>();

    public Entity RelatedWith { get; } = relatedWith;

    public IEnumerator<KeyValuePair<Entity, RelationContext>> GetEnumerator()
    {
        return _internalDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_internalDictionary).GetEnumerator();
    }

    public void Add(KeyValuePair<Entity, RelationContext> item)
    {
        _internalDictionary.Add(item);
    }

    public void Clear()
    {
        _internalDictionary.Clear();
    }

    public bool Contains(KeyValuePair<Entity, RelationContext> item)
    {
        return _internalDictionary.Contains(item);
    }

    public void CopyTo(KeyValuePair<Entity, RelationContext>[] array, int arrayIndex)
    {
        _internalDictionary.CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<Entity, RelationContext> item)
    {
        return _internalDictionary.Remove(item);
    }

    public int Count => _internalDictionary.Count;

    public bool IsReadOnly => _internalDictionary.IsReadOnly;

    public void Add(Entity key, RelationContext value)
    {
        _internalDictionary.Add(key, value);
    }

    public void Add(RelationContext value)
    {
        _internalDictionary.Add(value.With, value);
    }

    public bool ContainsKey(Entity key)
    {
        return _internalDictionary.ContainsKey(key);
    }

    public bool Remove(Entity key)
    {
        return _internalDictionary.Remove(key);
    }

    public bool TryGetValue(Entity key, out RelationContext value)
    {
        return _internalDictionary.TryGetValue(key, out value);
    }

    public RelationContext this[Entity key]
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

    public ICollection<Entity> Keys => _internalDictionary.Keys;

    public ICollection<RelationContext> Values => _internalDictionary.Values;

}
