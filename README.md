# Concept

Concept is an experimental toolkit for modeling **persistent, causally evolving actors and other related nodes** without baking a particular social, game, narrative, or AI ontology into the core.

The project began by pressure-testing generic primitives for nodes and characteristics. Its next stage is intentionally more **consumer-first**: concrete consumers such as Project Anima, social simulations, and interactive-story experiments should expose real pressure first; Concept should extract only the abstractions that survive that pressure across consumers.

Concept is therefore not a finished framework and not a predefined psychology model. It is a place where reusable causal abstractions are **discovered, challenged, and promoted deliberately**.

## Two exploration surfaces

This repository is meant to work as a handoff, not only as source code.

Choose the surface that matches how you are entering the project:

- **For AI actors, reviewers, mentors, and research curators:** [`docs/exploration-for-ai.md`](docs/exploration-for-ai.md)
  - compact project state;
  - what is implemented versus hypothesized;
  - conceptual vocabulary;
  - consumer-first extraction rules;
  - recommended reading order;
  - questions that remain open.

- **Para Hernán / exploración humana:** [`docs/exploration-for-human.md`](docs/exploration-for-human.md)
  - la idea de Concept en lenguaje más directo;
  - qué queremos lograr con actores, características e historias emergentes;
  - cómo se conectan Anima, VSlices y Research;
  - qué cosas todavía son hipótesis y cuáles ya sobrevivieron pressure tests.

The two surfaces point to the same project. They differ only in navigation and explanation style.

## The core idea

A consumer defines concrete nodes and the state that matters to its domain. Concept provides small reusable structures that let those nodes have characteristics, topology, validated state snapshots, and eventually participate in perception, relation, composition, events, and causal history.

A useful mental model is:

```text
world / consumer domain
        |
        v
 concrete actors or nodes
        |
        v
 characteristics + relations + perception + history
        |
        v
 causal transformations over persistent state
        |
        v
 observable behavior / narrative / decisions
```

For actor-oriented consumers, the long-term causal direction may look approximately like:

```text
real state
  -> expression
  -> perception
  -> interpretation / domain reaction
  -> relation or internal change
  -> experience / history
  -> future behavior
```

That loop is a **consumer hypothesis**, not a mandatory `Concept.Core` pipeline.

## Why this matters for stories and actors

Concept is interested in the same broad family of problems that make simulation-heavy systems compelling: stories should be able to emerge from persistent state and causal interaction instead of being the primary source of truth.

A future interactive-story surface should therefore be a **projection of simulation state**, not a second hidden simulation. Narrative or LLM layers may explain and render causes, but should not invent contradictory causes after the fact.

Likewise, Project Anima is expected to act as an important consumer and source of pressure: if a persistent actor needs perception, memory, relation, continuity, or interpretation, those needs should first be expressed naturally in Anima. Only repeated structural patterns that also survive other consumers should be candidates for promotion into Concept.

## Current foundation

The current implementation experiment lives on `feat/core-characteristic-foundation`.

What has survived pressure testing so far:

- `Node` is an abstract extension boundary; consumers define concrete node types.
- Characteristic sets are domain-defined and may use arbitrary topology.
- Characteristic state is heterogeneous; Core does not require a universal state geometry.
- `ICharacteristicState` is intentionally minimal and consumers may define arbitrary state shapes.
- `CharacteristicDefinition<TState>` owns state-shape compatibility while generic discovery remains possible.
- Stable characteristic identity is currently set-scoped through `CharacteristicKey = (CharacteristicSetId, CharacteristicId)`.
- `CharacteristicSetState` is an immutable validated snapshot.
- Transformations are currently consumer-owned; Core deliberately does not contain a generic transformation engine.

The foundation review concluded that Core should stop expanding horizontally until concrete consumers apply the next pressure.

## Development direction: consumer first, abstraction second

The current working rule is:

> **Do not abstract from one need. Abstract from structural similarity between independently useful needs.**

A preferred discovery loop is:

```text
Consumer need
  -> concrete consumer model
  -> friction / repeated structure
  -> candidate abstraction
  -> cross-consumer pressure test
  -> promote to Concept only if it survives
```

This is especially important now that Concept is expected to interact with several experiments:

- **Project Anima** — a persistent actor is a major consumer and source of requirements.
- **Concept** — this repository owns the reusable abstractions and their evidence.
- **VSlices Development** — methodological / technical mentoring for designing slices and experiments.
- **Alive Lab Researchs / Research actor** — research curation, thesis framing, falsifiability, and evidence quality.

Concept should remain useful even if any one consumer disappears. No consumer is allowed to silently become the ontology of Core.

## Start here after the exploration surface

The main documents are:

- [`docs/conceptual-model.md`](docs/conceptual-model.md) — conceptual vocabulary and boundaries.
- [`docs/implementation-plan.md`](docs/implementation-plan.md) — phased implementation plan and current status.
- [`docs/core-foundation-review-v0.1.md`](docs/core-foundation-review-v0.1.md) — broad review after the first pressure tests.
- [`docs/phase-1-pressure-test.md`](docs/phase-1-pressure-test.md) — generic nodes, sets, topology, and state-shape pressure.
- [`docs/phase-2-state-shape-ownership.md`](docs/phase-2-state-shape-ownership.md) — who owns state-shape validity.
- [`docs/phase-2-characteristic-identity.md`](docs/phase-2-characteristic-identity.md) — stable characteristic identity.
- [`docs/phase-2-transformation-semantics.md`](docs/phase-2-transformation-semantics.md) — immutable snapshots and consumer-owned transformation semantics.
- [`docs/development-boundaries.md`](docs/development-boundaries.md) — boundaries that should not be crossed casually.
- [`docs/evolution.md`](docs/evolution.md) — later interactive-story direction.
- [`docs/legacy-notes.md`](docs/legacy-notes.md) — useful ideas preserved from the previous implementation without preserving its architecture.

## Status

Concept is intentionally experimental. Some ideas are established only as **current preferred models**, while perception, relation, plasticity, tolerance, composition, memory/history semantics, story generation, and actor continuity remain open to consumer-driven experimentation.

When in doubt, prefer preserving pressure and evidence over prematurely making the API convenient.