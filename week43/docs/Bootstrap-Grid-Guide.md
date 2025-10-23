# Bootstrap Grid System - Komplett Guide

Denne guiden forklarer **Bootstrap's Grid System** med fokus på `row`, `col-*` og spacing-klasser som brukes i CounterPlus-komponenten.  
Bootstrap Grid lar deg lage **responsive layouts** med rader og kolonner som automatisk tilpasser seg forskjellige skjermstørrelser.

---

## 🎯 1. Grunnleggende konsepter

### Container → Row → Column hierarki

```html
<div class="container">          <!-- Wrapper for hele layouten -->
    <div class="row">             <!-- Horisontal rad -->
        <div class="col">         <!-- Kolonne inni raden -->
            Innhold her
        </div>
    </div>
</div>
```

🔹 **Hovedregler:**
- **Container** → Gir margin og max-width
- **Row** → Lager horisontal gruppe med kolonner  
- **Column** → Innholdet plasseres i kolonner
- **12-kolonne system** → Hver rad kan deles opp i maksimalt 12 kolonner

<div style="page-break-after:always;"></div>

## 📏 2. Kolonne-systemet (12-grid)

### Grunnleggende kolonner

```html
<!-- Full bredde (12/12) -->
<div class="row">
    <div class="col-12">Full bredde</div>
</div>

<!-- Halv bredde (6/12 + 6/12 = 12) -->
<div class="row">
    <div class="col-6">Halv</div>
    <div class="col-6">Halv</div>
</div>

<!-- Tredjedeler (4/12 + 4/12 + 4/12 = 12) -->
<div class="row">
    <div class="col-4">Tredjedel</div>
    <div class="col-4">Tredjedel</div>
    <div class="col-4">Tredjedel</div>
</div>

<!-- Automatisk størrelse -->
<div class="row">
    <div class="col">Auto</div>
    <div class="col">Auto</div>
    <div class="col">Auto</div>
</div>
```

### Som i din CounterPlus:

```html
<div class="row gy-3">
    <div class="col-md-6">Første kort</div>    <!-- 50% på medium+ skjermer -->
    <div class="col-md-6">Andre kort</div>     <!-- 50% på medium+ skjermer -->
    <div class="col-md-6">Tredje kort</div>    <!-- 50% på medium+ skjermer -->
    <div class="col-md-6">Fjerde kort</div>    <!-- 50% på medium+ skjermer -->
</div>
```

🔹 **Resultat:** 
- På **medium skjermer og oppover** (≥768px): 2 kolonner side ved side
- På **små skjermer** (<768px): 1 kolonne under hverandre

<div style="page-break-after:always;"></div>

## 📱 3. Responsive breakpoints

Bootstrap har **5 hovedstørrelser** med tilhørende prefiks:

| Størrelse | Breakpoint | Prefix | Beskrivelse |
|-----------|------------|--------|-------------|
| **Extra Small** | <576px | (ingen) | Mobil portrait |
| **Small** | ≥576px | `sm` | Mobil landscape |
| **Medium** | ≥768px | `md` | Tablet |
| **Large** | ≥992px | `lg` | Desktop |
| **Extra Large** | ≥1200px | `xl` | Store skjermer |

### Praktiske eksempler:

```html
<!-- Forskjellig layout på forskjellige skjermer -->
<div class="row">
    <div class="col-12 col-sm-6 col-md-4 col-lg-3">
        <!-- Mobil: 100% bred (12/12) -->
        <!-- Small+: 50% bred (6/12) -->  
        <!-- Medium+: 33% bred (4/12) -->
        <!-- Large+: 25% bred (3/12) -->
    </div>
</div>

<!-- Som i din CounterPlus -->
<div class="col-md-6">
    <!-- Under 768px: 100% bred (stables vertikalt) -->
    <!-- Over 768px: 50% bred (2 kolonner side ved side) -->
</div>
```

<div style="page-break-after:always;"></div>

## 🎨 4. Spacing - `gy-3` og andre spacing-klasser

### Gutter spacing (mellomrom mellom kolonner)

```html
<!-- Vertikal spacing mellom rader -->
<div class="row gy-3">     <!-- 3 enheter spacing vertikalt -->
    <div class="col-md-6">Kort 1</div>
    <div class="col-md-6">Kort 2</div>
    <div class="col-md-6">Kort 3</div>    <!-- Vil ha 3 enheter margin-top -->
    <div class="col-md-6">Kort 4</div>
</div>

<!-- Horisontal spacing mellom kolonner -->
<div class="row gx-3">     <!-- 3 enheter spacing horisontalt -->
    <div class="col-6">Kort 1</div>      <!-- Mellomrom til høyre -->
    <div class="col-6">Kort 2</div>      <!-- Mellomrom til venstre -->
</div>

<!-- Både horisontal og vertikal -->
<div class="row g-3">      <!-- 3 enheter spacing i alle retninger -->
    <div class="col-md-6">Kort 1</div>
    <div class="col-md-6">Kort 2</div>
</div>
```

