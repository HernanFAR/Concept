# Phase 2 — mutation and transformation semantics

This pressure test asks the first behavioral question of the new Core: **what is the smallest operation required to let a consuming domain evolve characteristic state without moving domain rules into `Concept.Core`?**

## Starting constraint

The current model intentionally allows heterogeneous characteristic states. A transformation may therefore need to read and replace several different state shapes in one logical domain operation.

For example, the non-social `Process` fixture can transform:

- a bounded conversion state;
- a consumer-defined structured output state;
- a plain waste value;

as consequences of one domain operation.

## Alternatives considered

### Mutable state objects

Core could expose setters or mutate `CharacteristicSetState` in place.

This is attractive for simplicity, but makes causal history, before/after comparison, rollback, deterministic replay and concurrent inspection harder. It also lets observers see partially-applied multi-characteristic changes.

**Rejected for the current model.**

### A Core transformation/rule abstraction

Core could introduce concepts such as `ITransformation`, rule registries, propagation engines, or link-bound behavior.

The pressure test does not justify them. The meaning of converting input into output and waste is entirely consumer-owned. Encoding that behavior into Core now would confuse reusable state mechanics with domain transition policy.

**Not introduced.**

### Immutable replacement primitive + consumer-owned pure transformations

Core provides one small capability: replace the state associated with a typed characteristic and return a new validated `CharacteristicSetState` snapshot.

A consuming domain expresses behavior as ordinary functions over snapshots.

This is the current experiment.

## Current implementation

`CharacteristicSetState.With<TState>(CharacteristicDefinition<TState>, TState)`:

- validates that the typed characteristic belongs to the set by stable key and state contract;
- replaces only that characteristic;
- returns a new `CharacteristicSetState`;
- leaves the source snapshot unchanged;
- works with reconstructed typed definitions that preserve the same stable identity and state contract;
- rejects handles from another set.

No transformation interface exists in Core.

The `Process` test fixture implements domain transformations outside Core. One transformation changes a single bounded state. Another reads one source snapshot, calculates changes across bounded, plain and consumer-defined states, and returns a final snapshot by composing `With` calls.

## Findings

### Snapshot semantics fit future causality well

A transition naturally has a `before` and an `after`. That aligns with the later event/history goal without requiring history to exist yet.

### Domain behavior does not need to live on topology links

The current `flow` and `loss` link kinds remain descriptive topology. The consumer can use them later if useful, but this experiment produced meaningful behavior without teaching Core what those kinds mean.

### Typed definitions work as mutation handles too

The same typed handle used for retrieval can safely identify replacement targets. This reinforces the current split:

- `CharacteristicKey` owns stable identity;
- `CharacteristicDefinition<TState>` owns state-shape compatibility;
- consuming code owns transition semantics.

### Multi-characteristic replacement exposes useful friction

Composing several `With` calls creates intermediate snapshots and revalidates the whole set each time. This is semantically safe because only the final returned snapshot is published, but it may become noisy or inefficient for large transitions.

Do **not** add a batch mutation API yet. A later consumer should first demonstrate whether we need:

- a validated batch replacement primitive;
- a builder/transient state;
- a transaction-like transformation context;
- or simply optimized structural sharing behind the same API.

### Node evolution remains deliberately unspecified

`Node` currently owns a set of snapshots but does not provide a generic `WithCharacteristicSet` or cloning contract. Core cannot reconstruct an arbitrary consumer-defined node subtype safely without knowing its constructor or additional domain state.

That is healthy friction: node-level evolution should not be invented until a concrete consumer forces the boundary.

## Current conclusion

The smallest useful Core behavior is currently **immutable validated state replacement**, not a generic transformation engine.

For now, transformations are consumer-owned pure functions:

`CharacteristicSetState -> CharacteristicSetState`

possibly using additional domain inputs.

This keeps the causal boundary visible and gives later history/event work explicit before/after snapshots. The design should now receive a broad review before new concepts are added.
