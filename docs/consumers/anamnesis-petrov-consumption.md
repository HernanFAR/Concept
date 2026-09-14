---
id: concept.consumer.anamnesis.petrov
type: consumable-service-continuity
status: active
related:
  - docs/decisions/0001-consumer-first-boundary.md
  - docs/decisions/0002-single-consumer-need-does-not-justify-core-promotion.md
  - docs/decisions/0003-keep-petrov-semantics-consumer-owned-until-friction.md
  - docs/decisions/0004-require-cross-consumer-evidence-for-core-promotion.md
---

# Anamnesis / Petrov — continuidad de consumo de Concept

## Propósito

Preservar cómo Anamnesis debe consumir Concept durante el primer vertical Petrov sin convertir la semántica del experimento en ontología de `Concept.Core`.

Esta superficie responde principalmente a:

```text
consumer need
→ current Concept capability
→ consumer realization
→ observed friction
→ reusable hypothesis
→ promote / reject / keep consumer-specific
```

No define qué significa Petrov para Anamnesis, qué diferencias conductuales son relevantes para el fenómeno ni qué inferencias experimentales pueden sostenerse a partir de una ejecución.

## Punto de entrada

La necesidad recibida desde Anamnesis exige un entorno causal persistente donde un actor pueda recibir información parcial, actuar, producir consecuencias reales, repetir trayectorias y variar condiciones controladamente.

Petrov es el primer vertical elegido para ejercer esa necesidad.

La primera materialización debe ser determinista y usar políticas conocidas antes de introducir un actor inferencial.

La semántica de Petrov permanece propiedad del consumidor.

## Capacidades actuales de Concept que sí pueden consumirse

El Core actual ofrece una foundation deliberadamente pequeña:

- `Node` como boundary de extensión para nodos concretos definidos por consumidores;
- identidad de nodos;
- characteristic sets definidos por el consumidor;
- estados de características heterogéneos;
- `CharacteristicDefinition<TState>` como owner de compatibilidad de shape;
- `CharacteristicKey = (CharacteristicSetId, CharacteristicId)` como identidad estable actualmente pressure-tested;
- `CharacteristicSetState` como snapshot inmutable validado;
- topología descriptiva entre características.

Estas capacidades pueden usarse cuando describan naturalmente alguna parte del vertical, pero Petrov no está obligado a convertir todo su estado en características.

## Capacidades que Concept no ofrece actualmente

No deben darse por existentes durante Building:

- `World` genérico;
- motor de eventos;
- historia causal ejecutable;
- transformación genérica de nodos;
- `Relation` ejecutable;
- `Perception` ejecutable;
- observación parcial genérica;
- trace/provenance engine;
- scheduler o runtime de escenarios;
- políticas de decisión;
- semántica de autoridad, riesgo, irreversibilidad o challenge.

La ausencia de estas capacidades no constituye por sí sola una razón para agregarlas a Core.

## Modelo de consumo para el primer Petrov

Durante el primer vertical, Anamnesis puede definir localmente todo mecanismo que sea necesario para expresar fielmente el experimento, incluyendo estructuras específicas para:

```text
estado del escenario
world truth
observaciones disponibles
acciones posibles
políticas deterministas
transiciones
consecuencias
traza de ejecución
variantes experimentales
```

Concept se consume sólo donde sus primitivas existentes reduzcan duplicación o preserven una responsabilidad claramente genérica sin deformar la semántica del consumidor.

La dirección inicial es por tanto:

```text
Anamnesis Petrov semantics
        ↓
consumer-owned realization
        ↓
use existing Concept primitives where they fit naturally
        ↓
execute
        ↓
observe friction
        ↓
classify ownership
```

No:

```text
Petrov vocabulary
        ↓
rename generically
        ↓
Concept.Core
```

## Qué cuenta como fricción útil

Una fricción merece registrarse cuando el consumidor puede señalar un problema concreto al usar el Concept actual, por ejemplo:

