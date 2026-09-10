# Core foundation review v0.1

This review intentionally pauses feature growth after the first topology, state-shape, identity, and transformation pressure tests.

The question is not whether the API is finished. It is whether the current foundation is small, internally coherent, and extensible enough to justify moving toward a concrete consumer.

## Current shape

The Core currently separates four concerns:

1. **Node identity and extension** — `Node` is abstract; consumers define concrete node types.
2. **Characteristic structure** — sets contain typed characteristic definitions and domain-defined topology.
3. **Characteristic state** — `ICharacteristicState` is open to consumer-defined shapes; Core provides only plain and bounded convenience shapes.
4. **State evolution** — `CharacteristicSetState` is an immutable validated snapshot; consumers own transformation semantics and use typed replacement handles.

Stable characteristic identity is currently set-scoped through `CharacteristicKey = (CharacteristicSetId, CharacteristicId)`.

## What looks strong enough to keep

### `Node` as an abstract extension boundary

This is intentionally stricter than a directly constructible Core node. Every consumer must make its concrete domain node explicit, so missing extension points become visible early.

No generic cloning or mutation contract is imposed on consumer node types.

### Characteristic sets remain heterogeneous

A set can mix Core-provided and consumer-defined state shapes. Neither `Node` nor topology needs to know those concrete shapes.

This survived both the Color and Process fixtures.

### Identity and representation are separate

`CharacteristicKey` answers "which characteristic?" while `CharacteristicDefinition<TState>` answers "which state shape is compatible?".

This allows equivalent definitions to be reconstructed without tying semantic identity to object allocation or display metadata.

### Topology remains descriptive

`CharacteristicLink` and `CharacteristicLinkKind` do not currently execute behavior. The transformation experiment showed that meaningful domain changes can remain consumer-owned without turning topology into a rule engine.

This is an important boundary to preserve until a real consumer demonstrates reusable link semantics.

### Immutable snapshots are a useful causal primitive

A transformation naturally produces `before` and `after` snapshots. This gives future events, history, replay, diagnostics, and testing something concrete to reference.

No event system is needed yet to benefit from that shape.

## Friction worth preserving

### Multi-characteristic transformations are somewhat verbose

Several `With` calls create intermediate snapshots and repeat validation. A batch primitive may eventually be justified, but adding one before a real consumer needs it would hide useful pressure.

Keep the friction for now.

### Node-level evolution is not generic

Core cannot safely manufacture a new arbitrary `Node` subtype. That is not a missing feature yet; it is a boundary telling us that node reconstruction belongs to a consumer unless repeated patterns emerge.

### Generic discovery loses compile-time detail

An inspector operating only through `ICharacteristicDefinition` / `ICharacteristicState` necessarily sees runtime shape information. Strongly typed domain code gets typed handles; generic tooling gets discovery.

That asymmetry appears appropriate rather than accidental.

### Characteristic-set namespace/versioning is unresolved

`CharacteristicSetId` is only a textual id today. If independent packages define the same id, or schemas evolve incompatibly, a stronger namespace/version story may eventually be required.

Do not solve this until persistence or a second consumer makes the collision real.

## Things we should not add yet

- a generic transformation engine;
- behavior attached directly to `CharacteristicLinkKind`;
- generic arithmetic/interpolation capabilities;
- a universal midpoint/bounds/value capability hierarchy;
- node cloning factories;
- event sourcing infrastructure;
- schema version machinery;
- visualization-specific ordering or coordinates;
- social concepts in Core.

Each of these has plausible future value, but none has survived enough independent pressure to deserve Core status.

## Naming/API review

### `CharacteristicDefinition<TState>`

The name is slightly verbose but accurately communicates that this object is definition/schema data rather than current state. Keep it for now.

### `ICharacteristicState`

This is intentionally only a marker. That nominal requirement is a small price for keeping arbitrary consumer states discoverable as characteristic state without using `object`.

Keep it minimal.

### `CharacteristicSetState.With(...)`

The method is immutable despite the mutation-like name. `With` is idiomatic for producing modified copies and keeps the API small. No rename is justified yet.

### Sealed definition/state types

The extensibility boundary is primarily **composition and consumer-provided state types**, not inheritance from every Core data object. `Node` needed inheritance because consumers define what a node is. `CharacteristicDefinition<TState>` and `CharacteristicSetDefinition` currently model validated structural data, and no use case requires subclassing them.

Keep them closed until extension pressure appears instead of opening every type pre-emptively.

## One concern to carry into the next consumer

The current Core validates the *shape* of state, but not arbitrary semantic invariants spanning characteristics.

For example, it cannot know that a process output plus waste must conserve some input, or that two social dimensions must obey a domain rule. This is expected: such invariants currently belong to consumer transformations/constructors.

The next concrete consumer should tell us whether repeated cross-characteristic validation needs a reusable hook. Do not add one solely from speculation.

## Review verdict

The foundation is coherent enough to continue.

More importantly, the pressure tests have consistently removed assumptions rather than adding machinery:

- universal bounded state was removed;
- reference identity was replaced by stable set-scoped identity;
- transformation behavior remained outside Core;
- snapshots stayed immutable;
- concrete domains remain responsible for concrete nodes and rules.

That direction matches the project's goal: Core should provide a small language for other projects rather than becoming the project that decides what those projects mean.

## Recommended next move

Stop expanding Core horizontally for a moment.

The next useful pressure should come from the first small concrete consumer — likely the social model — using this foundation to express one characteristic set and one or two meaningful transformations. Only abstractions that become awkward in that consumer should come back to Core for reconsideration.

The visual playground can follow once there is enough concrete state to inspect.
