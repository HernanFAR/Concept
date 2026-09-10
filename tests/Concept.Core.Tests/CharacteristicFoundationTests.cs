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
    public void Bounded_state_rejects_values_outside_its_bounds()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BoundedCharacteristicState<int>(10, 9, 20));
        Assert.Throws<ArgumentOutOfRangeException>(() => new BoundedCharacteristicState<int>(10, 21, 20));
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

        Assert.True(node.TryGetCharacteristicSet(ColorSetId, out var retrieved));
        Assert.NotNull(retrieved);
        Assert.True(retrieved.TryGet<BoundedCharacteristicState<int>>(Warmth, out var warmth));
        Assert.True(retrieved.TryGet<BoundedCharacteristicState<int>>(Brightness, out var brightness));
        Assert.Equal(40, warmth.Value);
        Assert.Equal(70, brightness.Value);
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
    public void One_set_can_contain_plain_bounded_and_consumer_specific_state_shapes()
    {
        var process = CreateProcessState();

        Assert.True(process.TryGet<ValueCharacteristicState<decimal>>(Intake, out var intake));
        Assert.Equal(12.5m, intake.Value);

        Assert.True(process.TryGet<BoundedCharacteristicState<decimal>>(Conversion, out var conversion));
        Assert.Equal(8.75m, conversion.Value);
        Assert.Equal(20m, conversion.Maximum);

        Assert.True(process.TryGet<ProcessOutputState>(Output, out var output));
        Assert.Equal(6.25m, output.Yield);
        Assert.Equal(0.92m, output.Quality);

        Assert.True(process.TryGet<ValueCharacteristicState<decimal>>(Waste, out var waste));
        Assert.Equal(2.5m, waste.Value);
    }

    [Fact]
    public void One_node_can_expose_multiple_sets_without_knowing_their_state_shapes()
    {
        var node = new TestNode(new NodeId("multi-set"), [CreateColorState(), CreateProcessState()]);

        Assert.Equal(2, node.CharacteristicSets.Count);
        Assert.True(node.TryGetCharacteristicSet(ColorSetId, out var color));
        Assert.True(node.TryGetCharacteristicSet(ProcessSetId, out var process));
        Assert.NotNull(color);
        Assert.NotNull(process);

        Assert.True(color.TryGet<BoundedCharacteristicState<int>>(Warmth, out var warmth));
        Assert.True(process.TryGet<ProcessOutputState>(Output, out var output));
        Assert.Equal(40, warmth.Value);
        Assert.Equal(0.92m, output.Quality);
    }

    [Fact]
    public void Retrieving_a_characteristic_with_the_wrong_state_shape_fails_without_conversion()
    {
        var process = CreateProcessState();

        Assert.False(process.TryGet<BoundedCharacteristicState<decimal>>(Output, out _));
        Assert.False(process.TryGet<ProcessOutputState>(Intake, out _));
    }

    [Fact]
    public void Set_state_requires_exactly_one_state_for_every_characteristic()
    {
        var definition = CreateProcessDefinition();
        var missing = new Dictionary<CharacteristicId, ICharacteristicState>
        {
            [Intake] = new ValueCharacteristicState<decimal>(10m),
            [Conversion] = new BoundedCharacteristicState<decimal>(0m, 10m, 20m),
            [Output] = new ProcessOutputState(8m, 0.8m)
        };

        Assert.Throws<ArgumentException>(() => new CharacteristicSetState(definition, missing));
    }

    private static CharacteristicSetState CreateColorState()
    {
        var definition = new CharacteristicSetDefinition(
            ColorSetId,
            "Color",
            [
                new CharacteristicDefinition(Warmth, "Warmth"),
                new CharacteristicDefinition(Brightness, "Brightness")
            ],
            [new CharacteristicLink(Warmth, Brightness, new CharacteristicLinkKind("association"))]);

        return new CharacteristicSetState(definition, new Dictionary<CharacteristicId, ICharacteristicState>
        {
            [Warmth] = new BoundedCharacteristicState<int>(0, 40, 100),
            [Brightness] = new BoundedCharacteristicState<int>(0, 70, 100)
        });
    }

    private static CharacteristicSetState CreateProcessState() =>
        new(
            CreateProcessDefinition(),
            new Dictionary<CharacteristicId, ICharacteristicState>
            {
                [Intake] = new ValueCharacteristicState<decimal>(12.5m),
                [Conversion] = new BoundedCharacteristicState<decimal>(0m, 8.75m, 20m),
                [Output] = new ProcessOutputState(6.25m, 0.92m),
                [Waste] = new ValueCharacteristicState<decimal>(2.5m)
            });

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

    private readonly record struct ProcessOutputState(decimal Yield, decimal Quality) : ICharacteristicState;

    private sealed class TestNode(NodeId id, IEnumerable<CharacteristicSetState>? characteristicSets = null)
        : Node(id, characteristicSets);
}