### Spacing-nivåer (0-5):

| Klasse | Spacing | Pixels (ca.) | Bruksområde |
|--------|---------|--------------|-------------|
| `g-0` | 0 | 0px | Ingen spacing |
| `g-1` | 0.25rem | 4px | Minimal spacing |
| `g-2` | 0.5rem | 8px | Liten spacing |
| `g-3` | 1rem | 16px | **Standard spacing** |
| `g-4` | 1.5rem | 24px | Medium spacing |
| `g-5` | 3rem | 48px | Stor spacing |

🔹 **I din CounterPlus:** `gy-3` gir **16px vertikal spacing** mellom kortene når de stables.

<div style="page-break-after:always;"></div>

## 🃏 5. Praktisk eksempel - Analysering av CounterPlus

La oss analysere din kode steg for steg:

```html
<div class="row gy-3">
    <!-- Første kort -->
    <div class="col-md-6">
        <div class="card">
            <div class="card-header">One-way binding</div>
            <div class="card-body">
                <!-- Kort innhold -->
            </div>
        </div>
    </div>
    
    <!-- Andre kort -->
    <div class="col-md-6">
        <div class="card">
            <div class="card-header">Two-way binding</div>
            <div class="card-body">
                <!-- Kort innhold -->
            </div>
        </div>
    </div>
    
    <!-- Tredje kort -->
    <div class="col-md-6">
        <!-- ... -->
    </div>
    
    <!-- Fjerde kort -->
    <div class="col-md-6">
        <!-- ... -->
    </div>
</div>
```

<div style="page-break-after:always;"></div>

### Hva skjer her?

1. **`row gy-3`** → Lager en rad med 16px vertikal spacing
2. **`col-md-6`** → Hver kolonne tar 50% av bredden på medium+ skjermer
3. **Responsivt oppførsel:**
   - **Desktop/Tablet (≥768px):** 2 kolonner × 2 rader = 4 kort i 2×2 grid
   - **Mobil (<768px):** 1 kolonne × 4 rader = 4 kort stablet vertikalt

### Visuell representasjon:

```
Desktop (≥768px):
┌─────────────┬─────────────┐
│   Kort 1    │   Kort 2    │
├─────────────┼─────────────┤  ← gy-3 spacing
│   Kort 3    │   Kort 4    │
└─────────────┴─────────────┘

Mobil (<768px):
┌─────────────────────────────┐
│           Kort 1            │
├─────────────────────────────┤  ← gy-3 spacing
│           Kort 2            │
├─────────────────────────────┤  ← gy-3 spacing
│           Kort 3            │
├─────────────────────────────┤  ← gy-3 spacing
│           Kort 4            │
└─────────────────────────────┘
```

<div style="page-break-after:always;"></div>

## 🛠️ 6. Vanlige kolonne-kombinasjoner

### Symmetriske layouts:

```html
<!-- 2 kolonner (50% / 50%) -->
<div class="row">
    <div class="col-md-6">Venstre</div>
    <div class="col-md-6">Høyre</div>
</div>

<!-- 3 kolonner (33% / 33% / 33%) -->
<div class="row">
    <div class="col-md-4">Venstre</div>
    <div class="col-md-4">Midten</div>
    <div class="col-md-4">Høyre</div>
</div>

<!-- 4 kolonner (25% / 25% / 25% / 25%) -->
<div class="row">
    <div class="col-md-3">1</div>
    <div class="col-md-3">2</div>
    <div class="col-md-3">3</div>
    <div class="col-md-3">4</div>
</div>
```

### Asymmetriske layouts:

```html
<!-- Sidebar + main (25% / 75%) -->
<div class="row">
    <div class="col-md-3">Sidebar</div>
    <div class="col-md-9">Main content</div>
</div>

<!-- Content + sidebar (66% / 33%) -->
<div class="row">
    <div class="col-md-8">Content</div>
    <div class="col-md-4">Sidebar</div>
</div>
```

<div style="page-break-after:always;"></div>

## 📐 7. Avanserte grid-teknikker

### Offset (flytt kolonner til høyre):

```html
<div class="row">
    <div class="col-md-4 offset-md-2">
        <!-- Starter på kolonne 3 (offset 2), tar 4 kolonner -->
        <!-- Resulterer i sentrert kolonne -->
    </div>
</div>
```

### Order (endre rekkefølge):

