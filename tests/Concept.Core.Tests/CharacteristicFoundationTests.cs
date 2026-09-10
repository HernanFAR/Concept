using Concept.Core.Characteristics;
using Concept.Core.Nodes;

namespace Concept.Core.Tests;

public sealed class CharacteristicFoundationTests
{
    private static readonly CharacteristicSetId ColorSetId = new("color");
    private static readonly CharacteristicId Warmth = new("warmth");
    private static readonly CharacteristicId Brightness = new("brightness");

    [Fact]
    public void State_rejects_values_outside_its_bounds()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new CharacteristicState<int>(10, 9, 20));
        Assert.Throws<ArgumentOutOfRangeException>(() => new CharacteristicState<int>(10, 21, 20));
    }

    [Fact]
    public void Set_rejects_links_to_unknown_characteristics()
    {
        var definitions = new[]
        {
            new CharacteristicDefinition(Warmth, "Warmth"),
            new CharacteristicDefinition(Brightness, "Brightness")
        };

        Assert.Throws<ArgumentException>(() => new CharacteristicSetDefinition(
            ColorSetId,
            "Color",
            definitions,
            [new CharacteristicLink(Warmth, new CharacteristicId("unknown"), new CharacteristicLinkKind("contrast"))]));
    }

    [Fact]
    public void Node_can_hold_and_retrieve_a_domain_defined_characteristic_set()
    {
        var definition = new CharacteristicSetDefinition(
            ColorSetId,
            "Color",
            [
                new CharacteristicDefinition(Warmth, "Warmth"),
                new CharacteristicDefinition(Brightness, "Brightness")
            ],
            [new CharacteristicLink(Warmth, Brightness, new CharacteristicLinkKind("association"))]);

        var state = new CharacteristicSetState<int>(definition, new Dictionary<CharacteristicId, CharacteristicState<int>>
        {
            [Warmth] = new(0, 40, 100),
            [Brightness] = new(0, 70, 100)
        });

        var node = new Node(new NodeId("sample"), [state]);

        Assert.True(node.TryGetCharacteristicSet<int>(ColorSetId, out var retrieved));
        Assert.NotNull(retrieved);
        Assert.Equal(40, retrieved[Warmth].Value);
        Assert.Equal(70, retrieved[Brightness].Value);
    }
}
