# Conceptual model v0.1

Status: **frozen for the first implementation experiments**.

This document defines the vocabulary and boundaries of the model. It deliberately avoids committing to concrete classes, storage, numeric ranges or algorithms unless the semantics require them.

## 1. Node

A **Node** is the common unit of the model: something that can possess characteristics and participate in relations or composition.

Examples include an individual, a couple, a family, an organization, a city or a society.

Nodes may contain other nodes. A composite node is not merely a container: its own state may **emerge** from the nodes that compose it and from the structure between them.

This gives the model recursive scale:

`individual -> group -> community -> society`

The same primitives should remain usable at every level.

## 2. Characteristics and their topology

A **Characteristic** represents one measurable aspect of a node.

The characteristic system has three structural ideas:

1. **Characteristics** by themselves can be represented as a row of values.
2. **Tensions** place characteristics in opposition. Raising one may create pressure against another.
3. **Complements** connect characteristics that reinforce or feed one another. With tensions across the opposite side and complements adjacent, the structure becomes circular rather than a flat list.

The circle is therefore not decoration: it describes the topology through which state can move.

The exact characteristic set is domain-defined. The motivating example used concepts such as Human, Order, Knowledge, Beast, Chaos and Destruction, but Concept must not hard-code that particular vocabulary into the primitive model.

## 3. Characteristic state

For a characteristic `C`, distinguish at least:

- **Ability**: the current value of the characteristic.
- **Potential**: the maximum value that the node can currently reach in that characteristic.
- **Minimum**: the lower bound that constrains how far the characteristic can currently recede.

Invariant:

`Minimum(C) <= Ability(C) <= Potential(C)`

Potential and Minimum describe the shape available to the node; Ability describes its present position inside that shape.

Modifiers or multiplicative systems may deform not only current values but also these limits. This is important because two nodes with the same current Ability need not have the same reachable space.

## 4. Plasticity: Talent and Fragility

Current state does not explain how a node changes. Plasticity is modeled separately.

- **Talent(C)**: how readily the node increases `C`.
- **Fragility(C)**: how readily the node loses or regresses in `C`.

Talent and Fragility are themselves stats, but are expected to be substantially harder and slower to change than ordinary characteristic values.

They are independent dimensions. High Talent does not imply low Fragility, nor does low Talent imply high Fragility.

This gives meaningful profiles such as:

- high Talent + high Fragility: highly moldable;
- high Talent + low Fragility: acquires quickly and retains strongly;
- low Talent + low Fragility: changes slowly but preserves what is acquired;
- low Talent + high Fragility: difficult to build and easy to lose.

These stats can therefore encode traces of biography without storing biography as a stat. A highly moldable entity may have become so through deliberate openness, external pressure, instability or many other histories. The model records the disposition; history explains how it arose.

## 5. Tolerance

**Tolerance** is the primitive that connects characteristics to immediate interpersonal compatibility.

For node `X` and characteristic `C`, tolerance is an interval:

`Tolerance(X, C) = [lower, upper]`

with a meaningful midpoint.

The interval does not mean "liking" a characteristic. It describes the range of perceived values that X can accommodate without immediate rejection.

### 5.1 Outside the interval

A perceived value outside the interval produces immediate rejection or friction before a prior relationship is required.

### 5.2 Near the edges

Values near the boundaries are especially relevant to **evolution of the interval**. Interactions at the edge can expand or contract tolerance depending on experience.

This allows a relationship with one entity to change what kinds of entities can later be tolerated.

### 5.3 Near the midpoint

The closer the perceived value is to the midpoint, the faster the relationship can evolve.

This is **relational receptivity**, not automatic positive affinity. A compatible interaction can accelerate trust or attachment, but a harmful interaction can also accelerate a negative relationship.

Thus similarity or comfort increases sensitivity to interaction rather than forcing friendship.

## 6. Expression

A node's real characteristic value is not necessarily what it manifests in every interaction.

