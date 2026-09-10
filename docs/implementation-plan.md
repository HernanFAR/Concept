# Implementation plan

The implementation should grow from generic causal primitives outward. Narrative generation and concrete social ontologies are intentionally deferred until the reusable abstractions can explain and survive multiple consuming models.

## Architectural rule

`Concept.Core` must remain domain-agnostic. It should provide reusable primitives for systems of related nodes without assuming individuals, societies, personality dimensions, game-specific characteristics or a fixed visualization.

A likely project structure is:

- `Concept.Core`: generic primitives;
- `Concept.Social`: provisional consumer/domain layer for the individual/society model explored here;
- `Concept.Core.Tests`: invariant and abstraction tests, using concrete fixtures only as examples;
- `Concept.Social.Tests`: tests for social hypotheses;
- `Concept.Social.Playground` (working name): interactive visual laboratory for concrete models.

The split is provisional in naming, but the dependency direction is not: domain layers consume Core; Core does not depend on them.

## Phase 0 — Conceptual model

Freeze vocabulary, invariants, architectural boundaries and known open questions.

**Exit:** `conceptual-model.md` can describe the system without relying on implementation details or a particular social ontology.

Status: **complete for v0.1**, subject to refinement when experiments reveal conceptual contradictions.

## Phase 1 — Generic nodes, characteristic sets and topology

Implement the smallest reusable model capable of representing:

- nodes;
- domain-defined characteristic sets;
- characteristics belonging to those sets;
- current values and generic bounded values where justified;
- domain-defined links/topology among characteristics;
- composition of nodes where this can be expressed without social assumptions.

Do not encode a fixed characteristic circle. Circular tension/complement arrangements must be constructs that a consuming model can define using generic topology primitives.

**Exit:** at least two materially different test characteristic sets can be represented using the same Core API, including one non-circular set.

Status: **pressure-tested on `feat/core-characteristic-foundation`**. See [`phase-1-pressure-test.md`](phase-1-pressure-test.md).

## Phase 2 — State shape and deformation

Challenge the assumption that every characteristic has the same state geometry.

The current experiment separates characteristic identity from characteristic state shape:

- `ValueCharacteristicState<TValue>` represents a plain value;
- `BoundedCharacteristicState<TValue>` represents lower/current/upper state;
- consumers may introduce arbitrary structured states through `ICharacteristicState`.

A single characteristic set can contain different state shapes without requiring changes to `Node`, set membership, or topology.

The state-shape ownership experiment currently uses a typed definition contract:

- `ICharacteristicDefinition` provides the heterogeneous discovery surface;
- `CharacteristicDefinition<TState>` declares which state shape a characteristic accepts;
- `CharacteristicSetState` rejects mismatched state associations during construction;
- a typed definition can be used as a typed retrieval handle, avoiding casts and repeated type arguments;
- id-based typed retrieval remains available for generic/runtime discovery.

This is provisional. The experiment explicitly compared unconstrained definitions, typed definitions, separate schemas, and consumer-owned validators. The typed-definition approach currently gives the best balance without making Core aware of consumer-specific state types. See [`phase-2-state-shape-ownership.md`](phase-2-state-shape-ownership.md).

Characteristic identity has now been pressure-tested separately. The current preferred model is a set-scoped key:

`CharacteristicKey = (CharacteristicSetId, CharacteristicId)`

This allows local ids to be reused by different sets, permits equivalent definitions to be reconstructed after serialization, and keeps state-shape compatibility separate from semantic identity. A set rejects definitions whose key belongs to another set, while typed lookup accepts an independently reconstructed definition when key and state type agree. See [`phase-2-characteristic-identity.md`](phase-2-characteristic-identity.md).

This identity model is still provisional until mutation, persistence, and a second consumer exercise versioning, namespaces, aliases, and schema evolution.

Do not add generic capability interfaces such as bounds, midpoint, arithmetic or interpolation until a second use case requires them.

**Exit:** plain, bounded and consumer-specific state coexist cleanly; invalid state-shape associations have a deliberate ownership model; strongly typed consumers do not require unsafe casts or Core changes for every new state type; characteristic identity semantics are explicit enough for the first mutation/serialization use cases.

Status: **state-shape ownership and set-scoped identity pressure-tested; next pressure should come from mutation/transformation semantics, then a general review before expanding further**.

## Phase 3 — Social plasticity experiment

In `Concept.Social`, prototype Talent and Fragility as slow-changing stats that affect increase and regression rates.

Do not promote them into Core merely because the first consumer needs them.

**Exit:** two social nodes exposed to the same sequence evolve differently because their plasticity differs; the experiment produces evidence for or against a more generic Core abstraction.

## Phase 4 — Social tolerance experiment

Add tolerance intervals in `Concept.Social`:

