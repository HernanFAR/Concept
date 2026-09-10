# Development boundaries

The first implementation exists to test the conceptual model without collapsing it into a single use case.

## Core rule

`Concept.Core` must remain domain-agnostic.

It may provide reusable primitives for:

- nodes;
- characteristic identity and grouping;
- characteristic sets;
- bounded characteristic state;
- generic topology/links between characteristics;
- composition and relation primitives when their semantics are demonstrably reusable;
- inspectable transitions.

It must not hard-code:

- a social ontology;
- BiteFight-like characteristics;
- individuals, families, cities or societies as special node classes;
- a circular UI;
- tolerance, talent, fragility or expression merely because the first consumer needs them.

Those concepts should be promoted into Core only when their generality is demonstrated.

## Concrete consumers are allowed

Concrete models are expected in tests, experiments and downstream projects. A future `Concept.Social` may depend on `Concept.Core` and define a social interpretation over the generic primitives.

Concrete test models are useful pressure tests. They should demonstrate that Core can support a domain without teaching Core the domain vocabulary.

## First development slice

The initial implementation should focus only on the smallest reusable foundation needed to represent:

1. a node;
2. one or more characteristic sets attached to that node;
3. characteristics identified within a set;
4. a bounded state with minimum, current value and maximum;
5. generic links between characteristics without assigning domain behavior to those links yet.

Mutation propagation, social plasticity, tolerance, perception and story presentation remain later slices.
