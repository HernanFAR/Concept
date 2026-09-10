# Conceptual model v0.1

Status: **frozen for the first implementation experiments**.

This document defines the vocabulary and boundaries of the model. It deliberately avoids committing to concrete classes, storage, numeric ranges or algorithms unless the semantics require them.

## 0. Architectural boundary

Concept is a **toolkit for building systems of related nodes**. It must not encode a particular society model, personality model, game ontology, or fixed set of characteristics.

Concrete domains are consumers of the primitives, not part of the primitives themselves.

A likely architectural split is:

- **Concept.Core**: domain-agnostic primitives for nodes, characteristics, topology, composition, relations, perception and transformations.
- **Concept.Social**: a consumer/domain layer that explores individuals, groups and societies using Concept.Core.
- **Playground / test applications**: concrete models used to validate hypotheses and inspect behavior visually.

Names beyond `Concept.Core` are provisional. The conceptual boundary is not.

Concrete characteristic vocabularies used in examples or tests are illustrative only and must never become requirements of the Core.

## 1. Node

A **Node** is the common unit of the model: something that can possess characteristics and participate in relations or composition.

Examples may include an individual, a couple, a family, an organization, a city or a society, but these are domain interpretations rather than built-in node kinds.

Nodes may contain other nodes. A composite node is not merely a container: its own state may **emerge** from the nodes that compose it and from the structure between them.

This gives the model recursive scale without requiring different primitives at every scale.

## 2. Characteristics and their topology

A **Characteristic** represents one measurable aspect of a node.

Characteristics belong to **characteristic sets** defined by a consuming domain. A set groups the characteristics that should be interpreted together and may define topology among them.

A characteristic set may have any number of characteristics and any topology supported by the Core. A circular arrangement is one useful projection when the domain defines adjacent complements and opposing tensions, but Concept must not assume that every set is circular.

Within a set, domains may define structural relations such as:

1. **Independent characteristics** that simply coexist.
2. **Tensions** that place characteristics in opposition or create pressure between them.
3. **Complements** that reinforce, feed or otherwise affect one another.

The exact characteristic set is domain-defined. A motivating game example used concepts such as Human, Order, Knowledge, Beast, Chaos and Destruction, but Concept must not hard-code that vocabulary or assume that it is generally suitable for social modeling.

## 3. Characteristic state

For a characteristic `C`, distinguish at least:

- **Ability**: the current value of the characteristic.
- **Potential**: the maximum value that the node can currently reach in that characteristic.
- **Minimum**: the lower bound that constrains how far the characteristic can currently recede.

Invariant:

`Minimum(C) <= Ability(C) <= Potential(C)`

Potential and Minimum describe the shape available to the node; Ability describes its present position inside that shape.

Modifiers or multiplicative systems may deform not only current values but also these limits. Two nodes with the same current Ability therefore need not have the same reachable space.

## 4. Plasticity: Talent and Fragility

Current state does not explain how a node changes. A social or behavioral model may represent plasticity separately.

- **Talent(C)**: how readily the node increases `C`.
- **Fragility(C)**: how readily the node loses or regresses in `C`.

Talent and Fragility can themselves be stats and may be substantially harder and slower to change than ordinary characteristic values.

They are independent dimensions. High Talent does not imply low Fragility, nor does low Talent imply high Fragility.

This gives meaningful profiles such as highly moldable, quickly acquiring but stable, slowly changing but stable, or difficult to build and easy to lose.

Whether Talent and Fragility belong in Concept.Core or in a consuming model such as Concept.Social remains an architectural question to be decided by generality, not convenience.

## 5. Tolerance

A social model may define **Tolerance** as the primitive that connects perceived characteristics to immediate compatibility.

For node `X` and characteristic `C`, tolerance is an interval:

`Tolerance(X, C) = [lower, upper]`

with a meaningful midpoint.

The interval does not mean "liking" a characteristic. It describes the range of perceived values that X can accommodate without immediate rejection.

### 5.1 Outside the interval

A perceived value outside the interval produces immediate rejection or friction before a prior relationship is required.

### 5.2 Near the edges

Values near the boundaries are especially relevant to **evolution of the interval**. Interactions at the edge can expand or contract tolerance depending on experience.

### 5.3 Near the midpoint

The closer the perceived value is to the midpoint, the faster the relationship can evolve.

This is **relational receptivity**, not automatic positive affinity. A compatible interaction can accelerate trust or attachment, but a harmful interaction can also accelerate a negative relationship.

Tolerance is currently part of the social-model hypothesis, not a guaranteed Concept.Core primitive.

## 6. Expression

A node's real characteristic value is not necessarily what it manifests in every interaction.

**Expression** describes how much of a characteristic becomes observable in a particular interaction or situation.

