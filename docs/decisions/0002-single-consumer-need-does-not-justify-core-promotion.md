---
id: decision.concept.single-need-not-core-promotion
type: decision-record
status: active
related:
  - docs/decisions/0001-consumer-first-boundary.md
---

# Una necesidad individual no justifica promoción a Concept.Core

## Decision

Tratar una necesidad individual de Anamnesis como justificación suficiente para experimentar, pero no como evidencia suficiente para promover una abstracción a `Concept.Core`.

## Status

Active.

## Context

Anamnesis está empezando a ejercer presión real sobre Concept con necesidades como causalidad persistente, trayectorias reconstruibles, propiedades relacionales y evocación posterior.

## Problem or tension

Una capacidad puede ser necesaria y correcta para Anamnesis sin ser reusable ni pertenecer a la responsabilidad de Concept.Core.

## Options considered

### Promover al Core cuando una necesidad real aparece

Reduce duplicación temprana, pero mezcla necesidad del consumidor con ownership reusable.

### Experimentar primero fuera del Core

Preserva la distinción entre adecuación al consumidor y evidencia de generalidad.

## Selected option

Experimentar primero fuera del Core.

## Rationale

La evidencia mínima para Core debe superar “funcionó para este consumidor”. Concept busca estructuras que sobrevivan presión independiente y puedan expresarse sin absorber semántica específica.

## Tradeoffs

| Gain | Cost or risk |
| --- | --- |
| Menor acoplamiento a Anamnesis | Posible duplicación provisional |
| Mejor evidencia de reusabilidad | Promoción más lenta |
| Ownership más claro | Algunas APIs permanecerán incómodas durante discovery |

## Consequences

- Consumer adequacy y Core worthiness se evalúan por separado.
- Una implementación puede considerarse exitosa aunque permanezca consumer-specific.
- Rechazar promoción no implica rechazar la necesidad ni el experimento.

## Related artifacts

- `docs/decisions/0001-consumer-first-boundary.md`

## Review conditions

Revisar cuando aparezca estructura recurrente, presión de otro consumidor o una responsabilidad claramente genérica que el Core pueda asumir sin semántica específica.