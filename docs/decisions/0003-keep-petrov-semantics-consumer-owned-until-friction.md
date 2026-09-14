---
id: decision.concept.petrov-consumer-owned-until-friction
type: decision-record
status: active
related:
  - docs/decisions/0002-single-consumer-need-does-not-justify-core-promotion.md
---

# Mantener la semántica de Petrov en el consumidor hasta observar fricción reusable

## Decision

Implementar el primer vertical Petrov usando el Concept actual y mantener la semántica específica del vertical fuera de `Concept.Core` hasta observar una fricción real que pueda formularse de manera reusable.

## Status

Active.

## Context

Petrov fue seleccionado por Anamnesis como primer vertical de presión. Incluye estado causal, observación parcial, autoridad, costes, evidencia y acciones posibles, pero esos conceptos todavía pertenecen al fenómeno experimental y no constituyen primitivas justificadas de Concept.

## Problem or tension

Diseñar primero una infraestructura genérica facilitaría el vertical, pero impediría saber qué capacidades son realmente necesarias y cuáles fueron anticipadas por conveniencia.

## Options considered

### Diseñar primero abstracciones genéricas para Petrov

Reduce trabajo local, pero arriesga transformar el vertical en ontología del Core.

### Usar Core como existe y esperar la primera fricción

Permite que implementación produzca evidencia sobre la frontera real.

## Selected option

Usar Core como existe y preservar Petrov consumer-owned hasta la primera fricción reusable.

## Rationale

La implementación debe ejercer el límite actual de Concept antes de extenderlo. Una fricción observada es evidencia más fuerte que una necesidad anticipada.

## Tradeoffs

| Gain | Cost or risk |
| --- | --- |
| Evidencia limpia sobre límites reales de Core | Código provisional en el consumidor |
| Reduce sobre-generalización | Puede requerir refactor posterior |
| Mantiene Petrov fuera de la ontología de Concept | Primera implementación puede ser menos conveniente |

## Consequences

- No se añade una primitive `Petrov`, `AuthorityPressure`, `World` o equivalente por anticipación.
- La primera implementación puede contener estructuras específicas del vertical.
- Cualquier propuesta de Core debe señalar la fricción concreta que la originó.

## Related artifacts

- `README.md`
- `docs/decisions/0002-single-consumer-need-does-not-justify-core-promotion.md`

## Review conditions

Revisar al aparecer la primera fricción repetida, un segundo consumidor independiente o una responsabilidad claramente genérica que el consumidor no debería poseer.