# Evolution

This document records directions that are intentionally outside the current implementation scope but are expected to become useful once the underlying model is validated.

## Interactive story surface

A future evolution is an interactive **story UI** built on top of the same causal model exposed by the inspector/playground.

The story surface must not become a second simulation model. It should consume nodes, characteristic sets, expressions, perceptions, relations, events and other domain-defined state, then present meaningful actions and outcomes to a user.

The motivating shape is a narrative interaction where the actions available to a node and the consequences of choosing them are derived from the current simulated state rather than from an unrelated hard-coded story system.

Examples of possible actions include talking, observing, helping, intimidating, withdrawing or other domain-specific choices. These examples are illustrative only and do not belong in Concept.Core.

The intended sequence is:

1. validate the causal model with tests;
2. make it inspectable through the set-driven wheel/playground;
3. validate social dynamics through a consuming model such as Concept.Social;
4. only then explore an interactive story surface as another projection over the same state and events.

A narrative or LLM layer, if introduced later, should render and explain simulated causes rather than invent hidden causes that contradict them.
