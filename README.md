# Concept

Concept is an experimental toolkit for modeling systems of related nodes without baking a particular social, game, or simulation ontology into the core.

The current work is focused on discovering the smallest reusable primitives for:

- domain-defined node types;
- characteristic sets;
- arbitrary characteristic topology;
- heterogeneous characteristic state shapes;
- later composition, perception, relation, event, and history experiments.

Concrete social modeling is intentionally treated as a consumer of `Concept.Core`, not as the definition of the Core itself.

## Current development

The active implementation experiment is on `feat/core-characteristic-foundation`.

The first pressure test established that characteristic sets and arbitrary topology can remain generic while mandatory `Minimum / Value / Maximum` state cannot. State shape is now separated from characteristic identity.

The current Phase 2 experiment goes one step further: a characteristic definition is typed by the state shape it accepts while heterogeneous sets remain discoverable through non-generic Core contracts. This lets Core reject invalid state associations without knowing consumer-specific state types.

See:

- [`docs/conceptual-model.md`](docs/conceptual-model.md)
- [`docs/implementation-plan.md`](docs/implementation-plan.md)
- [`docs/phase-1-pressure-test.md`](docs/phase-1-pressure-test.md)
- [`docs/phase-2-state-shape-ownership.md`](docs/phase-2-state-shape-ownership.md)
- [`docs/development-boundaries.md`](docs/development-boundaries.md)
- [`docs/evolution.md`](docs/evolution.md)

The API remains intentionally provisional while these pressure tests are running.
