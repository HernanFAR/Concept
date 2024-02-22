using Concept.Core.Abstracts;
using Concept.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Concept.Core.Characteristics.YieldStrategies;

public class AverageOfRelationsCys(Entity fromEntity) : CharacteristicYieldStrategy(fromEntity)
{
    public override IEnumerator<Characteristic> GetEnumerator()
    {
        if (Context is null)
        {
            throw new System.InvalidOperationException("Strategy context not set");
        }

        IEnumerable<Characteristic> allCharacteristics = FromEntity.Relations
            .Select(e => e.Key)
            .SelectMany(e => e.Characteristics);

        Dictionary<Characteristic, IEnumerable<Characteristic>> dictionaryContext = [];

        foreach (var @char in allCharacteristics)
        {
            dictionaryContext[@char] = Enumerable.Empty<Characteristic>();

            foreach (var key in dictionaryContext.Keys)
            {
                // If the Context.PointOfView's comparer DOES NOT trates the same this characteristic and
                // the current in the key, then continue to the next key
                if (Context.PointOfView.GetRelationContext(FromEntity).CharComparer.Equals(key, @char) is false)
                {
                    continue;
                }

                dictionaryContext[@char] = dictionaryContext[@char].Append(key);
            }
        }

        // ReSharper disable once SuggestVarOrType_DeconstructionDeclarations
        foreach (var (@char, values) in dictionaryContext)
        {
            var average = (byte)values.Average(c => c.Value);

            yield return @char.WithValue(average);
        }
    }
}
