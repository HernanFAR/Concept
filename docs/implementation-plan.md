# Implementation plan

The implementation should grow from causal primitives outward. Narrative generation is intentionally deferred until the simulation can explain its own state transitions.

## Phase 0 — Conceptual model

Freeze vocabulary, invariants and known open questions.

**Exit:** `conceptual-model.md` can describe the system without relying on implementation details.

Status: **complete for v0.1**.

## Phase 1 — Characteristic geometry

Implement a minimal node and characteristic system with:

- current Ability;
- Minimum and Potential;
- tension links;
- complement links;
- mutations constrained by the characteristic topology.

**Exit:** changing one characteristic produces inspectable and repeatable consequences through tensions/complements while respecting bounds.

## Phase 2 — Plasticity

Add Talent and Fragility as slow-changing stats that affect increase and regression rates.

**Exit:** two nodes exposed to the same sequence of events evolve differently because their plasticity differs.

## Phase 3 — Tolerance

Add tolerance intervals, midpoint receptivity, rejection outside the interval and edge-driven interval evolution.

**Exit:** two previously unrelated nodes can exhibit immediate compatibility/friction, and repeated edge interactions can reshape future tolerance.

## Phase 4 — Expression and perception

Separate real state from expressed state and perceived state. Perception must support partial and incorrect knowledge.

**Exit:** a node can react coherently to another while holding an incomplete or mistaken model of it.

## Phase 5 — Relation experiment

Introduce the smallest directional relation representation that can correctly modulate the impact of events.

Do not pre-commit to a single scalar versus a multidimensional relation model.

**Exit:** the same external event produces materially different effects depending on the relation between observer and affected node.

## Phase 6 — Social perception

Allow nodes to perceive relations between other nodes and let those perceptions influence their own relations.

**Exit:** association effects such as "friend of my friend" or distrust by perceived affiliation can emerge from general rules rather than dedicated feature code.

## Phase 7 — Composite nodes

Allow nodes to contain nodes and define experimental emergence algorithms for composite characteristics.

**Exit:** a group can develop characteristics not equal to any one member and not reducible to a naive average; individuals and the group can influence each other in both directions.

## Phase 8 — Events and causal history

Represent transitions as events and retain enough causal lineage to explain current state.

**Exit:** the simulator can answer a useful form of "why is X like this?" by pointing to concrete prior transitions.

## Phase 9 — Minimal society simulation

Run a deterministic simulation with approximately:

- 10–50 individuals;
- one or a few composite groups;
- a small domain-defined characteristic circle;
- no LLM and no generative narrative.

The first milestone should demonstrate without special-case rules:

1. immediate rejection due to tolerance;
2. rapid relational evolution near tolerance midpoints;
3. a relationship changing later tolerance;
4. a third party changing its relation after perceiving another relation;
5. a composite node acquiring emergent characteristics.

If those effects require dedicated rules for each phenomenon, revisit the primitives before scaling.

## Phase 10 — Scale

Explore aggregate nodes as social level-of-detail. Keep detailed individual state active where required and use composite nodes as compression elsewhere.

**Exit:** the simulation can grow substantially without requiring every node to maintain complete knowledge or direct relations with every other node.

## Phase 11 — Narrative layer

Only after causal behavior is trustworthy, allow a narrative system or LLM to consume events, perceptions and causal history.

The narrative layer should explain and render what happened; it should not invent hidden causes that contradict the simulation.