```html
<div class="row">
    <div class="col-md-6 order-md-2">Vises til høyre på desktop</div>
    <div class="col-md-6 order-md-1">Vises til venstre på desktop</div>
</div>
```

### Nested grids (grid inni grid):

```html
<div class="row">
    <div class="col-md-6">
        <div class="row">
            <div class="col-6">Nested 1</div>
            <div class="col-6">Nested 2</div>
        </div>
    </div>
    <div class="col-md-6">Regular column</div>
</div>
```

<div style="page-break-after:always;"></div>

## 🎯 8. Best Practices

### ✅ Gjør dette:

```html
<!-- Bruk container for å wrap grid -->
<div class="container">
    <div class="row">
        <div class="col-md-6">Content</div>
    </div>
</div>

<!-- Bruk spacing-klasser for mellomrom -->
<div class="row g-3">
    <div class="col-md-6">Card 1</div>
    <div class="col-md-6">Card 2</div>
</div>

<!-- Kombiner breakpoints for optimal responsivitet -->
<div class="col-12 col-sm-6 col-lg-4">
    Responsive content
</div>
```

### ❌ Unngå dette:

```html
<!-- Ikke sett kolonner direkte inni hverandre -->
<div class="col-md-6">
    <div class="col-md-6">Wrong nesting</div>
</div>

<!-- Ikke overstiger 12 kolonner per rad -->
<div class="row">
    <div class="col-8">8</div>
    <div class="col-6">6</div>  <!-- 8 + 6 = 14 > 12! -->
</div>

<!-- Ikke bruk grid for alt - bruk flexbox utilities når passende -->
<div class="d-flex gap-3">  <!-- Bedre for horisontale knapper -->
    <button class="btn btn-primary">Button 1</button>
    <button class="btn btn-secondary">Button 2</button>
</div>
```

<div style="page-break-after:always;"></div>

## 📊 9. Cheat Sheet - Rask referanse

### Kolonne-størrelser:

| Klasse | Bredde | Bruk |
|--------|--------|------|
| `col-1` | 8.33% | Veldig smal |
| `col-2` | 16.67% | Smal |
| `col-3` | 25% | Kvart |
| `col-4` | 33.33% | Tredjedel |
| `col-6` | 50% | **Halv (som i CounterPlus)** |
| `col-8` | 66.67% | To tredjedeler |
| `col-9` | 75% | Tre firedeler |
| `col-12` | 100% | Full bredde |

### Spacing shortcuts:

| Klasse | Retning | Bruk |
|--------|---------|------|
| `g-*` | Alle retninger | Generell spacing |
| `gx-*` | Horisontal | Mellom kolonner |
| `gy-*` | Vertikal | **Mellom rader (som i CounterPlus)** |
| `g-0` | Ingen | Fjern all spacing |

### Breakpoint prefiks:

| Prefix | Skjermstørrelse | Eksempel |
|--------|----------------|----------|
| (ingen) | Alle størrelser | `col-6` |
| `sm-` | ≥576px | `col-sm-6` |
| `md-` | ≥768px | **`col-md-6` (som i CounterPlus)** |
| `lg-` | ≥992px | `col-lg-6` |
| `xl-` | ≥1200px | `col-xl-6` |

<div style="page-break-after:always;"></div>

## ✅ 10. Oppsummering - CounterPlus eksempel

Din CounterPlus-komponent bruker **Bootstrap Grid** på en elegant måte:

```html
<div class="row gy-3">          <!-- ✅ Rad med vertikal spacing -->
    <div class="col-md-6">      <!-- ✅ 50% bredde på tablet+ -->
        <div class="card">      <!-- ✅ Bootstrap card inni kolonnen -->
```

**Hva du kan forvente:**

- 🖥️ **Desktop/Tablet:** 2×2 grid med kort side ved side
- 📱 **Mobil:** 4 kort stablet vertikalt
- 📏 **Spacing:** 16px mellomrom mellom rader når de stables
- 🎨 **Responsivt:** Automatisk tilpasning uten media queries

**Hvorfor dette fungerer så bra:**
- **Enkelt** - Bare 2 klasser (`row gy-3` og `col-md-6`)
- **Fleksibelt** - Fungerer på alle skjermstørrelser  
- **Konsistent** - Samme spacing og layout-prinsipper
- **Vedlikeholdbart** - Bootstrap håndterer all responsive logikk

👉 **Ønsker du andre layouts?** Endre bare `col-md-6` til f.eks:
- `col-md-4` → 3 kolonner (33% hver)
- `col-md-3` → 4 kolonner (25% hver)  
- `col-lg-4 col-md-6` → 3 på desktop, 2 på tablet, 1 på mobil

---