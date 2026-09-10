# Development boundaries

This document records constraints for implementation experiments. It exists to make accidental domain leakage visible while the API is still fluid.

## Concept.Core

Core may define reusable structural primitives such as:

- abstract node identity and extension boundaries;
- characteristic sets and membership;
- topology between characteristics;
- arbitrary characteristic state contracts;
- typed compatibility between characteristic definitions and state shapes;
- stable characteristic identity scoped by characteristic set when supported by pressure tests.

Core must not assume:

- a particular society or personality ontology;
- BiteFight characteristic names or circular geometry;
- Talent, Fragility, Tolerance, Expression, Perception, or social Relation semantics;
- that every characteristic has minimum/current/maximum state;
- that state representation is itself semantic identity;
- that textual local characteristic ids are globally unique;
- UI layout or story behavior.

A current pressure-tested identity model is `CharacteristicKey = (CharacteristicSetId, CharacteristicId)`. Treat it as provisional until mutation, persistence, schema evolution, and another consumer have challenged it.

## Consumers

Consumers define what nodes actually are and may introduce any domain-specific state shapes, rules, semantics, projections, or algorithms they need.

The fact that two consumers need similar concepts is evidence for possible promotion into Core; the fact that one consumer needs them is not.

## Pressure-test rule

Prefer friction over speculative abstraction. When a valid consumer requires awkward extra work, preserve and examine that friction before adding another generic mechanism. Missing extensibility should be visible in tests rather than hidden behind convenience APIs.

## Near-term review checkpoint

Before expanding into substantial mutation/transformation behavior, review the accumulated Core surface as a whole:

- whether each abstraction has survived more than one use case;
- whether names describe structural rather than social semantics;
- whether identities and ownership boundaries remain coherent;
- whether test fixtures reveal unnecessary ceremony;
- whether any convenience API is masking a missing abstraction;
- whether anything currently in Core belongs back in a consumer.