- una responsabilidad estructural debe implementarse repetidamente fuera de Core;
- dos estructuras conceptualmente iguales no pueden compartir una primitive existente sin pérdida semántica;
- una garantía que debería pertenecer al proveedor sólo puede mantenerse mediante disciplina manual del consumidor;
- una identidad o referencia estable necesaria para reconstrucción causal no puede preservarse con los mecanismos actuales;
- una operación genérica requiere acceder a detalles de representación que deberían permanecer encapsulados;
- el mismo patrón reaparece bajo semánticas independientes de Petrov.

No cuenta como evidencia suficiente de promoción:

- que una API genérica sería más cómoda;
- que Petrov necesita un nombre nuevo;
- que una estructura aparece una sola vez;
- que el consumidor contiene código provisional;
- que una abstracción podría ser útil hipotéticamente en el futuro.

## Cómo preservar una fricción

Antes de modificar `Concept.Core`, registrar como mínimo:

```text
consumer need
current realization
exact friction
why the consumer should or should not own it
existing mechanisms considered
minimum local workaround
observable cost or limitation
candidate reusable responsibility, if any
what evidence would falsify promotion
```

Después ejercer primero el mecanismo mínimo local cuando sea posible.

La implementación del consumidor es evidencia de diseño: puede mostrar que una abstracción anticipada era innecesaria, que la fricción desaparece o que la responsabilidad realmente se repite.

## Boundary de semántica y autoridad

Anamnesis / Anima posee:

- el significado del fenómeno que Petrov pretende representar;
- qué diferencias conductuales importan para ese fenómeno;
- significado de los estados del escenario;
- significado de observaciones y acciones;
- reglas de transición del vertical;
- consecuencias del mundo;
- semántica de autoridad, evidencia, coste, gravedad e irreversibilidad;
- definición de las variantes experimentales.

Research:

- formaliza hipótesis y criterios experimentales;
- cuestiona si una operacionalización permite sostener las inferencias pretendidas;
- identifica confusores, amenazas a validez y problemas de falsabilidad;
- no redefine por autoridad propia el fenómeno que Anima pretende estudiar.

Concept posee:

- sus primitives genéricas existentes;
- sus garantías estructurales;
- la decisión de promover, rechazar o mantener una candidate abstraction fuera de Core;
- la preservación de evidencia que justifique una evolución de Concept.

## Gate de Concept hacia Building

Desde la perspectiva de Consumable Service, Building puede comenzar cuando se cumplan estas condiciones:

1. Anamnesis puede expresar el primer Petrov usando semántica consumer-owned sin depender de una primitive nueva de Core no justificada.
2. Está claro qué capacidades actuales de Concept son opcionales de consumir y cuáles simplemente no existen todavía.
3. La implementación tiene permiso explícito para introducir mecanismos locales provisionales.
4. Cualquier presión sobre Core deberá señalar una fricción observada y no una conveniencia anticipada.
5. La semántica necesaria para implementar world truth, observation, action, transition, consequence y trace está suficientemente preservada por el consumidor.

Los puntos 1–4 están suficientemente establecidos por las decisiones actuales de Concept.

El punto 5 pertenece al Domain Context de Anamnesis y debe verificarse antes de considerar abierto el gate completo hacia Building.

## Estado actual

```text
Consumable Service continuity:
  sufficient for first implementation attempt

Concept.Core changes required before Building:
  none currently demonstrated

Expected first implementation shape:
  consumer-owned Petrov realization
  + selective use of existing Concept primitives

Current blocking continuity outside Concept ownership:
  operational Petrov Domain Context
```

## Condiciones de revisión

Revisar esta superficie cuando:

- Building produzca la primera fricción concreta contra Concept;
- aparezca una candidate abstraction;
- un segundo consumidor ejerza una estructura semejante;
- una responsabilidad actualmente local revele ownership claramente genérico;
- o el modelo de consumo actual obligue a deformar la semántica del consumidor.
