using Concept.Core.Characteristics;
using Concept.Core.Nodes;

namespace Concept.Core.Tests;

public sealed class CharacteristicFoundationTests
{
    private static readonly CharacteristicSetId ColorSetId = new("color");
    private static readonly CharacteristicId Warmth = new("warmth");
    private static readonly CharacteristicId Brightness = new("brightness");

    private static readonly CharacteristicSetId ProcessSetId = new("process");
    private static readonly CharacteristicId Intake = new("intake");
    private static readonly CharacteristicId Conversion = new("conversion");
    private static readonly CharacteristicId Output = new("output");
    private static readonly CharacteristicId Waste = new("waste");

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
    public void Node_subtype_can_hold_and_retrieve_a_domain_defined_characteristic_set()
    {
        var state = CreateColorState();
        var node = new TestNode(new NodeId("sample"), [state]);

        Assert.True(node.TryGetCharacteristicSet<int>(ColorSetId, out var retrieved));
        Assert.NotNull(retrieved);
        Assert.Equal(40, retrieved[Warmth].Value);
        Assert.Equal(70, retrieved[Brightness].Value);
    }

    [Fact]
    public void Core_can_represent_a_non_circular_directed_characteristic_topology()
    {
        var definition = CreateProcessDefinition();

        Assert.Equal(4, definition.Characteristics.Count);
        Assert.Equal(3, definition.Links.Count);

        Assert.Contains(definition.Links, link =>
            link.Source == Intake &&
            link.Target == Conversion &&
            link.IsDirected &&
            link.Kind == new CharacteristicLinkKind("flow"));

        Assert.Contains(definition.Links, link =>
            link.Source == Conversion &&
            link.Target == Waste &&
            link.IsDirected &&
            link.Kind == new CharacteristicLinkKind("loss"));
    }

    [Fact]
    public void One_node_can_expose_multiple_sets_with_different_scalar_types()
    {
        var color = CreateColorState();
        var process = new CharacteristicSetState<decimal>(
            CreateProcessDefinition(),
            new Dictionary<CharacteristicId, CharacteristicState<decimal>>
            {
                [Intake] = new(0m, 12.5m, 20m),
                [Conversion] = new(0m, 8.75m, 20m),
                [Output] = new(0m, 6.25m, 20m),
                [Waste] = new(0m, 2.5m, 20m)
            });

        var node = new TestNode(new NodeId("multi-set"), [color, process]);

        Assert.Equal(2, node.CharacteristicSets.Count);

        Assert.True(node.TryGetCharacteristicSet<int>(ColorSetId, out var retrievedColor));
        Assert.NotNull(retrievedColor);
        Assert.Equal(40, retrievedColor[Warmth].Value);

        Assert.True(node.TryGetCharacteristicSet<decimal>(ProcessSetId, out var retrievedProcess));
        Assert.NotNull(retrievedProcess);
        Assert.Equal(8.75m, retrievedProcess[Conversion].Value);
    }

    [Fact]
    public void Retrieving_a_set_with_the_wrong_scalar_type_fails_without_casting_or_conversion()
    {
        var node = new TestNode(new NodeId("typed"), [CreateColorState()]);

        Assert.False(node.TryGetCharacteristicSet<decimal>(ColorSetId, out var state));
        Assert.Null(state);
    }

    [Fact]
    public void Set_state_requires_exactly_one_state_for_every_characteristic()
    {
        var definition = CreateProcessDefinition();

        var missing = new Dictionary<CharacteristicId, CharacteristicState<decimal>>
        {
            [Intake] = new(0m, 10m, 20m),
            [Conversion] = new(0m, 10m, 20m),
            [Output] = new(0m, 10m, 20m)
        };

        Assert.Throws<ArgumentException>(() => new CharacteristicSetState<decimal>(definition, missing));
    }

    private static CharacteristicSetState<int> CreateColorState()
    {
        var definition = new CharacteristicSetDefinition(
            ColorSetId,
            "Color",
            [
                new CharacteristicDefinition(Warmth, "Warmth"),
                new CharacteristicDefinition(Brightness, "Brightness")
            ],
            [new CharacteristicLink(Warmth, Brightness, new CharacteristicLinkKind("association"))]);

        return new CharacteristicSetState<int>(definition, new Dictionary<CharacteristicId, CharacteristicState<int>>
        {
            [Warmth] = new(0, 40, 100),
            [Brightness] = new(0, 70, 100)
        });
    }

    private static CharacteristicSetDefinition CreateProcessDefinition() =>
        new(
            ProcessSetId,
            "Process",
            [
                new CharacteristicDefinition(Intake, "Intake"),
                new CharacteristicDefinition(Conversion, "Conversion"),
                new CharacteristicDefinition(Output, "Output"),
                new CharacteristicDefinition(Waste, "Waste")
            ],
            [
                new CharacteristicLink(Intake, Conversion, new CharacteristicLinkKind("flow"), IsDirected: true),
                new CharacteristicLink(Conversion, Output, new CharacteristicLinkKind("flow"), IsDirected: true),
                new CharacteristicLink(Conversion, Waste, new CharacteristicLinkKind("loss"), IsDirected: true)
            ]);

    private sealed class TestNode(NodeId id, IEnumerable<ICharacteristicSetState>? characteristicSets = null)
        : Node(id, characteristicSets);
}
