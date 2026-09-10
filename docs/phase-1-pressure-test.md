# Phase 1 pressure test

This note records what the first concrete consumers reveal about the initial `Concept.Core` API. It is deliberately diagnostic: findings are not automatically promoted into new abstractions.

## Scenarios exercised

The current tests use two materially different characteristic sets:

1. **Color**
   - two characteristics;
   - integer scalar;
   - one undirected association;
   - intentionally small and structurally weak.

2. **Process**
   - four characteristics;
   - decimal scalar;
   - directed, non-circular topology;
   - multiple link kinds (`flow`, `loss`).

A single concrete `Node` subtype is also exercised with both sets at once, including different scalar types per set.

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

### Different sets may use different scalar types

A node can expose an integer-valued set and a decimal-valued set simultaneously. Core does not currently require a universal scalar for a node.

## Friction deliberately left visible

### 1. Bounded state is currently mandatory

`CharacteristicSetState<TScalar>` currently requires every characteristic to use `CharacteristicState<TScalar>`, and that state always has `Minimum`, `Value` and `Maximum`.

That is stronger than the generic concept has earned.

Some consumers may only need a value. Others may need bounds, uncertainty, distributions, vectors, categorical state, or domain-specific state shapes.

**Do not hide this yet.** Phase 2 exists specifically to determine whether bounded state is a reusable primitive, a specialization, or merely one consumer-level interpretation.

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

### 4. Wrong scalar retrieval is explicit friction

Asking a node for a known set using the wrong `TScalar` returns `false`. This keeps conversion out of Core, but it also exposes that callers need to know the scalar shape of the selected set.

The future generic inspector/playground will pressure-test whether `ScalarType` plus untyped discovery is sufficient or whether a richer projection contract is needed.

## Current conclusion

The first pressure test does **not** justify adding more Core abstractions yet.

The current topology and set-definition model survives two different shapes, while the strongest unresolved issue is state representation. The next implementation work should therefore challenge the mandatory bounded-state assumption before introducing mutation or social mechanics.
