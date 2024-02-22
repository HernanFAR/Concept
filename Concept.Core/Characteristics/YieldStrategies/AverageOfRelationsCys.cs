using Concept.Core.Abstracts;
using Concept.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Concept.Core.Characteristics.YieldStrategies;

public class AverageOfRelationsCys(Entity yieldFrom) : CharacteristicYieldStrategy(yieldFrom)
{
    public override IEnumerator<Characteristic> GetEnumerator()
    {
        IEnumerable<Characteristic> allCharacteristics = YieldFrom.Relations
            .Select(e => e.Key)
            .SelectMany(e => e.Characteristics);

        Dictionary<Characteristic, IEnumerable<Characteristic>> dictionaryContext = [];

        foreach (var @char in allCharacteristics)
        {
            dictionaryContext[@char] = Enumerable.Empty<Characteristic>();

            foreach (var key in dictionaryContext.Keys)
            {
                if (Context?.PointOfView is null)
                {
                    dictionaryContext[@char] = dictionaryContext[@char].Append(key);
                    continue;
                }

                // If the Context.PointOfView's comparer related to YieldFrom DOES think that this
                // two characteristics are equal, then continue to the next key
                if (Context.PointOfView.GetRelationContext(YieldFrom).CharComparer.Equals(key, @char) )
                {
                    dictionaryContext[@char] = dictionaryContext[@char].Append(key);
                }
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
