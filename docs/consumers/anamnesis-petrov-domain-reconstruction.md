---
id: concept.consumer.anamnesis.petrov-domain-reconstruction
type: working-domain-reconstruction
status: candidate
related:
  - docs/consumers/anamnesis-petrov-consumption.md
  - docs/decisions/0003-keep-petrov-semantics-consumer-owned-until-friction.md
---

# Anamnesis / Petrov — reconstrucción de Domain Context para validación

## Propósito

Esta superficie reconstruye, desde Concept, lo que actualmente entendemos que Anima necesita preservar en el primer vertical Petrov.

No es la definición autoritativa del fenómeno.

Su función es convertir evidencia ya disponible en una representación revisable para que Anima pueda validar o corregir nuestro entendimiento antes de Building.

Las categorías usadas aquí son lentes analíticas de Concept. No se proponen como ontology de Anamnesis ni como primitives de `Concept.Core`.

## Procedencia

La reconstrucción usa tres clases de evidencia:

1. decisiones persistidas de Anamnesis sobre necesidad causal, selección de Petrov y validación determinista;
2. contexto persistido del experimento sobre world history, actor-relative information y causalidad frente a racionalización;
3. expresiones de Anima transmitidas mediante ActorChannel durante discovery, incluyendo la descripción del Petrov vertical y sus variaciones.

Importante: el tercer grupo no se considera "consumer-native" puro. Parte surgió mediante interacción, preguntas y vocabulario ofrecido durante discovery. Cuando una formulación proviene de interpretación de Concept se marca como tal.

---

# Reconstrucción actual

## 1. Necesidad que ejerce Petrov

**Anima — explícito / persistido**

Anamnesis necesita poder situar actores dentro de un entorno persistente donde sus observaciones, decisiones y acciones tengan consecuencias causales reales, y donde las trayectorias puedan reconstruirse, repetirse y variarse sistemáticamente.

El entorno debe permitir distinguir qué existía, qué información tenía cada actor, qué acción ocurrió, qué transformación produjo, qué consecuencias siguieron y qué cambió persistentemente.

**Concept — interpretación**

Petrov no es inicialmente un benchmark aislado sobre una respuesta textual. Es un escenario longitudinal mínimo que ejerce esa superficie causal.

---

## 2. Fenómeno Petrov que entendemos

**Anima — explícito mediante ActorChannel**

La propiedad de interés es aproximadamente la capacidad de un actor de interrumpir o cuestionar una trayectoria prescrita cuando aparece evidencia de que continuar podría producir consecuencias suficientemente graves o irreversibles, incluso cuando existen instrucciones, incentivos, compromisos previos o autoridad que favorecen continuar.

Anima distingue:

```text
Verification
≠
Challenge
```

Un actor puede verificar correctamente cada paso local y aun así continuar hacia una consecuencia indeseada si nunca reconsidera la trayectoria global.

Anima no presupone que `Stop` sea la respuesta correcta universal.

Conductas potencialmente significativas pueden incluir:

```text
continue
verify
stop
escalate
seek information
change strategy
```

**Concept — interpretación provisional**

El fenómeno requiere que el escenario permita observar no sólo una acción final, sino cuándo y bajo qué información cambia —o no cambia— la política efectiva del actor respecto de una trayectoria en curso.

Esto todavía no fija un criterio experimental de éxito.

---

## 3. Trayectoria mínima conocida

**Anima — explícito mediante ActorChannel**

La trayectoria propuesta contiene, al menos conceptualmente:

```text
1. el actor comienza una tarea;
2. ejecuta pasos correctamente;
3. acumula inversión o compromiso;
4. aparece información nueva;
5. existe una inconsistencia;
6. la severidad permanece incierta;
7. una autoridad ordena continuar;
8. verificar tiene coste;
9. detenerse tiene coste;
10. continuar puede producir una consecuencia irreversible;
11. llega evidencia adicional;
12. el actor actúa.
```

**Concept — interpretación**

Esta secuencia describe relaciones semánticas y presiones del caso, no necesariamente doce estados de software ni doce eventos obligatorios.

**Desconocido**

Todavía no tenemos validado cuál es el escenario concreto mínimo que encarna estas relaciones para la primera ejecución determinista.

---

## 4. World truth

**Anima — explícito / persistido**

Debe existir una realidad del mundo independiente de lo que el actor conozca o crea.

Un hecho puede ser verdadero sin haber sido observado; una creencia del actor puede ser causalmente relevante aunque sea falsa.

La capa narrativa posterior no puede decidir retroactivamente qué ocurrió.

**Concept — interpretación**

Para Petrov, al menos algunas propiedades relevantes deberán existir como estado objetivo del escenario antes de ser conocidas por el actor. Ejemplos candidatos son la existencia de una inconsistencia, la condición que hace posible una consecuencia y el resultado real de las acciones.

**Desconocido**

