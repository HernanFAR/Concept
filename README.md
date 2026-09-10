# Concept

Concept explores a compact model for simulating individuals, groups and societies as systems of related nodes.

The current branch intentionally contains **no implementation**. It freezes the conceptual model before code is written again.

## Status

**Conceptual model v0.1 — frozen for experimentation.**

The model is documented in [`docs/conceptual-model.md`](docs/conceptual-model.md). The intended implementation sequence and validation milestones live in [`docs/implementation-plan.md`](docs/implementation-plan.md).

## Core idea

Everything that participates in the simulation can be represented as a **node**. Nodes have characteristics, can perceive and relate to other nodes, and can themselves be composed of other nodes. Composite nodes may develop emergent characteristics from their members and internal structure.

This lets the same language describe an individual, a couple, a family, an institution, a community or a society without introducing a separate simulation model for every scale.

## Historical note

The repository previously contained an experimental C# implementation built around nodes, characteristics and relations. That implementation has deliberately been removed from this branch. A few conceptual intuitions survived, but the new model should not be constrained by the old API or architecture. See [`docs/legacy-notes.md`](docs/legacy-notes.md).
