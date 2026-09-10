# Phase 1 pressure test

This note records what the first concrete consumers reveal about the initial `Concept.Core` API. It is deliberately diagnostic: findings are not automatically promoted into new abstractions.

## Scenarios exercised

The current tests use two materially different characteristic sets:

1. **Color**
   - two characteristics;
   - bounded integer state;
   - one undirected association;
   - intentionally small and structurally weak.

2. **Process**
   - four characteristics;
   - directed, non-circular topology;
   - multiple link kinds (`flow`, `loss`);
   - heterogeneous state shapes inside the same set:
     - a plain decimal value;
     - a bounded decimal value;
     - a consumer-specific structured state.

A single concrete `Node` subtype is also exercised with both sets at once.

## What survived cleanly

### Node as an abstract extension boundary

Requiring consumers to derive a concrete node makes missing extensibility visible immediately. Core defines capabilities; the consumer defines what the node actually is.

### Characteristic sets as data-defined domains

Neither test requires a Core subtype for `Color` or `Process`. The same set definition mechanism represents both, which is evidence that set membership and topology can remain data-driven for now.

### Topology is not inherently circular

The process fixture forms a directed branching graph:

`Intake -> Conversion -> Output`

`                    -> Waste`

This fits the same `CharacteristicLink` primitive used by the simpler color fixture. A wheel is therefore a projection for suitable sets, not an assumption of the Core model.

### State shape is no longer universal

The initial implementation assumed every characteristic had one comparable scalar plus `Minimum`, `Value` and `Maximum`.

The pressure test showed this was too strong. Core now treats characteristic state as an extensibility boundary through `ICharacteristicState`.

Two small reusable state shapes exist only as conveniences:

- `ValueCharacteristicState<TValue>` for a single value;
- `BoundedCharacteristicState<TValue>` for an inclusive minimum/current/maximum shape.

A consumer may introduce its own state type by implementing `ICharacteristicState`. The process fixture does exactly this with a structured output state containing yield and quality.

This means a characteristic set can contain heterogeneous state shapes without changing `Node`, `CharacteristicSetDefinition`, or topology.

### State retrieval is typed but not coercive

`CharacteristicSetState.TryGet<TState>` retrieves a characteristic only when its actual state shape matches the requested type.

Core performs no conversion and does not pretend unrelated state shapes are interchangeable. Asking for a bounded state when the characteristic actually contains a consumer-specific state simply fails.

## Friction deliberately left visible

### 1. `ICharacteristicState` is intentionally almost empty

It is currently a marker contract. This is deliberate.

The experiment proves that arbitrary state can participate in the model, but does **not** yet prove that all state shares capabilities such as value, bounds, midpoint, arithmetic, interpolation, mutation, serialization, or projection.

Those capabilities should be added only when multiple consumers pressure the model in the same direction.

### 2. Characteristic ordering is not part of the contract

A set exposes a collection of characteristics and explicit links, but no semantic ordering.

That is acceptable for graph-shaped domains, but a circular/radial visualization may eventually require either:

- an ordering supplied by the consuming domain;
- enough topology to derive one;
- projection-specific layout metadata outside Core.

The visual playground should force this decision rather than Core inventing UI coordinates now.

### 3. `CharacteristicLinkKind` describes meaning but not behavior

Kinds such as `association`, `flow` and `loss` are currently identifiers. Core does not know what they do.

This is intentional for Phase 1. Before adding transitions we must decide whether behavior belongs to:

- the link kind;
- a separate transformation/rule system;
- the consuming domain;
- or some composition of these.

Encoding behavior directly into `CharacteristicLink` now would prematurely couple topology and transition policy.

### 4. Definitions do not prescribe state shape

`CharacteristicDefinition` currently identifies and names a characteristic, but does not declare which state type it expects.

That gives maximum freedom, but means an invalid state shape can currently be associated with a characteristic as long as the characteristic id exists in the set.

We should not fix this reflexively. Phase 2 must determine whether state-shape constraints belong in the definition, in a typed characteristic abstraction, in the consumer, or in a separate validation layer.

## Current conclusion

The bounded-state assumption did not survive pressure testing and has been removed from the generic set contract.

The current model now demonstrates three state categories without modifying the surrounding node/set/topology abstractions:

1. plain value;
2. bounded value;
3. consumer-specific structured state.

The next pressure point is **state-shape ownership**: whether a characteristic definition should know or constrain the shape of its state, and how strongly typed consumers can express that without making heterogeneous sets painful.
