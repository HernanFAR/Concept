---
id: decision.concept.consumer-first-boundary
type: decision-record
status: active
related:
  - README.md
---

# Mantener Concept consumer-first

## Decision

Evolucionar Concept a partir de presión real de consumidores concretos y promover al núcleo sólo estructuras que sobrevivan más allá de una necesidad individual.

## Status

Active.

## Context

Concept ya cuenta con una fundación pequeña de nodos, characteristic sets, state shapes y snapshots. La siguiente etapa debe descubrir capacidades a partir del consumo real, especialmente Anamnesis, sin convertir a ningún consumidor en la ontología del Core.

## Problem or tension

Expandir Core por anticipación puede producir abstracciones cómodas pero no justificadas; esperar presión real preserva evidencia sobre qué estructuras son verdaderamente compartidas.

## Options considered

### Expandir Core horizontalmente por diseño anticipado

Facilita APIs tempranas pero aumenta el riesgo de sobre-abstracción.

### Mantener consumer-first

Permite que consumidores concretos revelen primero la fricción y que la generalización ocurra después.

## Selected option

Mantener consumer-first.

## Rationale

Concept pretende ser reusable entre dominios sociales, narrativos, experimentales y de actores persistentes. Esa generalidad no puede justificarse desde la conveniencia de un único consumidor.

## Tradeoffs

| Gain | Cost or risk |
| --- | --- |
| Menos abstracción prematura | Más trabajo inicialmente en consumidores |
| Mejor procedencia de capacidades reusables | APIs compartidas aparecen más tarde |
| Core más pequeño y honesto | Puede existir duplicación provisional |

## Consequences

- Anamnesis puede ejercer presión real sin definir automáticamente Concept.Core.
- La fricción consumer-owned es evidencia, no deuda que deba eliminarse de inmediato.
- Las nuevas capacidades deben conservar procedencia desde el consumidor.

## Related artifacts

- `README.md`
- `docs/core-foundation-review-v0.1.md`

## Review conditions

Revisar si mantener una capacidad fuera de Core empieza a producir estructura repetida e independientemente útil entre consumidores.