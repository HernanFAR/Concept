using Concept.Abstracts;
using Concept.Structs;
using System.Collections.Generic;
using System.Linq;
using Concept.Interfaces;

namespace Concept;

public class Society(
    string? name, 
    ICharacteristicComparer characteristicComparer, 
    CharacteristicYieldStrategy characteristicYieldStrategy,
    params EntityAndRelation[] relationWithOthers)
    : Entity(name, relationWithOthers)
{
    protected CharacteristicYieldStrategy CharacteristicYieldStrategy { get; } = characteristicYieldStrategy;
    protected ICharacteristicComparer CharacteristicComparer { get; } = characteristicComparer;

    public override IEnumerable<CharAndComparer> Characteristics 
        => CharacteristicYieldStrategy.Select(characteristic => new CharAndComparer(characteristic, CharacteristicComparer));
}
