# Exploration surface for AI actors

This document is the shortest reliable handoff for an AI actor entering Concept without prior conversational context.

Its job is to answer four questions quickly:

1. What is Concept trying to become?
2. What has actually survived implementation pressure so far?
3. What is still hypothesis rather than architecture?
4. How should a new actor reason about the project without accidentally freezing premature abstractions?

---

## 1. One-sentence definition

**Concept is an experimental toolkit for discovering reusable abstractions for persistent, causally evolving nodes/actors whose state, perception, relations, history, and composition can influence future behavior.**

It must remain domain-agnostic enough that social simulation, Project Anima, interactive stories, and later unrelated consumers can use it without inheriting one another's ontology.

---

## 2. Current methodological stance

Concept started Core-first, using small pressure tests to challenge generic primitives.

The project is now intentionally moving toward **consumer-first discovery**.

The rule is:

> A concrete consumer may justify an experiment, but one consumer alone does not justify a Core abstraction.

Preferred loop:

```text
consumer need
  -> concrete consumer implementation
  -> observed friction / repeated structure
  -> candidate abstraction
  -> pressure in another consumer
  -> promote, revise, or reject
```

A good abstraction must not force a consumer to speak worse about its own domain.

Do not turn Project Anima into `Concept.Core` with generic names.

---

## 3. Current project topology

There are four related experimental contexts around this work:

### Project Anima

Anima, the actor, is an important client/consumer.

Anima is expected to pressure Concept with requirements around persistent actor identity, perception, experience, memory, relation, continuity, interpretation, and action.

Those concepts should first be expressed naturally in Anima's own domain.

### Project Concept

This repository owns reusable abstractions and the evidence for why they exist.

Concept should extract structure only after consumer evidence exists.

### Suite VSlices / VSlices Development

VSlices Development acts as a methodological and technical mentor for experiment design, slices, continuity, implementation structure, and evidence-producing development.

### Alive Lab Researchs / Research actor

Research acts as a thesis/research curator.

Its expected concerns include hypothesis quality, falsifiability, confounds, interpretation of findings, evidence traceability, and whether conclusions actually follow from experiments.

### Human role

Hernán participates transversally across all four contexts, contributes hypotheses and design ideas, and serves as the human-in-the-loop communication bridge when actors exchange structured messages.

Do not assume that the four contexts share ownership of the same decisions. Their responsibilities are intentionally different.

---

## 4. What exists in Core now

The current implementation is intentionally small.

### Node

`Node` is abstract.

Core defines capabilities common to nodes; consumers define what a concrete node is.

Examples such as person, group, machine, institution, city, or society are consumer interpretations, not built-in node kinds.

### Characteristic sets

Consumers define characteristic sets.

A set groups characteristics that make sense to interpret together and may include arbitrary descriptive topology.

Core does not require a circle, axis system, bipolar model, or any fixed number of characteristics.

### Characteristic identity

Current preferred identity:

```text
CharacteristicKey = (CharacteristicSetId, CharacteristicId)
```

This lets local ids be reused across sets while allowing definitions to be reconstructed without object-reference identity.

### Characteristic state shape

There is no universal state geometry.

Current reusable shapes include:

- `ValueCharacteristicState<TValue>`
- `BoundedCharacteristicState<TValue>`

Consumers may define arbitrary state shapes by implementing `ICharacteristicState`.

`CharacteristicDefinition<TState>` declares the state shape accepted by a characteristic.

### Characteristic set state

`CharacteristicSetState` is an immutable validated snapshot.

Transformations currently remain consumer-owned and produce new snapshots using typed replacement operations.

This is deliberate: Core does not yet contain a generic transformation engine.

### Topology

`CharacteristicLink` / `CharacteristicLinkKind` are descriptive.

They do not execute behavior.

No reusable behavioral semantics have yet justified turning topology into a rules engine.

---

## 5. Findings that already killed earlier assumptions

These are important because Concept should preserve the learning, not merely the resulting code.

### Rejected: universal bounded characteristic state

An early model assumed every characteristic had `Minimum / Value / Maximum`.

Pressure testing showed that this was too strong.

Bounded state remains useful, but only as one possible state shape.

### Rejected: object-reference identity for characteristic definitions

Reference identity prevented natural reconstruction after serialization/persistence.

Set-scoped stable identity replaced it.

### Not justified: executable topology

Meaningful transformations worked without attaching behavior to links.

Keep topology descriptive until a consumer demonstrates reusable semantics.

### Not justified: generic transformation engine

Consumer-owned transformations over immutable snapshots were sufficient for the first behavioral pressure test.

Do not add an engine merely because one can be imagined.

---

## 6. Major future hypotheses

The following vocabulary is important to future Concept work, but most of it is **not frozen as Core API**.

### Ability / current state

The present value/state of a characteristic.

### Potential

A possible upper reachable bound for a characteristic in consumer models that use bounded state.

Two actors may share the same current value but differ in reachable space.

Potential is not universal Core semantics.

### Minimum

A possible lower reachable bound for a characteristic.

Like Potential, it belongs to bounded state models rather than every characteristic.

