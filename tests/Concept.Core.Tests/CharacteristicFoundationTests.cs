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
        var warmth = Def<BoundedCharacteristicState<int>>(ColorSetId, Warmth, "Warmth");
        var brightness = Def<BoundedCharacteristicState<int>>(ColorSetId, Brightness, "Brightness");

        Assert.Throws<ArgumentException>(() => new CharacteristicSetDefinition(
            ColorSetId,
            "Color",
            [warmth, brightness],
            [new CharacteristicLink(Warmth, new CharacteristicId("unknown"), new CharacteristicLinkKind("contrast"))]));
    }

    [Fact]
    public void Set_rejects_characteristic_definitions_owned_by_another_set()
    {
        var foreign = Def<BoundedCharacteristicState<int>>(ProcessSetId, Warmth, "Warmth");

        Assert.Throws<ArgumentException>(() => new CharacteristicSetDefinition(ColorSetId, "Color", [foreign]));
    }

    [Fact]
    public void Same_local_characteristic_id_can_exist_in_different_sets()
    {
        var sharedId = new CharacteristicId("level");
        var colorLevel = Def<ValueCharacteristicState<int>>(ColorSetId, sharedId, "Color level");
        var processLevel = Def<ValueCharacteristicState<int>>(ProcessSetId, sharedId, "Process level");

        Assert.NotEqual(colorLevel.Key, processLevel.Key);
        Assert.Equal(sharedId, colorLevel.Id);
        Assert.Equal(sharedId, processLevel.Id);
    }

    [Fact]
    public void Node_subtype_can_hold_and_retrieve_a_domain_defined_characteristic_set()
    {
        var model = CreateColorModel();
        var node = new TestNode(new NodeId("sample"), [model.State]);

        Assert.True(node.TryGetCharacteristicSet(ColorSetId, out var retrieved));
        Assert.NotNull(retrieved);
        Assert.True(retrieved.TryGet(model.Warmth, out var warmth));
        Assert.True(retrieved.TryGet(model.Brightness, out var brightness));
        Assert.Equal(40, warmth.Value);
        Assert.Equal(70, brightness.Value);
    }

    [Fact]
    public void Core_can_represent_a_non_circular_directed_characteristic_topology()
    {
        var model = CreateProcessModel();

        Assert.Equal(4, model.Definition.Characteristics.Count);
        Assert.Equal(3, model.Definition.Links.Count);
        Assert.Contains(model.Definition.Links, link =>
            link.Source == Intake && link.Target == Conversion && link.IsDirected &&
            link.Kind == new CharacteristicLinkKind("flow"));
        Assert.Contains(model.Definition.Links, link =>
            link.Source == Conversion && link.Target == Waste && link.IsDirected &&
            link.Kind == new CharacteristicLinkKind("loss"));
    }

    [Fact]
    public void One_set_can_contain_plain_bounded_and_consumer_specific_state_shapes()
    {
        var model = CreateProcessModel();

        Assert.True(model.State.TryGet(model.Intake, out var intake));
        Assert.Equal(12.5m, intake.Value);
        Assert.True(model.State.TryGet(model.Conversion, out var conversion));
        Assert.Equal(8.75m, conversion.Value);
        Assert.True(model.State.TryGet(model.Output, out var output));
        Assert.Equal(0.92m, output.Quality);
        Assert.True(model.State.TryGet(model.Waste, out var waste));
        Assert.Equal(2.5m, waste.Value);
    }

    [Fact]
    public void One_node_can_expose_multiple_sets_without_knowing_their_state_shapes()
    {
        var color = CreateColorModel();
        var process = CreateProcessModel();
        var node = new TestNode(new NodeId("multi-set"), [color.State, process.State]);

        Assert.Equal(2, node.CharacteristicSets.Count);
        Assert.True(node.TryGetCharacteristicSet(ColorSetId, out var colorState));
        Assert.True(node.TryGetCharacteristicSet(ProcessSetId, out var processState));
        Assert.NotNull(colorState);
        Assert.NotNull(processState);
        Assert.True(colorState.TryGet(color.Warmth, out var warmth));
        Assert.True(processState.TryGet(process.Output, out var output));
        Assert.Equal(40, warmth.Value);
        Assert.Equal(0.92m, output.Quality);
    }

    [Fact]
    public void Set_state_rejects_a_state_shape_that_disagrees_with_its_definition()
    {
        var model = CreateProcessModel();
        var invalid = new Dictionary<CharacteristicId, ICharacteristicState>
        {
            [Intake] = new ValueCharacteristicState<decimal>(12.5m),
            [Conversion] = new BoundedCharacteristicState<decimal>(0m, 8.75m, 20m),
            [Output] = new ValueCharacteristicState<decimal>(6.25m),
            [Waste] = new ValueCharacteristicState<decimal>(2.5m)
        };

        Assert.Throws<ArgumentException>(() => new CharacteristicSetState(model.Definition, invalid));
    }

    [Fact]
    public void Reconstructed_definition_with_same_key_and_state_type_is_a_valid_typed_handle()
    {
        var model = CreateProcessModel();
        var reconstructed = Def<ProcessOutputState>(ProcessSetId, Output, "Output reconstructed elsewhere");

        Assert.True(model.State.TryGet(reconstructed, out var output));
        Assert.Equal(0.92m, output.Quality);
    }

    [Fact]
    public void Same_local_id_from_another_set_is_not_a_valid_typed_handle()
    {
        var model = CreateProcessModel();
        var foreign = Def<ProcessOutputState>(ColorSetId, Output, "Output in another set");

        Assert.False(model.State.TryGet(foreign, out _));
    }

    [Fact]
    public void Same_key_with_wrong_state_type_is_not_a_valid_typed_handle()
    {
        var model = CreateProcessModel();
        var wrongShape = Def<ValueCharacteristicState<decimal>>(ProcessSetId, Output, "Output with wrong schema");

        Assert.False(model.State.TryGet(wrongShape, out _));
    }

    [Fact]
    public void Untyped_retrieval_remains_available_for_generic_discovery()
    {
        var model = CreateProcessModel();

        Assert.True(model.State.TryGet<ProcessOutputState>(Output, out var output));
        Assert.Equal(6.25m, output.Yield);
    }

    [Fact]
    public void Set_state_requires_exactly_one_state_for_every_characteristic()
    {
        var model = CreateProcessModel();
        var missing = new Dictionary<CharacteristicId, ICharacteristicState>
        {
            [Intake] = new ValueCharacteristicState<decimal>(10m),
            [Conversion] = new BoundedCharacteristicState<decimal>(0m, 10m, 20m),
            [Output] = new ProcessOutputState(8m, 0.8m)
        };

        Assert.Throws<ArgumentException>(() => new CharacteristicSetState(model.Definition, missing));
    }

    [Fact]
    public void Consumer_transformation_returns_a_new_snapshot_and_leaves_the_source_unchanged()
    {
        var model = CreateProcessModel();

        var next = IncreaseConversion(model, 2m);

        Assert.True(model.State.TryGet(model.Conversion, out var before));
        Assert.True(next.TryGet(model.Conversion, out var after));
        Assert.Equal(8.75m, before.Value);
        Assert.Equal(10.75m, after.Value);
        Assert.NotSame(model.State, next);
    }

    [Fact]
    public void Consumer_transformation_can_replace_multiple_heterogeneous_states_from_one_source_snapshot()
    {
        var model = CreateProcessModel();

        var next = Convert(model, 2m);

        Assert.True(next.TryGet(model.Conversion, out var conversion));
        Assert.True(next.TryGet(model.Output, out var output));
        Assert.True(next.TryGet(model.Waste, out var waste));
        Assert.Equal(10.75m, conversion.Value);
        Assert.Equal(7.75m, output.Yield);
        Assert.Equal(3.0m, waste.Value);

        Assert.True(model.State.TryGet(model.Output, out var originalOutput));
        Assert.Equal(6.25m, originalOutput.Yield);
    }

    [Fact]
    public void Replacement_accepts_a_reconstructed_definition_with_the_same_stable_identity_and_contract()
    {
        var model = CreateProcessModel();
        var reconstructed = Def<ProcessOutputState>(ProcessSetId, Output, "Output reconstructed elsewhere");

        var next = model.State.With(reconstructed, new ProcessOutputState(9m, 0.95m));

        Assert.True(next.TryGet(model.Output, out var output));
        Assert.Equal(9m, output.Yield);
    }

    [Fact]
    public void Replacement_rejects_a_characteristic_handle_from_another_set()
    {
        var model = CreateProcessModel();
        var foreign = Def<ProcessOutputState>(ColorSetId, Output, "Foreign output");

        Assert.Throws<ArgumentException>(() => model.State.With(foreign, new ProcessOutputState(9m, 0.9m)));
    }

    private static CharacteristicSetState IncreaseConversion(ProcessModel model, decimal amount)
    {
        Assert.True(model.State.TryGet(model.Conversion, out var current));
        return model.State.With(
            model.Conversion,
            new BoundedCharacteristicState<decimal>(current.Minimum, current.Value + amount, current.Maximum));
    }

    private static CharacteristicSetState Convert(ProcessModel model, decimal amount)
    {
        Assert.True(model.State.TryGet(model.Conversion, out var conversion));
        Assert.True(model.State.TryGet(model.Output, out var output));
        Assert.True(model.State.TryGet(model.Waste, out var waste));

        var nextConversion = new BoundedCharacteristicState<decimal>(
            conversion.Minimum,
            conversion.Value + amount,
            conversion.Maximum);
        var nextOutput = output with { Yield = output.Yield + amount * 0.75m };
        var nextWaste = new ValueCharacteristicState<decimal>(waste.Value + amount * 0.25m);

        return model.State
            .With(model.Conversion, nextConversion)
            .With(model.Output, nextOutput)
            .With(model.Waste, nextWaste);
    }

    private static ColorModel CreateColorModel()
    {
        var warmth = Def<BoundedCharacteristicState<int>>(ColorSetId, Warmth, "Warmth");
        var brightness = Def<BoundedCharacteristicState<int>>(ColorSetId, Brightness, "Brightness");
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

        return new(definition, state, warmth, brightness);
    }

    private static ProcessModel CreateProcessModel()
    {
        var intake = Def<ValueCharacteristicState<decimal>>(ProcessSetId, Intake, "Intake");
        var conversion = Def<BoundedCharacteristicState<decimal>>(ProcessSetId, Conversion, "Conversion");
        var output = Def<ProcessOutputState>(ProcessSetId, Output, "Output");
        var waste = Def<ValueCharacteristicState<decimal>>(ProcessSetId, Waste, "Waste");
        var definition = new CharacteristicSetDefinition(
            ProcessSetId,
            "Process",
            [intake, conversion, output, waste],
            [
                new CharacteristicLink(Intake, Conversion, new CharacteristicLinkKind("flow"), IsDirected: true),
                new CharacteristicLink(Conversion, Output, new CharacteristicLinkKind("flow"), IsDirected: true),
                new CharacteristicLink(Conversion, Waste, new CharacteristicLinkKind("loss"), IsDirected: true)
            ]);
        var state = new CharacteristicSetState(definition, new Dictionary<CharacteristicId, ICharacteristicState>
        {
            [Intake] = new ValueCharacteristicState<decimal>(12.5m),
            [Conversion] = new BoundedCharacteristicState<decimal>(0m, 8.75m, 20m),
            [Output] = new ProcessOutputState(6.25m, 0.92m),
            [Waste] = new ValueCharacteristicState<decimal>(2.5m)
        });

        return new(definition, state, intake, conversion, output, waste);
    }

    private static CharacteristicDefinition<TState> Def<TState>(CharacteristicSetId setId, CharacteristicId id, string name)
        where TState : ICharacteristicState =>
        new(new CharacteristicKey(setId, id), name);

    private readonly record struct ColorModel(
        CharacteristicSetDefinition Definition,
        CharacteristicSetState State,
        CharacteristicDefinition<BoundedCharacteristicState<int>> Warmth,
        CharacteristicDefinition<BoundedCharacteristicState<int>> Brightness);

    private readonly record struct ProcessModel(
        CharacteristicSetDefinition Definition,
        CharacteristicSetState State,
        CharacteristicDefinition<ValueCharacteristicState<decimal>> Intake,
        CharacteristicDefinition<BoundedCharacteristicState<decimal>> Conversion,
        CharacteristicDefinition<ProcessOutputState> Output,
        CharacteristicDefinition<ValueCharacteristicState<decimal>> Waste);

    private readonly record struct ProcessOutputState(decimal Yield, decimal Quality) : ICharacteristicState;

    private sealed class TestNode(NodeId id, IEnumerable<CharacteristicSetState>? characteristicSets = null)
        : Node(id, characteristicSets);
}