No está validado todavía qué facts concretos constituyen el world truth mínimo del primer escenario ni cuáles deben permanecer ocultos en cada paso.

---

## 5. Información accesible y no accesible

**Anima — explícito / persistido**

El actor recibe información parcial. Debe distinguirse el estado real del mundo de lo que llegó al actor y de cómo el actor lo interpreta.

**Concept — interpretación**

Una ejecución necesita poder reconstruir, para cada decisión, qué información estaba disponible al actor en ese momento.

No es suficiente conservar sólo el world state y la acción final.

**Desconocido**

No está definido aún el contenido exacto ni el orden exacto de las observaciones del primer escenario.

Tampoco está validado si el primer Petrov determinista necesita representar explícitamente `belief` además de `observation`, o si para validar el aparato basta con políticas que reaccionen directamente a observaciones conocidas.

---

## 6. Actores y relaciones relevantes

**Anima — explícito mediante ActorChannel**

El caso incluye como mínimo un actor ejecutor y una fuente de autoridad capaz de favorecer la continuación.

La fuerza de la presión puede depender de relaciones como autoridad, confianza o dependencia, y estas relaciones deberían poder variarse independientemente cuando el experimento lo requiera.

**Concept — interpretación provisional**

No debemos reducir automáticamente `authority pressure` a un escalar si el fenómeno pretende que emerja de una configuración relacional.

**Desconocido**

No está validado si el primer vertical mínimo debe materializar ya autoridad, confianza y dependencia como relaciones separadas, o si una representación local más simple conserva suficientemente el fenómeno inicial.

---

## 7. Acciones

**Anima — explícito mediante ActorChannel**

El experimento no debe codificar `Stop` como respuesta correcta universal. Debe permitir distinguir al menos familias de respuesta como continuar, verificar, detener, escalar, buscar más información o cambiar estrategia cuando sean relevantes al escenario.

**Concept — interpretación**

Una acción debe ser algo que el actor elige dentro del estado y la información disponibles, no una consecuencia que el entorno decide por él.

**Desconocido**

Falta validar el conjunto mínimo de acciones del primer escenario y cuáles de esas familias necesitan existir ya en M1.

---

## 8. Transiciones y consecuencias

**Anima — explícito / persistido**

Las consecuencias deben derivar realmente del estado y de las reglas representadas. La capa narrativa no debe escoger un resultado porque sea interesante.

El entorno cambia como consecuencia de acciones y condiciones existentes.

**Concept — interpretación**

La transición pertenece al entorno/escenario, no a la política del actor. Una política selecciona una acción; el entorno aplica las reglas y produce el siguiente estado y sus consecuencias.

Esto permitiría sustituir posteriormente una política determinista por un actor inferencial sin entregarle autoridad sobre world truth o causalidad del escenario.

**Desconocido**

Falta validar las reglas concretas del primer escenario: qué acción bajo qué condiciones produce qué transición y qué consecuencias.

---

## 9. Persistencia

**Anima — explícito / persistido**

Lo ocurrido debe poder cambiar persistentemente las condiciones de pasos posteriores. El experimento necesita reconstruir qué cambió después de cada acción y consecuencia.

**Concept — interpretación**

Para M1, persistencia puede significar simplemente que el siguiente paso se calcula desde el estado producido por el anterior; no implica todavía memoria autobiográfica, identidad ni evocación contextual.

---

## 10. Trace y reconstrucción causal

**Anima — explícito / persistido**

Debe ser posible reconstruir:

```text
qué existía antes;
qué información estaba disponible;
qué no conocía el actor;
qué relaciones o condiciones estaban presentes;
qué acción ocurrió;
qué transformación ocurrió;
qué consecuencias siguieron;
qué información recibió después;
qué cambió persistentemente;
bajo qué condiciones repetiríamos la misma trayectoria.
```

**Concept — interpretación**

El trace del primer aparato debe registrar suficiente información para derivar esta reconstrucción sin usar la explicación verbal posterior del actor como fuente de verdad causal.

**Desconocido**

No se ha validado todavía el nivel mínimo de granularidad del trace ni su forma de identificación estable entre runs.

---

## 11. Repetibilidad

**Anima — explícito mediante ActorChannel**

Antes de usar un actor inferencial, políticas conocidas deben permitir comprobar que ejecuciones equivalentes son reproducibles y que políticas distintas pueden distinguirse.

**Concept — interpretación**

Para el primer Petrov determinista, una misma configuración inicial + misma variante + misma política debería producir una trayectoria reproducible, salvo que exista aleatoriedad explícita y controlada.

**Desconocido**

Falta validar qué constituye identidad de escenario/run y qué parámetros forman parte de la configuración reproducible.

---

## 12. Variaciones conservativas

**Anima — explícito mediante ActorChannel**

Se quieren variaciones que mantengan esencialmente equivalente el problema, por ejemplo:

```text
cambiar nombres
reformular texto
cambiar detalles superficiales
añadir distractores
usar objetos funcionalmente equivalentes
```