### Talent

A social/behavioral hypothesis representing how readily a characteristic increases.

It is distinct from current value.

### Fragility

A social/behavioral hypothesis representing how readily a characteristic decreases or regresses.

Talent and Fragility are intentionally independent dimensions.

### Tolerance

A social hypothesis connecting perceived characteristic values to compatibility/friction.

Candidate shape:

```text
Tolerance(X, C) = [lower, upper]
```

with a meaningful midpoint.

Possible semantics:

- outside range -> immediate friction/rejection;
- near edges -> experiences may reshape tolerance;
- near midpoint -> greater relational receptivity, not necessarily positive affinity.

### Expression

Separates internal state from what becomes observable in a context.

```text
real state != expressed state
```

### Perception

Separates world truth from what one actor believes about another.

Candidate causal boundary:

```text
real state -> expression -> perception -> reaction
```

Perception may be partial or wrong.

### Relation

Directional state from one node toward another.

```text
Relation(A, B) != Relation(B, A)
```

The representation is intentionally not frozen as a scalar or multidimensional structure.

Its important hypothesized role is causal: relation changes how events concerning another node affect the observer.

### Experience / History / Memory

Current frozen point: **history is not a stat**.

History is expected to preserve causal explanation for current state.

Project Anima may force finer distinctions such as:

```text
what happened
what an actor experienced
what the actor remembers
what the actor believes happened
what the actor later inferred
```

Do not collapse these into one abstraction before consumer pressure demonstrates the right structure.

### Composition / emergence

Nodes may compose other nodes.

A group/society/composite node may have state that emerges from members, relations, and structure.

Do not assume arithmetic averaging.

---

## 7. Emergent story direction

Interactive narrative is a future projection over the same causal world, not a second source of truth.

Desired architecture:

```text
world state
  -> available actions
  -> actor/player choice
  -> domain transformation
  -> new world state
  -> causal events / experience
  -> narrative projection
```

A narrative or LLM layer may render, summarize, or explain causes.

It should not invent hidden causes that contradict the simulation.

A useful inspiration is simulation-first emergent storytelling in systems such as Dwarf Fortress: actors exist and evolve first; stories arise from the consequences.

Do not copy Dwarf Fortress mechanics blindly. The relevant inspiration is **causal persistence and emergent narrative**.

---

## 8. Anima-specific pressure expected

Anima is especially valuable because it can expose needs that synthetic test fixtures cannot.

Expected questions include:

- What makes actor identity continuous across time?
- How are observations separated from beliefs and memories?
- How does confidence/uncertainty attach to knowledge?
- How do past experiences bias future interpretation?
- How can an actor explain why a relation or disposition changed?
- Which state belongs to world truth, which to the actor, and which to an observer of the actor?
- How should decisions consume causal state without hard-coding narrative outcomes?

Do not answer these in Core pre-emptively.

Let Anima produce concrete pressure first.

---

## 9. Research posture

Treat important design decisions as hypotheses with lineage.

A desirable long-term evidence chain is:

```text
research question
  -> consumer observation
  -> hypothesis
  -> experiment
  -> implementation
  -> observation
  -> finding
  -> cross-consumer replication
  -> abstraction / rejection
```

A future thesis-quality repository should be able to answer not only *what* an abstraction is, but *why it exists* and *which experiments support it*.

When reviewing a finding, distinguish:

- implementation result;
- domain-specific result;
- reusable structural result;
- unsupported interpretation.

Research should be able to reject Concept's conclusions.

---

## 10. What not to do

Do not:

- add social vocabulary to Core because Anima or the first social model needs it;
- model every characteristic as numeric;
- model every characteristic as bounded;
- assume every topology is circular or bipolar;
- make links executable without repeated evidence;
- introduce a generic event/memory/transformation framework merely because future use is plausible;
- let narrative text become the authoritative causal state;
- infer that a current preferred model is frozen API;
- erase useful friction before a real consumer demonstrates it is accidental rather than informative.

---

## 11. Recommended reading order

For a new AI actor, read in this order:

1. `README.md`
2. this document
3. `docs/core-foundation-review-v0.1.md`
4. `docs/conceptual-model.md`
5. `docs/implementation-plan.md`
6. `docs/phase-1-pressure-test.md`
7. `docs/phase-2-state-shape-ownership.md`
8. `docs/phase-2-characteristic-identity.md`
9. `docs/phase-2-transformation-semantics.md`
10. `docs/development-boundaries.md`
11. `docs/evolution.md`
12. relevant source/tests for the current experiment

The test suite is part of the design evidence. Read tests as API usage examples, not only as verification.

---

## 12. Current handoff state

The current Core foundation is considered coherent enough to stop expanding horizontally.

The next high-value work should come from real consumer experiments, especially Project Anima and small social/story experiments.

Any proposal to add something to Core should ideally answer:

1. Which consumer required it?
2. What concrete friction occurred without it?
3. Is the same structure present in another consumer?
4. What is the smallest abstraction that preserves both domains' language?
5. What observation would falsify the abstraction's claimed generality?

If those answers are missing, prefer an experiment over a new Core primitive.