using System.Collections.Generic;
using System.Linq;
using Concept.Core.Abstracts;
using Concept.Core.Extensions;
using Concept.Core.Structs;

namespace Concept.Core.Characteristics.YieldStrategies;

public class AverageOfRelationsCys(Entity entity) : CharacteristicYieldStrategy(entity)
{
    public override IEnumerator<Characteristic> GetEnumerator()
    {
        IEnumerable<CharAndComparer> allCharAndComparers = Entity.RelationsWithOthers
            .Select(e => e.Entity)
            .SelectMany(e => e.Characteristics);

        Dictionary<Characteristic, IEnumerable<Characteristic>> dictionaryContext = [];

        foreach (var (@char, comparer) in allCharAndComparers)
        {
            dictionaryContext[@char] = Enumerable.Empty<Characteristic>();
            
            foreach (var key in dictionaryContext.Keys)
            {
                // If the entity's comparer DOES NOT trates the same this characteristic and
                // the current in the key, then continue to the next key
                if (comparer.Equals(key, @char) is false)
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