- lower and upper bounds;
- meaningful midpoint;
- rejection outside the interval;
- increased relational receptivity near the midpoint;
- edge-driven expansion/contraction of the interval.

**Exit:** unrelated nodes can exhibit immediate compatibility/friction, and repeated edge interactions can reshape future tolerance without special-case narrative code.

## Phase 5 — Expression and perception

Separate real state from expressed state and perceived state. Perception must support partial and incorrect knowledge.

The implementation should make clear which part is reusable graph/information infrastructure and which part belongs specifically to the social consumer.

**Exit:** a node can react coherently to another while holding an incomplete or mistaken model of it.

## Phase 6 — Relation experiment

Introduce the smallest directional relation representation that can correctly modulate the impact of events.

Do not pre-commit to a single scalar versus a multidimensional relation model.

**Exit:** the same external event produces materially different effects depending on the relation between observer and affected node.

## Phase 7 — Social perception

Allow nodes to perceive relations between other nodes and let those perceptions influence their own relations according to social rules.

**Exit:** association effects such as "friend of my friend" or distrust by perceived affiliation can emerge from general rules rather than dedicated feature code.

## Phase 8 — Composite and emergent nodes

Explore reusable composition primitives in Core and concrete emergence algorithms in the consuming layer.

A composite node must not be assumed to be a naive arithmetic average of members.

**Exit:** a group can develop characteristics not equal to any one member; individuals and the group can influence each other in both directions; the reusable portion of this behavior is clearly separated from social policy.

## Phase 9 — Events and causal history

Represent transitions as events and retain enough causal lineage to explain current state.

History remains a record/explanation mechanism rather than an opaque stat.

**Exit:** the system can answer a useful form of "why is this node like this?" by pointing to concrete prior transitions.

## Phase 10 — Set-driven visual playground

Build an interactive playground after enough primitives exist to inspect meaningfully. Its purpose is model exploration, debugging and pressure testing, not business logic.

The central visualization may use a wheel inspired by the motivating interface, but the wheel must be **generic and characteristic-set driven**.

The user must be able to:

- choose a node;
- choose any characteristic set exposed by that node;
- render all characteristics in that selected set;
- switch to another set without changing visualization code;
- use topology/order metadata when available;
- gracefully fall back to another layout when a circular projection is inappropriate.

For the same selected characteristic set, provide contextual views/tabs as supported by the consuming model:

- `State`: lower/current/upper values;
- `Plasticity`: Talent/Fragility;
- `Tolerance`: interval and midpoint;
- `Expression`: internal versus expressed values;
- `Perception`: viewer -> target perceived values and confidence/completeness;
- `Relation`: directional relation state where meaningful;
- `Composition`: emergent versus contributing values;
- `History`: changes and causal transitions.

These tabs are **projections over the same selected characteristic set**, not separate wheels tied to fixed concepts.

Complement the wheel with precise diagnostic views where useful:

- numeric table;
- causal timeline/event log;
- relation graph;
- side-by-side comparison of nodes and perceived-versus-real state.

**Exit:** the playground can inspect at least two different characteristic sets and multiple contextual views without domain-specific rendering branches in the generic visualization components.

## Phase 11 — Minimal social simulation

Run a deterministic consumer-level simulation with approximately:

- 10–50 individuals;
- one or a few composite groups;
- one or more domain-defined characteristic sets;
- no LLM and no generative narrative.

The first milestone should demonstrate without dedicated phenomenon-specific rules:

1. immediate rejection due to tolerance;
2. rapid relational evolution near tolerance midpoints;
3. a relationship changing later tolerance;
4. a third party changing its relation after perceiving another relation;
5. a composite node acquiring emergent characteristics.

If those effects require bespoke systems named after each phenomenon, revisit the primitives before scaling.

This phase validates `Concept.Social`; it does **not** redefine Concept.Core as a society simulator.

## Phase 12 — Cross-domain pressure test

Before promoting social abstractions into Core, build at least one non-social consumer or substantial test model using the same Core primitives.

The goal is to distinguish genuinely reusable concepts from abstractions that merely fit the social model nicely.

**Exit:** Core survives a second domain with minimal or no changes, or the mismatches reveal which abstractions need to move back into domain-specific packages.

## Phase 13 — Scale

Explore aggregate nodes as level-of-detail and information compression. Keep detailed state active where required and use composite nodes elsewhere.

**Exit:** large systems do not require every node to maintain complete knowledge or direct relations with every other node.

## Evolution — Interactive stories

Interactive stories are deliberately outside the initial implementation sequence. They become an **evolution** once the causal model, social consumer and inspector are trustworthy.

A future story UI should be another projection over the same simulated state: it consumes nodes, characteristic sets, events, expressions, perceptions, relations and domain-defined actions rather than introducing a second simulation model.

Any narrative or LLM layer added later should render and explain simulated causes rather than invent hidden causes that contradict them.

See [`evolution.md`](evolution.md).
