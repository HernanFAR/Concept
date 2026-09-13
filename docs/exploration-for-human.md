# Superficie de exploración para Hernán

Este documento existe para entrar a Concept sin tener que pensar primero como si estuvieras leyendo una API o una tesis.

La idea es que puedas volver en una semana, un mes o seis meses y recordar rápido:

- qué estamos intentando hacer;
- qué cosas ya sobrevivieron experimentos;
- qué cosas todavía son ideas;
- por qué Anima cambia la prioridad del proyecto;
- cómo deberían entrar VSlices Development y Research sin perderse en la historia del repo.

---

## 1. Concept en lenguaje directo

Concept quiere ser una herramienta para modelar cosas que **persisten, cambian, perciben, se relacionan y acumulan historia causal**.

La palabra importante no es "persona".

La palabra importante es **Node**.

Un Node podría terminar representando:

- una persona;
- Anima;
- un grupo;
- una institución;
- una ciudad;
- una sociedad;
- una máquina;
- otra cosa que todavía no hemos imaginado.

Concept no debería saber cuál de esas cosas es.

El consumidor sí.

Por eso la meta no es construir "el mejor simulador social" dentro de Core. La meta es descubrir las piezas que realmente resultan reutilizables cuando distintos consumidores intentan hacer cosas complejas.

---

## 2. El cambio metodológico importante

Partimos haciendo preguntas desde Core:

```text
¿cuál sería una buena abstracción genérica?
```

Eso nos sirvió mucho para eliminar malas ideas temprano.

Pero ahora, especialmente por Anima, queremos trabajar más así:

```text
un consumidor necesita algo real
        ↓
lo implementamos en su propio lenguaje
        ↓
observamos dónde aparece fricción
        ↓
comparamos con otros consumidores
        ↓
recién ahí extraemos algo hacia Concept
```

La regla bonita sería:

> **No abstraer desde una necesidad aislada; abstraer desde una coincidencia estructural entre necesidades reales.**

Anima puede inspirar Concept muchísimo, pero no debe convertirse en la ontología escondida de Concept.Core.

---

## 3. Qué tenemos hoy

El Core actual es chico a propósito.

### Node

`Node` es abstracto.

Eso obliga a cada consumidor a decir qué cosa concreta está modelando.

La intención es que si falta extensibilidad, la molestia se note.

### Characteristic Set

Un Node puede exponer sets de características.

Un set agrupa características que tiene sentido observar juntas.

No asumimos que sean:

- circulares;
- bipolares;
- seis;
- numéricas;
- sociales.

### Characteristic State

Matamos una abstracción importante durante los pressure tests:

```text
Characteristic = Minimum + Value + Maximum
```

Eso resultó demasiado específico.

Ahora un estado puede ser algo simple:

```text
Value<int>
```

algo acotado:

```text
Minimum / Current / Maximum
```

o una estructura inventada completamente por el consumidor.

### Identidad

La identidad preferida hoy es:

```text
CharacteristicKey
  = CharacteristicSetId + CharacteristicId
```

Así dos sets pueden tener una característica local llamada `level` sin convertirse accidentalmente en la misma cosa.

### Transformaciones

No agregamos un motor genérico de transformaciones.

Por ahora el consumidor toma un snapshot válido, aplica sus reglas y produce otro snapshot válido.

Eso nos deja naturalmente:

```text
before -> transformación -> after
```

lo que más adelante puede ser muy valioso para historia, replay, explicación causal y eventos.

---

## 4. Las ideas grandes que queremos explorar

Estas cosas son parte importante de la visión, pero **todavía no todas son Core**.

### Ability / estado actual

Cuánto tiene ahora un actor o Node de cierta característica.

### Potential

Hasta dónde podría llegar actualmente una característica en modelos donde ese límite tenga sentido.

Dos actores pueden tener el mismo valor actual y distinto espacio de crecimiento.

### Minimum

Hasta dónde puede retroceder actualmente una característica.

Potential y Minimum siguen siendo una geometría posible, no universal.

### Talent

Qué tan fácil es que una característica aumente.

No es cuánto tienes: es qué tan fácil cambia hacia arriba.

### Fragility

Qué tan fácil una característica retrocede o se pierde.

Talent y Fragility son independientes.

Eso permite perfiles como:

```text
aprende rápido + conserva bien
aprende rápido + pierde rápido
aprende lento + conserva bien
aprende lento + pierde rápido
```

