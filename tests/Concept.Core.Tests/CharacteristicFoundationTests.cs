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
        var definitions = new ICharacteristicDefinition[]
        {
            new CharacteristicDefinition<BoundedCharacteristicState<int>>(Warmth, "Warmth"),
            new CharacteristicDefinition<BoundedCharacteristicState<int>>(Brightness, "Brightness")
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
        var (state, warmthDefinition, brightnessDefinition) = CreateColorState();
        var node = new TestNode(new NodeId("sample"), [state]);

        Assert.True(node.TryGetCharacteristicSet(ColorSetId, out var retrieved));
        Assert.NotNull(retrieved);
        Assert.True(retrieved.TryGet(warmthDefinition, out var warmth));
        Assert.True(retrieved.TryGet(brightnessDefinition, out var brightness));
        Assert.Equal(40, warmth.Value);
        Assert.Equal(70, brightness.Value);
    }

    [Fact]
    public void Core_can_represent_a_non_circular_directed_characteristic_topology()
    {
        var definition = CreateProcessDefinition();

        Assert.Equal(4, definition.Definition.Characteristics.Count);
        Assert.Equal(3, definition.Definition.Links.Count);

        Assert.Contains(definition.Definition.Links, link =>
            link.Source == Intake &&
            link.Target == Conversion &&
            link.IsDirected &&
            link.Kind == new CharacteristicLinkKind("flow"));

        Assert.Contains(definition.Definition.Links, link =>
            link.Source == Conversion &&
            link.Target == Waste &&
            link.IsDirected &&
            link.Kind == new CharacteristicLinkKind("loss"));
    }

    [Fact]
    public void One_set_can_contain_plain_bounded_and_consumer_specific_state_shapes()
    {
        var process = CreateProcessState();

        Assert.True(process.State.TryGet(process.Intake, out var intake));
        Assert.Equal(12.5m, intake.Value);

        Assert.True(process.State.TryGet(process.Conversion, out var conversion));
        Assert.Equal(8.75m, conversion.Value);
        Assert.Equal(20m, conversion.Maximum);

        Assert.True(process.State.TryGet(process.Output, out var output));
        Assert.Equal(6.25m, output.Yield);
        Assert.Equal(0.92m, output.Quality);

        Assert.True(process.State.TryGet(process.Waste, out var waste));
        Assert.Equal(2.5m, waste.Value);
    }

    [Fact]
    public void One_node_can_expose_multiple_sets_without_knowing_their_state_shapes()
    {
        var color = CreateColorState();
        var process = CreateProcessState();
        var node = new TestNode(new NodeId("multi-set"), [color.State, process.State]);

        Assert.Equal(2, node.CharacteristicSets.Count);
        Assert.True(node.TryGetCharacteristicSet(ColorSetId, out var retrievedColor));
        Assert.True(node.TryGetCharacteristicSet(ProcessSetId, out var retrievedProcess));
        Assert.NotNull(retrievedColor);
        Assert.NotNull(retrievedProcess);

        Assert.True(retrievedColor.TryGet(color.Warmth, out var warmth));
        Assert.True(retrievedProcess.TryGet(process.Output, out var output));
        Assert.Equal(40, warmth.Value);
        Assert.Equal(0.92m, output.Quality);
    }

    [Fact]
    public void Definition_rejects_an_invalid_state_shape_before_the_node_can_observe_it()
    {
        var process = CreateProcessDefinition();
        var invalid = new Dictionary<CharacteristicId, ICharacteristicState>
        {
            [Intake] = new ValueCharacteristicState<decimal>(12.5m),
            [Conversion] = new BoundedCharacteristicState<decimal>(0m, 8.75m, 20m),
            [Output] = new ValueCharacteristicState<decimal>(6.25m),
            [Waste] = new ValueCharacteristicState<decimal>(2.5m)
        };

        var exception = Assert.Throws<ArgumentException>(() => new CharacteristicSetState(process.Definition, invalid));

        Assert.Contains("output", exception.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Contains(nameof(ProcessOutputState), exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void Typed_definition_retrieval_does_not_require_a_cast_or_type_argument_at_the_call_site()
    {
        var process = CreateProcessState();

        var found = process.State.TryGet(process.Output, out var output);

        Assert.True(found);
        Assert.Equal(0.92m, output.Quality);
    }

    [Fact]
    public void Definition_from_another_set_is_not_a_valid_typed_handle_even_when_the_id_matches()
    {
        var process = CreateProcessState();
        var foreignOutput = new CharacteristicDefinition<ProcessOutputState>(Output, "Foreign output");

        Assert.False(process.State.TryGet(foreignOutput, out _));
    }

    [Fact]
    public void Untyped_retrieval_remains_available_for_generic_discovery()
    {
        var process = CreateProcessState();

        Assert.True(process.State.TryGet<ProcessOutputState>(Output, out var output));
        Assert.Equal(6.25m, output.Yield);
    }

    [Fact]
    public void Set_state_requires_exactly_one_state_for_every_characteristic()
    {
        var process = CreateProcessDefinition();
        var missing = new Dictionary<CharacteristicId, ICharacteristicState>
        {
            [Intake] = new ValueCharacteristicState<decimal>(10m),
            [Conversion] = new BoundedCharacteristicState<decimal>(0m, 10m, 20m),
            [Output] = new ProcessOutputState(8m, 0.8m)
        };

        Assert.Throws<ArgumentException>(() => new CharacteristicSetState(process.Definition, missing));
    }

    private static ColorFixture CreateColorState()
    {
        var warmth = new CharacteristicDefinition<BoundedCharacteristicState<int>>(Warmth, "Warmth");
        var brightness = new CharacteristicDefinition<BoundedCharacteristicState<int>>(Brightness, "Brightness");
        var definition = new CharacteristicSetDefinition(
            ColorSetId,
            "Color",
            [warmth, brightness],
            [new CharacteristicLink(Warmth, Brightness, new CharacteristicLinkKind("association"))]);

        var state = new CharacteristicSetState(definition, new Dictionary<CharacteristicId, ICharacteristicState>
        {
            [Warmth] = new BoundedCharacteristicState<int>(0, 40, 100),
            [Brightness] = new BoundedCharacteristicState<int>(0, 70, 100)
        });

        return new ColorFixture(state, warmth, brightness);
    }

    private static ProcessFixture CreateProcessState()
    {
        var definition = CreateProcessDefinition();
        var state = new CharacteristicSetState(
            definition.Definition,
            new Dictionary<CharacteristicId, ICharacteristicState>
            {
                [Intake] = new ValueCharacteristicState<decimal>(12.5m),
                [Conversion] = new BoundedCharacteristicState<decimal>(0m, 8.75m, 20m),
                [Output] = new ProcessOutputState(6.25m, 0.92m),
                [Waste] = new ValueCharacteristicState<decimal>(2.5m)
            });

        return new ProcessFixture(
            state,
            definition.Intake,
            definition.Conversion,
            definition.Output,
            definition.Waste);
    }

    private static ProcessDefinitionFixture CreateProcessDefinition()
    {
        var intake = new CharacteristicDefinition<ValueCharacteristicState<decimal>>(Intake, "Intake");
        var conversion = new CharacteristicDefinition<BoundedCharacteristicState<decimal>>(Conversion, "Conversion");
        var output = new CharacteristicDefinition<ProcessOutputState>(Output, "Output");
        var waste = new CharacteristicDefinition<ValueCharacteristicState<decimal>>(Waste, "Waste");

        var definition = new CharacteristicSetDefinition(
            ProcessSetId,
            "Process",
            [intake, conversion, output, waste],
            [
                new CharacteristicLink(Intake, Conversion, new CharacteristicLinkKind("flow"), IsDirected: true),
                new CharacteristicLink(Conversion, Output, new CharacteristicLinkKind("flow"), IsDirected: true),
                new CharacteristicLink(Conversion, Waste, new CharacteristicLinkKind("loss"), IsDirected: true)
            ]);

        return new ProcessDefinitionFixture(definition, intake, conversion, output, waste);
    }

    private readonly record struct ProcessOutputState(decimal Yield, decimal Quality) : ICharacteristicState;

    private sealed record ProcessDefinitionFixture(
        CharacteristicSetDefinition Definition,
        CharacteristicDefinition<ValueCharacteristicState<decimal>> Intake,
        CharacteristicDefinition<BoundedCharacteristicState<decimal>> Conversion,
        CharacteristicDefinition<ProcessOutputState> Output,
        CharacteristicDefinition<ValueCharacteristicState<decimal>> Waste);

    private sealed record ProcessFixture(
        CharacteristicSetState State,
        CharacteristicDefinition<ValueCharacteristicState<decimal>> Intake,
        CharacteristicDefinition<BoundedCharacteristicState<decimal>> Conversion,
        CharacteristicDefinition<ProcessOutputState> Output,
        CharacteristicDefinition<ValueCharacteristicState<decimal>> Waste);

    private sealed record ColorFixture(
        CharacteristicSetState State,
        CharacteristicDefinition<BoundedCharacteristicState<int>> Warmth,
        CharacteristicDefinition<BoundedCharacteristicState<int>> Brightness);

    private sealed class TestNode(NodeId id, IEnumerable<CharacteristicSetState>? characteristicSets = null)
        : Node(id, characteristicSets);
}
