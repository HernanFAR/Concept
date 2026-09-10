# Concept

Concept explores a compact model for systems of related nodes and the reusable primitives needed to build domain-specific simulations on top of them.

The repository is currently moving from the frozen conceptual model into the first implementation pressure tests.

## Status

**Conceptual model v0.1 — implementation experiments in progress.**

- Conceptual model: [`docs/conceptual-model.md`](docs/conceptual-model.md)
- Implementation plan: [`docs/implementation-plan.md`](docs/implementation-plan.md)
- Development boundaries: [`docs/development-boundaries.md`](docs/development-boundaries.md)
- Phase 1 pressure test: [`docs/phase-1-pressure-test.md`](docs/phase-1-pressure-test.md)
- Future evolutions: [`docs/evolution.md`](docs/evolution.md)

## Core idea

Everything that participates in the generic model can be represented as a **node**. Consumers define concrete node types and characteristic sets; Concept.Core supplies reusable structure without assuming what those domains mean.

Nodes can expose multiple characteristic sets, and those sets may use different scalar types and different topologies. Circular characteristic wheels are therefore projections for suitable sets rather than a built-in domain assumption.

## Architectural direction

`Concept.Core` is intentionally domain-agnostic. Concrete models such as a future `Concept.Social` consume Core rather than shaping Core around one social ontology.

The repository previously contained an experimental C# implementation built around nodes, characteristics and relations. That implementation was deliberately removed before this redesign. See [`docs/legacy-notes.md`](docs/legacy-notes.md).