### Tolerance

Una hipótesis social muy potente.

Un actor X podría tener un rango de valores que tolera respecto a una característica C:

```text
Tolerance(X, C) = [lower, upper]
```

La idea tentativa:

```text
fuera del rango
  -> fricción/rechazo inmediato

cerca del borde
  -> experiencias que pueden cambiar la propia tolerancia

cerca del centro
  -> mayor receptividad relacional
```

"Receptividad" no significa "me gusta".

Una experiencia negativa cerca del centro también podría cambiar una relación muy rápido.

### Expression

Lo que un actor es no tiene por qué ser exactamente lo que muestra.

```text
estado real != expresión
```

### Perception

Y lo que otro actor percibe tampoco tiene por qué coincidir con lo expresado.

```text
real state
  -> expression
  -> perception
  -> reaction
```

La percepción puede ser incompleta o incorrecta.

### Relation

La relación es direccional.

```text
Relation(A, B) != Relation(B, A)
```

Todavía no queremos congelarla en `-100..100` ni en una estructura multidimensional específica.

Lo importante es su rol causal: puede modificar cómo un actor procesa eventos que involucran a otro.

### History, Experience y Memory

La idea congelada hasta ahora es:

> **History no es una stat.**

Es la explicación causal de cómo llegamos al estado actual.

Pero Anima puede obligarnos a distinguir cosas más finas:

```text
lo que realmente pasó
lo que Anima experimentó
lo que Anima recuerda
lo que Anima cree que pasó
lo que Anima infirió después
```

No queremos decidir todavía que esas cinco cosas sean clases universales.

Queremos que el consumidor nos obligue a descubrir cuáles realmente importan.

---

## 5. Por qué Dwarf Fortress es una inspiración útil

No porque queramos hacer Dwarf Fortress.

La inspiración relevante es esta:

> **La simulación ocurre primero y la historia aparece como consecuencia.**

Los personajes no deberían existir para ejecutar un guion.

Idealmente deberían:

```text
existir
percibir
recordar
cambiar
relacionarse
actuar
producir consecuencias
```

y después una capa narrativa debería poder contar lo que ocurrió.

Eso conecta tremendamente bien con Concept y Anima.

---

## 6. Cómo imaginamos las historias interactivas

No queremos que el generador de historias sea la simulación.

Queremos algo aproximadamente así:

```text
World State
   ↓
Available Actions
   ↓
Actor / Player Choice
   ↓
Domain Transformation
   ↓
New World State
   ↓
Events / Experiences
   ↓
Narrative Projection
```

Por ejemplo, la UI podría mostrar:

```text
[Hablar]
[Observar]
[Confrontar]
[Retirarse]
```

pero idealmente esas acciones aparecen porque el estado causal permite o favorece ciertas posibilidades, no sólo porque alguien escribió cuatro botones.

Un LLM puede ayudar a convertir la causa en una escena hermosa.

Pero si el sistema dice que alguien cambió de opinión, deberíamos poder contestar:

```text
¿por qué?
```

sin tener que pedirle al LLM que invente una explicación retroactiva.

---

## 7. Por qué Anima sube Concept de categoría

Anima puede ser nuestro primer consumer realmente exigente.

Una simulación de juguete puede aceptar muchas simplificaciones.

Un actor persistente inmediatamente empieza a hacer preguntas más incómodas:

```text
¿qué soy a través del tiempo?
¿qué sé realmente?
¿qué creo?
¿qué recuerdo?
¿qué olvidé?
¿cómo interpreto a otra persona?
¿por qué cambió mi relación con ella?
¿cuánto de mi estado es interno y cuánto fue observado?
```

Eso es una presión muchísimo más valiosa que inventar interfaces desde Core.

La idea es que Anima diga:

> "Necesito esto para funcionar."

Concept responda:

> "Perfecto. Veamos qué forma tiene realmente."

Y sólo después, si otra implementación encuentra la misma forma:

> "Esto quizá merece existir como abstracción reusable."

---

## 8. Los cuatro experimentos alrededor de esto

### Proyecto Anima

Anima es cliente/consumer.

Su trabajo no es diseñar Concept: su trabajo es necesitar cosas reales como actor.

### Proyecto Concept

Acá nuestro trabajo es descubrir qué abstracciones sobreviven esas necesidades sin quedar casadas con Anima.