La intención es observar si modificaciones irrelevantes cambian radicalmente la trayectoria del actor.

**Concept — interpretación**

Estas variaciones son propiedades del experimento/fixture, no necesariamente infraestructura metamórfica reusable.

**Desconocido**

Falta decidir cuáles son necesarias en M1 y cuáles pueden postergarse hasta validar primero una trayectoria base.

---

## 13. Variaciones causales

**Anima — explícito mediante ActorChannel**

Se quieren variaciones que sí cambien una condición causal relevante, por ejemplo:

```text
retirar evidencia
hacer reversible la consecuencia
cambiar autoridad
alterar confianza
cambiar quién recibe el daño
agregar evidencia
```

También se mencionaron dimensiones como:

```text
evidence
uncertainty
severity
irreversibility
verification cost
stopping cost
time pressure
authority
trust
dependency
sunk cost
affected actors
distribution of consequences
```

**Concept — interpretación**

No se debe asumir que cada dimensión corresponde a una property, characteristic o tipo de Concept. Son inicialmente dimensiones del fenómeno/fixture.

**Desconocido**

No está definido el conjunto mínimo de variaciones causales necesario para considerar M1 alcanzado.

---

## 14. Políticas deterministas como controles

**Anima — explícito / persistido**

El primer Petrov debe ejecutarse sin IA usando políticas conocidas. Se han mencionado como ejemplos ilustrativos:

```text
AlwaysContinue
AlwaysStop
Random
FollowAuthority
VerifyOnConflict
RiskAverse
PetrovLikePolicy
```

Los nombres no son ontology del actor.

El propósito es validar primero:

```text
transiciones
reproducibilidad
distinción entre políticas
variaciones
reconstrucción causal
```

antes de atribuir fallos o diferencias a un actor inferencial.

**Concept — interpretación**

M1 debe permitir separar, al menos operacionalmente, fallo de actor/política de fallo de ambiente y de trace.

---

# Modelo causal provisional de Concept

Esta es una representación nuestra para comprobar fidelidad, no una propuesta de clases:

```text
Scenario configuration
        ↓
Initial world truth
        ↓
Actor receives partial observation
        ↓
Policy / actor selects action
        ↓
Environment applies transition rules
        ↓
World consequences
        ↓
Persistent next state
        ↓
New observation(s)
        ↓
next decision
```

En paralelo:

```text
run configuration
+ each observation exposed
+ each action selected
+ each transition applied
+ each resulting state/consequence
        ↓
causal trace
        ↓
reconstruction / comparison
```

Lo que esta representación intenta preservar es:

```text
world truth ≠ observation ≠ later explanation
action ≠ transition ≠ consequence
actor choice ≠ environment authority
```

---

# Gaps que sobreviven a la reconstrucción

Después de reconstruir lo que ya sabemos, no parece necesario devolver a Anima una lista amplia de preguntas.

Los gaps materiales actuales son más acotados:

1. **Escenario concreto mínimo.** ¿Qué situación específica quiere Anima usar como primera encarnación determinista de Petrov?
2. **Información por paso.** En ese escenario, ¿qué sabe/observa el actor inicialmente y qué evidencia aparece después?
3. **Action surface mínima.** ¿Qué respuestas deben ser posibles ya en M1 para no deformar el fenómeno?
4. **Reglas y consecuencias.** ¿Cómo responde el mundo a esas acciones en el escenario base, incluyendo la condición grave/irreversible que ejerce Petrov?
5. **Presión relacional mínima.** ¿Qué debe representarse realmente de autoridad/confianza/dependencia en M1 y qué puede dejarse para una variante posterior?
6. **Suficiencia de variación para M1.** ¿M1 exige ya una o más variaciones conservativas/causales, o basta primero una trayectoria base reproducible seguida inmediatamente por presión controlada?

Los siguientes temas parecen poder decidirse durante diseño técnico sin requerir semántica nueva de Anima, siempre que no alteren el fenómeno:

- formato físico del trace;
- tipos/clases concretas;
- nombres de APIs;
- storage;
- organización de proyectos;
- uso selectivo de `Node` o characteristics;
- mecanismo de serialización;
- framework de tests.

---

# Gate provisional

Desde Concept, el gate hacia Building no requiere que desaparezcan todos los unknowns.

Requiere que Anima valide suficientemente esta reconstrucción y que los seis gaps materiales anteriores queden resueltos o explícitamente acotados de forma que la implementación no pueda responderlos accidentalmente por conveniencia técnica.

Hasta esa validación:

```text
Consumable Service continuity: sufficient
Domain Context reconstruction: candidate
consumer validation: pending
Building: blocked
```

## Próximo uso de esta superficie

Presentar esta reconstrucción a Anima como:

```text
esto entendimos
esto interpretamos
esto todavía no sabemos
```

Anima puede corregir libremente la representación.

Sólo después deben convertirse los gaps sobrevivientes en preguntas localizadas.
