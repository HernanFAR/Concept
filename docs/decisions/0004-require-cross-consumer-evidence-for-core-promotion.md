---
id: decision.concept.cross-consumer-core-promotion
type: decision-record
status: active
related:
  - docs/decisions/0001-consumer-first-boundary.md
  - docs/decisions/0002-single-consumer-need-does-not-justify-core-promotion.md
---

# Exigir evidencia más allá de un único consumidor para promover a Core

## Decision

Promover una capacidad a `Concept.Core` sólo cuando exista evidencia suficiente de estructura reusable más allá de una necesidad individual, preferentemente mediante recurrencia independiente entre consumidores o una responsabilidad claramente genérica demostrada por presión real.

## Status

Active.

## Context

Concept define actualmente su evolución mediante el criterio: `Do not abstract from one need. Abstract from structural similarity between independently useful needs.`

## Problem or tension

Una regla rígida de “dos consumidores o nada” sería artificial, pero promover desde una sola necesidad confundiría adecuación local con generalidad.

## Options considered

### Exigir un número fijo de consumidores

Es fácil de aplicar, pero convierte una heurística en dogma y puede bloquear una responsabilidad obviamente genérica.

### Evaluar evidencia de estructura reusable

Permite recurrencia independiente, presión cruzada o una responsabilidad genérica claramente separable del consumidor.

## Selected option

Evaluar evidencia de estructura reusable sin fijar un contador obligatorio de consumidores.

## Rationale

Lo relevante es la independencia y calidad de la presión que sostiene la abstracción, no un número arbitrario.

## Tradeoffs

| Gain | Cost or risk |
| --- | --- |
| Evita una regla mecánica | Requiere juicio explícito |
| Permite promoción cuando la responsabilidad genérica es clara | Puede generar desacuerdo sobre suficiencia |
| Preserva consumer-first sin inmovilizar Core | La promoción puede ser más lenta |

## Consequences

Una candidate abstraction puede permanecer `consumer-specific`, `provisional`, ser promovida, rechazada o disuelta. La trayectoria y evidencia deben conservarse.

## Related artifacts

- `README.md`
- `docs/decisions/0001-consumer-first-boundary.md`
- `docs/decisions/0002-single-consumer-need-does-not-justify-core-promotion.md`

## Review conditions

Revisar cuando la experiencia de varias promociones muestre criterios de suficiencia más precisos o revele que esta regla produce sobre- o sub-generalización.