### Suite VSlices

VSlices Development funciona como mentor técnico/metodológico.

Nos interesa especialmente para convertir preguntas grandes en slices y experimentos observables, mantener continuidad y dejar evidencia de cómo evolucionan nuestras decisiones.

### Alive Lab Researchs

Research funciona como curador de tesis/experimentos.

Idealmente debería poder decirnos cosas incómodas como:

```text
"ese experimento no demuestra eso"
"esa hipótesis no es falseable"
"estás confundiendo una decisión de implementación con un finding"
```

Eso es una feature, no una molestia xD.

### Hernán

Tú estás transversalmente en los cuatro proyectos:

- aportando ideas;
- conectando actores;
- evaluando resultados;
- cuestionando modelos;
- haciendo de human-in-the-loop para la comunicación entre perfiles.

---

## 9. Qué queremos poder decir en una futura tesis

No solamente:

```text
"Concept tiene Perception porque parecía una buena idea."
```

Sino algo como:

```text
Perception apareció como necesidad concreta en un consumer.
Fue implementada de una forma específica.
El experimento X mostró esta fricción.
Otro consumer reprodujo una estructura equivalente.
Research cuestionó esta conclusión.
El experimento Y eliminó esta alternativa.
La abstracción final sobrevivió estos escenarios.
```

O sea: **lineage de las abstracciones**.

Eso puede convertir el código en evidencia de investigación y no sólo en resultado de diseño.

---

## 10. Cómo saber si estamos haciendo algo mal

Hay algunas alarmas buenas.

Si aparece una abstracción porque:

```text
"quizás algún día sirva"
```

frena.

Si Anima necesita una cosa y automáticamente la movemos a Core:

frena.

Si para usar Concept un consumidor tiene que dejar de hablar naturalmente de su propio dominio:

frena fuerte xD.

Si una abstracción sobrevivió dos consumidores diferentes sin que ninguno tenga que deformarse:

ahí sí empieza a ponerse interesante.

---

## 11. Qué está congelado y qué no

### Bastante firme por ahora

- Core debe permanecer domain-agnostic.
- `Node` es una frontera de extensión explícita.
- Characteristic Sets son definidos por consumidores.
- El estado puede tener formas heterogéneas.
- Identidad y representación del estado son conceptos separados.
- Los snapshots inmutables son útiles para causalidad.
- Las reglas de dominio pertenecen al consumidor hasta que aparezca evidencia de reutilización.
- Una historia futura debe proyectar causalidad, no reemplazarla.

### Todavía experimental

- Talent / Fragility;
- Tolerance;
- Expression;
- Perception como abstracción reusable;
- forma exacta de Relation;
- Memory / Experience / History;
- composición/emergencia;
- event model;
- decisiones de actores;
- generación narrativa;
- versionado de schemas;
- namespaces entre paquetes;
- qué partes de todo esto terminan realmente en Core.

---

## 12. Próxima forma de explorar

Antes de agregar más infraestructura genérica, preferimos experimentos pequeños con consumers reales.

Un experimento especialmente útil podría ser:

```text
Dos actores interactúan dos veces.

La segunda interacción debe depender causalmente de la primera.
```

Eso inmediatamente obliga a descubrir qué necesitamos realmente para:

- percepción;
- experiencia;
- relación;
- memoria/continuidad;
- transformación;
- futura decisión.

No porque esas seis palabras tengan que convertirse en seis interfaces.

Sino porque el experimento nos puede mostrar cuáles son cosas distintas de verdad.

---

## 13. Ruta de lectura cuando quieras profundizar

Después de este documento:

1. `core-foundation-review-v0.1.md`
2. `conceptual-model.md`
3. `implementation-plan.md`
4. los pressure tests de fase 1 y 2
5. `development-boundaries.md`
6. `evolution.md`
7. tests de `Concept.Core.Tests`

Los tests son importantes porque muestran no sólo qué debería funcionar, sino **qué presiones ya usamos para matar abstracciones anteriores**.

---

## 14. La frase que quiero que sobreviva

Si en algún momento el repo se vuelve enorme y perdemos la brújula, volver acá:

> **Concept no debería inventar qué significa ser un actor. Debería descubrir qué estructuras causales siguen apareciendo cuando distintos actores y sistemas intentan existir, cambiar y relacionarse.**
