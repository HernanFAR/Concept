# Phase 2 — characteristic identity

This pressure test asks what makes a characteristic *the same characteristic* once definitions are reconstructed, serialized, or used outside one in-memory graph.

## Problem exposed by typed definitions

`CharacteristicDefinition<TState>` is useful as a typed handle, but reference identity is too strong: rebuilding an equivalent definition after serialization would produce a different object even though the domain means the same characteristic.

At the same time, `CharacteristicId` alone is too weak because different characteristic sets should be allowed to reuse local names such as `level`, `weight`, or `status` without colliding.

## Alternatives considered

### Definition-instance identity

Two definitions are the same only when they are the same object instance.

This protects against accidental cross-set use, but breaks reconstruction and persistence. It also makes semantic identity depend on allocation history.

**Rejected as the stable identity model.**

### Globally unique `CharacteristicId`

Every characteristic id would be unique across all sets.

This is simple, but forces consumers to encode set/domain context into every id and prevents natural reuse of local vocabulary.

**Rejected for now.**

### Schema-bound identity

Identity could include state shape or a schema id.

This makes schema evolution part of identity: changing a representation could accidentally create a new semantic characteristic. The current model wants state compatibility to be validated without making representation itself the characteristic's semantic address.

**Not selected.**

### Set-scoped identity

Use the pair:

`(CharacteristicSetId, CharacteristicId)`

The branch expresses this explicitly as `CharacteristicKey`.

This lets different sets reuse local ids while giving reconstructed definitions a stable address. State type remains a compatibility contract checked separately.

**Current experiment.**

## Current implementation

`CharacteristicDefinition<TState>` now carries a `CharacteristicKey` rather than a bare `CharacteristicId`.

A `CharacteristicSetDefinition` rejects definitions whose key belongs to another set.

Typed lookup accepts a reconstructed definition when:

- its `CharacteristicKey` matches a characteristic in the set; and
- its state type matches the set's canonical definition.

It rejects:

- the same local `CharacteristicId` under another `CharacteristicSetId`;
- the same key paired with an incompatible state type.

This deliberately separates two questions:

- **identity:** which characteristic is this? → `CharacteristicKey`;
- **compatibility:** what state shape may it hold? → `CharacteristicDefinition<TState>`.

## Pressure-test results

The current tests demonstrate that:

1. the same local id can exist in multiple sets without collision;
2. a definition reconstructed independently can still act as a typed handle;
3. cross-set aliases do not silently resolve;
4. state-shape mismatch does not become identity equality;
5. topology can remain local to a set and continue using `CharacteristicId` because the containing set already supplies the missing scope.

## Remaining questions

This is still provisional. Mutation and serialization should challenge at least:

- whether `CharacteristicKey` is sufficient across versioned schemas;
- whether set ids themselves need a stronger namespace when multiple packages define sets with the same textual id;
- whether display metadata may change independently without affecting identity;
- whether migrations ever need aliases from one key to another.

Do not solve these pre-emptively.

## Current conclusion

Set-scoped identity is the smallest model that survives both local vocabulary reuse and reconstructed definitions.

It is therefore the current preferred identity model, but should remain open until mutation, persistence, and a second consumer have exercised it.