Therefore:

`real characteristic != expressed characteristic`

Expression prevents internal stats from behaving like universally visible metadata.

Whether expression is generalized by Core or introduced by a domain layer should be driven by reuse across domains.

## 7. Perception

Nodes do not necessarily react to privileged access to another node's internal state. They may react to what they **perceive**.

The basic causal boundary is:

`real state -> expression -> perception -> reaction`

Perception captures essentially:

- **which characteristics** X knows or believes it knows about Y;
- **how much** of each characteristic X believes Y has;
- **how much is known**: perception may be partial rather than exhaustive.

A perception may therefore be incomplete or incorrect.

Perception is not limited to characteristics of individual nodes. Relations themselves may be perceived. If X perceives that A has a strong relation with B, that observation can influence X's relation with A or B according to the rules of the consuming domain.

## 8. Relation

A **Relation** is directional state from one node toward another:

`Relation(X, Y)` need not equal `Relation(Y, X)`.

The exact numeric semantics are intentionally **not frozen yet**.

What is frozen is its causal role: relation can change the impact that events concerning another node have on the observing node.

Relations can themselves be perceived by third parties and can therefore participate in further relationship changes.

## 9. History and events

**History is not a stat.**

History is the causal record of events that explains how current state came to be.

An event may change any state exposed by a domain: characteristic values, limits, plasticity, tolerance, perceptions, relations, composite-node state, or other domain-defined values.

A central design goal is causal inspectability: it should be possible to ask "why did this node become like this?" and trace relevant transitions.

## 10. Composite and emergent nodes

A node can be composed of other nodes.

The state of a composite node is determined by algorithms over its members and their structure. It must not be assumed to be a simple arithmetic average.

For a composite node `G` and member `A`, a consuming model may distinguish:

- `Relation(A, G)`;
- influence of `G` on `A`;
- relations between `A` and other members of `G`;
- contribution of `A` and those internal relations to the emergent state of `G`.

Composite nodes also provide a natural way to model context without inventing a separate universal `Context` primitive whenever the surrounding system can itself be represented as a node.

## 11. Information is part of the simulation

The real world-state graph and each node's perceived graph are different things.

A node does not need complete knowledge of every other node. It may know only a subset of nodes, characteristics and relations, and those beliefs may be wrong.

This is both a semantic and a scaling property. Large systems can use aggregate nodes while detailed perceptions are instantiated only where interactions require them.

## 12. Generic projections and visualization

Visualization is not part of the domain model, but it is an important tool for inspecting and validating it.

A **projection** should be able to select any characteristic set exposed by a node and render that set independently of what the characteristics mean.

A wheel is one possible projection for sets whose topology benefits from a circular representation. The wheel must therefore be **set-driven rather than hard-coded to one ontology**:

- choose a node;
- choose a characteristic set;
- render all characteristics belonging to that set;
- derive ordering/topological hints from the set definition when available;
- allow different views over the same set.

The same selected set may be viewed through different contextual lenses, for example:

- **State**: Minimum, Ability and Potential;
- **Plasticity**: Talent and Fragility where the consuming model provides them;
- **Tolerance**: lower bound, midpoint and upper bound;
- **Expression**: internal state versus expressed state;
- **Perception**: one node's perceived state of another, including partial knowledge;
- **Relation**: relation-relevant values where meaningful;
- **Composition**: emergent versus contributing values;
- **History**: changes over time and their causes.

These are views over data, not separate characteristic systems. A consuming project may add further views without modifying Concept.Core.

The visualization layer should be treated as a **playground / microscope** for the model rather than as business logic. Unit tests remain authoritative for invariants; concrete example models in tests or playgrounds are allowed and encouraged as pressure tests of the generic abstractions.

## 13. Causal loop

At an interpersonal scale, a social consumer may approximately implement:

`who I am -> what I express -> what you perceive -> how that fits your tolerance -> how our relation changes -> what future events do to us`

At larger scales the same primitives can participate through composite nodes, without making that particular loop mandatory for Concept.Core consumers.

## 14. What is deliberately not frozen

The following remain experimental:

- concrete numeric ranges;
- exact formulas for tension and complement transfer;
- whether Talent, Fragility, Tolerance and Expression belong in Core or a social/domain layer;
- exact mappings from plasticity stats to change rates;
- exact tolerance response curves;
- relation representation beyond its directional and causal role;
- algorithms for emergent characteristics in composite nodes;
- event scheduling and simulation cadence;
- concrete social characteristic vocabularies;
- concrete UI technology;
- any narrative or LLM layer.

These are implementation hypotheses, not conceptual truths. The first implementation should exist to test them while protecting the domain-agnostic boundary of Concept.Core.
