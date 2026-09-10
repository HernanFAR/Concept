# Phase 2 — state-shape ownership

This experiment asks a narrow question: **who owns the invariant that a characteristic may only be paired with a valid state shape?**

The Phase 1 pressure test already showed that state shape itself cannot be universal. Plain values, bounded values and arbitrary consumer-defined states must coexist without changing `Node` or topology.

## Alternatives considered

### 1. Unconstrained definitions

A characteristic definition contains only identity and display metadata. The consuming domain validates state associations externally.

**Strengths**

- maximum freedom;
- Core remains ignorant of every state shape;
- no generic definition types.

**Pressure discovered**

- invalid state associations can enter a `CharacteristicSetState` unnoticed;
- every consumer needs to rebuild the same association-validation mechanism;
- typed retrieval still requires the caller to repeat or guess a state type.

This pushes an invariant that is structurally visible to Core into every consumer.

### 2. Generic characteristic definition

A characteristic definition carries the state type it accepts:

```csharp
CharacteristicDefinition<TState>
    where TState : ICharacteristicState
```

The set remains heterogeneous through the non-generic `ICharacteristicDefinition` contract.

**Strengths**

- state-shape validity becomes part of the characteristic contract;
- heterogeneous sets remain possible;
- a typed definition acts as a typed handle during retrieval;
- consumer-defined states remain first-class without Core changes;
- callers do not need casts or explicit type arguments when they hold the typed definition.

**Cost**

- generic definitions add some declaration noise;
- generic consumers that only discover characteristics at runtime still operate through `ICharacteristicDefinition` and `ICharacteristicState`;
- identity scope needs care: two different definition instances may carry the same `CharacteristicId` and state type.

This is the current implementation experiment.

### 3. Separate schema/validation object

Identity and state-shape validation could be split into separate objects.

**Potential advantage**

A single characteristic identity could theoretically participate in more than one schema.

**Pressure discovered before implementation**

For the current model this adds an extra association object without demonstrating a second responsibility. The characteristic already needs to communicate what state is valid wherever a concrete set is defined.

The schema remains a viable future extraction if validation grows beyond state type, but extracting it now would add indirection without evidence.

### 4. Consumer-owned validators

Core could accept validator delegates or external registries supplied by consumers.

This is flexible, but makes construction depend on external policy/configuration and weakens the characteristic as a self-describing unit. It also creates another mechanism the generic inspector would need to discover.

No current use case justifies that complexity.

## Current experiment

The branch now models a characteristic definition as a typed contract while preserving an untyped discovery surface:

- `ICharacteristicDefinition` exposes identity, name and accepted state type;
- `CharacteristicDefinition<TState>` establishes the state-shape invariant;
- `CharacteristicSetDefinition` stores heterogeneous `ICharacteristicDefinition` values;
- `CharacteristicSetState` validates that every supplied state matches its definition before the state becomes observable through a node;
- typed retrieval accepts `CharacteristicDefinition<TState>` and infers `TState` from the handle;
- id-based typed retrieval remains available for generic/runtime consumers.

The process pressure fixture intentionally mixes:

- `ValueCharacteristicState<decimal>`;
- `BoundedCharacteristicState<decimal>`;
- a consumer-defined `ProcessOutputState`.

No Core type is introduced for `ProcessOutputState`.

## Important remaining friction: definition identity

A typed definition is currently treated as a scoped handle belonging to the exact set definition that contains it. Another definition instance with the same `CharacteristicId` is not accepted as that handle.

This is deliberate for the experiment because characteristic IDs are currently scoped by their set. Accepting an equivalent-looking definition from another set would make accidental cross-set retrieval possible.

However, reference identity is not yet considered a final semantic model. Future pressure tests should decide whether the stable identity should instead be something like:

- `(CharacteristicSetId, CharacteristicId)`;
- an explicit globally unique characteristic key;
- a schema-bound characteristic identity;
- or the definition object itself.

Do not hide this issue with custom equality until composition, serialization or cross-process usage forces a choice.

## Current conclusion

The typed-definition approach currently provides the best balance of the tested options:

- the consumer controls what state types exist;
- Core owns the structural invariant that a definition and state must agree;
- sets remain heterogeneous;
- strongly typed consumers avoid casts;
- generic consumers retain untyped discovery.

This is **provisional**, not frozen. The next useful pressure should come from mutation/transformation semantics and from a consumer that needs to reconstruct definitions rather than sharing in-memory instances. Either may invalidate the current identity choice.