**Expression** describes how much of a characteristic becomes observable in a particular interaction or situation.

Therefore:

`real characteristic != expressed characteristic`

A highly chaotic node may express little Chaos in a particular situation. A highly knowledgeable node may reveal almost none of its Knowledge.

Expression prevents internal stats from behaving like universally visible metadata.

## 7. Perception

Nodes do not react to privileged access to another node's internal state. They react to what they **perceive**.

The basic causal boundary is:

`real state -> expression -> perception -> reaction`

Perception captures essentially:

- **which characteristics** X knows or believes it knows about Y;
- **how much** of each characteristic X believes Y has;
- **how much is known**: perception may be partial rather than exhaustive.

A perception may therefore be incomplete or incorrect.

Perception is not limited to characteristics of individual nodes. Relations themselves may be perceived. If X perceives that A has a strong relation with B, that observation can influence X's relation with A or B according to X's own existing relations and tolerances.

This enables association effects, reputation, mistaken alliances, rumors and social inference without requiring each to be a separate primitive.

## 8. Relation

A **Relation** is directional state from one node toward another:

`Relation(X, Y)` need not equal `Relation(Y, X)`.

The exact numeric semantics are intentionally **not frozen yet**.

What is frozen is its causal role: relation changes the impact that events concerning another node have on the observing node.

For example, the death of a node with relation near irrelevance should not affect X in the same way as the death of a node with a maximally significant relation to X.

Open question for the first experiments: whether relation is best represented as a single multiplier, multiple dimensions, or another compact structure. This must be decided empirically rather than encoded prematurely.

Relations can themselves be perceived by third parties and can therefore participate in further relationship changes.

## 9. History and events

**History is not a stat.**

History is the causal record of events that explains how the current stats came to be.

An event may change:

- current characteristic values;
- potentials or minima;
- Talent or Fragility;
- tolerance intervals;
- perceptions;
- relations;
- composite-node state.

The history of a node is useful for explanation, reconstruction and narrative, but should not become an opaque scalar standing in for those effects.

A central design goal is causal inspectability: it should be possible to ask "why did this node become like this?" and trace the relevant events and transitions.

## 10. Composite and emergent nodes

A node can be composed of other nodes.

The state of a composite node is determined by algorithms over its members and their structure. It must not be assumed to be a simple arithmetic average.

For a composite node `G` and member `A`, the model must support at least these distinct directions:

- `Relation(A, G)`: the individual's relation toward the emergent whole;
- influence of `G` on `A`;
- relations between `A` and other members of `G`;
- contribution of `A` and those internal relations to the emergent state of `G`.

This permits statements such as "I like this person but dislike their group", "I value the institution but dislike most members", or "one highly influential member changes the character of the group" without creating special-case systems.

Composite nodes also provide a natural way to model context. Instead of a separate primitive named `Context`, the relevant surrounding group or system can itself be represented as a node whose characteristics and relations influence the individual.

## 11. Information is part of the simulation

The real world-state graph and each node's perceived graph are different things.

A node does not need complete knowledge of every other node. It may know only a subset of nodes, characteristics and relations, and those beliefs may be wrong.

This is both a semantic and a scaling property. Large societies can be represented using aggregate nodes while detailed perceptions are instantiated only where interactions require them.

## 12. Causal loop

At the interpersonal scale, the intended loop is approximately:

`who I am -> what I express -> what you perceive -> how that fits your tolerance -> how our relation changes -> what future events do to us`

At the societal scale the same loop remains valid, with composite nodes participating alongside individuals.

## 13. What is deliberately not frozen

The following remain experimental:

- concrete numeric ranges;
- exact formulas for tension and complement transfer;
- exact mappings from Talent/Fragility stats to change rates;
- exact tolerance response curves;
- relation representation beyond its directional and causal role;
- algorithms for emergent characteristics in composite nodes;
- event scheduling and simulation cadence;
- any narrative or LLM layer.

These are implementation hypotheses, not conceptual truths. The first implementation should exist to test them.
