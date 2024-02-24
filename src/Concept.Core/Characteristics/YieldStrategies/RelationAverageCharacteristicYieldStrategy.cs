using Concept.Core.Characteristics.Comparers;
using Concept.Core.Extensions;
using Concept.Core.Nodes;
using System.Collections.Generic;
using System.Linq;

namespace Concept.Core.Characteristics.YieldStrategies;

/// <summary>
/// A characteristic yield strategy that yields the average of the relations of the entity
/// </summary>
/// <param name="yieldFrom">
/// An entity to yield the characteristics from
/// </param>
public class RelationAverageCharacteristicYieldStrategy(Node yieldFrom)
    : CharacteristicYieldStrategy(yieldFrom)
{
    /// <inheritdoc />
    public override IEnumerator<Characteristic> GetEnumerator()
    {
        IEnumerable<Characteristic> allCharacteristics = YieldFrom.Relations
            .Select(e => e.Key)
            .SelectMany(e => e.Characteristics);

        Dictionary<Characteristic, IEnumerable<Characteristic>> dictionaryContext = [];

        foreach (Characteristic @char in allCharacteristics)
        {
            dictionaryContext[@char] = Enumerable.Empty<Characteristic>();

            foreach (Characteristic key in dictionaryContext.Keys)
            {
                if (Context?.PointOfView is null)
                {
                    dictionaryContext[@char] = dictionaryContext[@char].Append(key);
                    continue;
                }

                CharacteristicComparer comparer = Context.PointOfView.GetRelationContext(YieldFrom)?.CharComparer
                                                  ?? Context.PointOfView.DefaultComparer;

                // If the Context.PointOfView's comparer related to YieldFrom DOES think that this
                // two characteristics are equal, then continue to the next key
                if (comparer.Equals(key, @char))
                {
                    dictionaryContext[@char] = dictionaryContext[@char].Append(key);
                }
            }
        }

        foreach ((Characteristic? @char, IEnumerable<Characteristic>? values) in dictionaryContext)
        {
            var average = (byte)values.Average(c => c.Value);

            yield return @char.WithValue(average);
        }
    }
}
