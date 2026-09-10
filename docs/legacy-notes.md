# Legacy notes

The previous implementation has been removed from this branch intentionally.

Two old ideas remain conceptually relevant:

1. **Node as a broad world concept.** The legacy `Node` described something existing in a world context and explicitly cited examples such as person, place, thing, identity and society. That broad scope aligns with the new recursive node model.
2. **Characteristics derived through relations.** The legacy code experimented with a `RelationAverageCharacteristicYieldStrategy` that derived characteristics from related nodes. The averaging algorithm itself is not retained, but the intuition that a node's characteristics may emerge from related/composed nodes remains useful.

Everything else should be treated as historical implementation, not as a constraint on the new design. In particular, the old C# APIs, comparers, dictionaries, execution context and yield strategies are not part of conceptual model v0.1.
