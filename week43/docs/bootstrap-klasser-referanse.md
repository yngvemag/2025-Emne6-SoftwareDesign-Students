# 🎨 Bootstrap CSS-klasser Referanse - BlazorPlayGround

## 📋 Oversikt

Denne dokumentasjonen forklarer alle Bootstrap-klasser brukt i BlazorPlayGround Components-mappen, organisert etter HTML-tags og funksjonsområder.

---

## 🏗️ Layout og Grid System

### `<div>` Container og Grid-klasser

| Bootstrap Klasse | Forklaring | Bruksområde | Kan brukes på |
|------------------|------------|-------------|---------------|
| `container-fluid` | 100% bredde container som dekker hele viewport-bredden | Layout wrapper som strekker seg over hele skjermen | `<div>`, `<main>`, `<section>` |
| `row` | Bootstrap grid row-container | Wrapper for kolonner i grid-systemet | `<div>` |
| `row g-0` | Row uten gutters (mellomrom mellom kolonner) | Når du vil ha kolonner uten mellomrom | `<div>` |
| `row gy-3` | Row med vertikal gutter på 1rem (16px) | Legger til vertikal avstand mellom grid-rader | `<div>` |
| `col-md-6` | 50% bredde (6/12 kolonner) på medium skjermer og større | Standard responsive kolonnebredde | `<div>` |
| `col-md-3` | 25% bredde (3/12 kolonner) på medium skjermer | Sidebar eller smal kolonne | `<div>` |
| `col-lg-2` | ~17% bredde (2/12 kolonner) på large skjermer | Enda smalere sidebar på store skjermer | `<div>` |
| `col-md-9` | 75% bredde (9/12 kolonner) på medium skjermer | Hovedinnholdsområde | `<div>` |
| `col-lg-10` | ~83% bredde (10/12 kolonner) på large skjermer | Bredere hovedinnhold på store skjermer | `<div>` |

<div style="page-break-after:always;"></div>

### 📱 Responsive Breakpoints

```css
/* Bootstrap responsive sizes */
sm: 576px+    /* Small devices (phones) */
md: 768px+    /* Medium devices (tablets) */
lg: 992px+    /* Large devices (laptops) */
xl: 1200px+   /* Extra large devices (desktops) */
xxl: 1400px+  /* Extra extra large devices */
```

<div style="page-break-after:always;"></div>

## 🎨 Spacing System

### Margin klasser (`m-*`)

| Bootstrap Klasse | CSS Ekvivalent | Forklaring | Eksempel bruk |
|------------------|----------------|------------|---------------|
| `mb-0` | `margin-bottom: 0` | Fjerner bottom margin | Overskrifter uten avstand under |
| `mb-1` | `margin-bottom: 0.25rem` | Liten margin under (4px) | Tett spacing mellom elementer |
| `mb-2` | `margin-bottom: 0.5rem` | Medium margin under (8px) | Standard avstand mellom elementer |
| `mb-3` | `margin-bottom: 1rem` | Stor margin under (16px) | Skille mellom seksjoner |
| `mt-1` | `margin-top: 0.25rem` | Liten margin over (4px) | Subtil avstand over element |
| `mt-3` | `margin-top: 1rem` | Stor margin over (16px) | Skille over seksjoner |
| `ms-auto` | `margin-left: auto` | Skyver element til høyre i flex/grid | Høyre-justerte knapper |

### Padding klasser (`p-*`)

| Bootstrap Klasse | CSS Ekvivalent | Forklaring | Kan brukes på |
|------------------|----------------|------------|---------------|
| `p-0` | `padding: 0` | Fjerner all padding | `<div>`, `<section>`, `<main>` |
| `p-2` | `padding: 0.5rem` | 8px padding på alle sider | Små kort, knapper |
| `p-3` | `padding: 1rem` | 16px padding på alle sider | Standard innholdsbokser |
| `p-4` | `padding: 1.5rem` | 24px padding på alle sider | Hovedinnholdsområder |

<div style="page-break-after:always;"></div>

## 🎭 Flexbox og Layout

### Flexbox-klasser for `<div>`

| Bootstrap Klasse | CSS Ekvivalent | Forklaring | Bruksscenario |
|------------------|----------------|------------|---------------|
| `d-flex` | `display: flex` | Gjør element til flex-container | Plassere elementer horisontalt |
| `flex-column` | `flex-direction: column` | Flex-elementer stables vertikalt | Vertikale layouter (sidebar) |
| `flex-grow-1` | `flex-grow: 1` | Element vokser til å fylle tilgjengelig plass | Hovedinnhold som skal fylles |
| `justify-content-between` | `justify-content: space-between` | Elementer fordeles med maks avstand | Knapper i endene av en rad |
| `align-items-center` | `align-items: center` | Sentrerer elementer vertikalt | Elementer på samme linje |
| `gap-2` | `gap: 0.5rem` | 8px mellomrom mellom flex-elementer | Knapper eller ikoner med spacing |
| `flex-wrap` | `flex-wrap: wrap` | Tillater flex-elementer å bryte til ny linje | Responsive button-grupper |

---

## 🎨 Farger og Bakgrunner

### Bakgrunnsfarger

| Bootstrap Klasse | Farge | Bruksområde | Kan brukes på |
|------------------|-------|-------------|---------------|
| `bg-primary` | Blå (#0d6efd) | Hovedfarger, call-to-action | `<div>`, `<header>`, `<button>` |
| `bg-light` | Lys grå (#f8f9fa) | Hovedinnhold, lys bakgrunn | `<div>`, `<main>`, `<section>` |
| `bg-dark` | Mørk grå/svart (#212529) | Sidebar, header, footer | `<div>`, `<nav>`, `<aside>` |
| `bg-success` | Grønn (#198754) | Suksessmeldinger, positive handlinger | `<div>`, `<alert>`, `<button>` |
| `bg-warning` | Gul (#ffc107) | Advarsler, oppmerksomhet | `<div>`, `<alert>`, `<badge>` |
| `bg-info` | Turkis (#0dcaf0) | Informasjon, tips | `<div>`, `<alert>` |
| `bg-danger` | Rød (#dc3545) | Feil, sletting, farlige handlinger | `<div>`, `<button>` |

### Tekstfarger

| Bootstrap Klasse | Farge | Bruksområde |
|------------------|-------|-------------|
| `text-white` | Hvit | Tekst på mørke bakgrunner |
| `text-muted` | Dempet grå | Hjelpetekst, sekundær informasjon |
| `text-primary` | Blå | Lenker, viktig informasjon |
| `text-success` | Grønn | Suksessmeldinger |
| `text-danger` | Rød | Feilmeldinger |

<div style="page-break-after:always;"></div>

## 🃏 Komponenter

### Card-komponenter

| Bootstrap Klasse | Forklaring | Brukes på | Eksempel |
|------------------|------------|-----------|----------|
| `card` | Hovedcontainer for kort | `<div>` | Innholdsbokser, widgets |
| `card-header` | Header-seksjon av kort | `<div>` | Tittel eller metadata |
| `card-body` | Hovedinnhold i kort | `<div>` | Tekst, knapper, innhold |
| `card-footer` | Bunnseksjon av kort | `<div>` | Handlingsknapper, metadata |

### Knapper (`<button>`)

| Bootstrap Klasse | Stil | Bruksområde |
|------------------|------|-------------|
| `btn` | Grunnleggende button-styling | Alle knapper (PÅKREVD) |
| `btn-primary` | Blå, filled knapp | Hovedhandlinger |
| `btn-secondary` | Grå, filled knapp | Sekundære handlinger |
| `btn-success` | Grønn, filled knapp | Positive handlinger (lagre, ok) |
| `btn-danger` | Rød, filled knapp | Farlige handlinger (slett) |
| `btn-warning` | Gul, filled knapp | Advarselshandlinger |
| `btn-info` | Turkis, filled knapp | Informasjonshandlinger |
| `btn-outline-primary` | Blå, outline knapp | Sekundære hovedhandlinger |
| `btn-outline-secondary` | Grå, outline knapp | Mindre viktige handlinger |
| `btn-outline-danger` | Rød, outline knapp | Mindre farlige handlinger |
| `btn-sm` | Liten knapp | Kompakte grensesnitt |
| `btn-lg` | Stor knapp | Viktige call-to-action |

### Forms og Input

| HTML Tag | Bootstrap Klasse | Forklaring | Bruksområde |
|----------|------------------|------------|-------------|
| `<input>` | `form-control` | Standard input-styling | Tekstfelt, nummer, dato |
| `<input>` | `form-control-sm` | Liten input | Kompakte skjemaer |
| `<input>` | `form-control-lg` | Stor input | Fremhevede inputfelt |
| `<label>` | `form-label` | Standard label-styling | Alle skjemafelt-labels |
| `<select>` | `form-select` | Dropdown-styling | Valgmenyer |
| `<textarea>` | `form-control` | Tekstområde-styling | Lange tekstinput |
| `<div>` | `form-group` | Grupperer label + input | Organisering av skjemafelt |
| `<div>` | `input-group` | Grupperer input med tillegg | Input med knapper/ikoner |
| `<span>` | `input-group-text` | Tekst/ikoner ved siden av input | Prefikser, suffixer |

### Navigasjon

| HTML Tag | Bootstrap Klasse | Forklaring | Bruksområde |
|----------|------------------|------------|-------------|
| `<nav>` | `nav` | Navigasjonscontainer | Hovednavigasjon, menyer |
| `<ul>` | `nav nav-pills` | Pill-style navigasjon | Vertikal/horisontal meny |
| `<ul>` | `nav flex-column` | Vertikal navigasjon | Sidemenyer |
| `<li>` | `nav-item` | Navigasjonselement | Hvert menyelement |
| `<a>` | `nav-link` | Navigasjonslenke | Lenker i menyer |

### Alerts og Meldinger

| Bootstrap Klasse | Stil | Bruksområde |
|------------------|------|-------------|
| `alert` | Grunnleggende alert-styling | Alle meldingsbokser |
| `alert-primary` | Blå informasjonsboks | Generell informasjon |
| `alert-success` | Grønn suksessboks | Suksessmeldinger |
| `alert-warning` | Gul advarselsboks | Advarsler |
| `alert-danger` | Rød feilboks | Feilmeldinger |
| `alert-info` | Turkis infoboks | Tips og hjelpeinformasjon |

### Badges og Labels

| Bootstrap Klasse | Stil | Bruksområde |
|------------------|------|-------------|
| `badge` | Grunnleggende badge-styling | Alle badges |
| `bg-primary` (på badge) | Blå badge | Statusindikatorer |
| `bg-success` (på badge) | Grønn badge | Positive indikatorer |
| `bg-warning` (på badge) | Gul badge | Advarsler |

<div style="page-break-after:always;"></div>

## 📐 Utilities

### Høyde og Bredde

| Bootstrap Klasse | CSS Ekvivalent | Forklaring |
|------------------|----------------|------------|
| `min-vh-100` | `min-height: 100vh` | Minimum høyde = full viewport |
| `w-100` | `width: 100%` | Full bredde av foreldrecontainer |
| `h-100` | `height: 100%` | Full høyde av foreldrecontainer |

### Tekst og Typografi

| Bootstrap Klasse | CSS Ekvivalent | Forklaring | Brukes på |
|------------------|----------------|------------|-----------|
| `lead` | Større tekst | Fremhever viktig tekst | `<p>` |
| `small` | Mindre tekst | Hjelpetekst, metadata | `<small>`, `<span>` |
| `text-uppercase` | `text-transform: uppercase` | Store bokstaver | Alle tekstelementer |
| `fw-bold` | `font-weight: bold` | Fet tekst | `<span>`, `<strong>` |
| `fs-6` | `font-size: 1rem` | Font størrelse 6 (standard) | Alle tekstelementer |

### Border og Rounding

| Bootstrap Klasse | CSS Ekvivalent | Forklaring |
|------------------|----------------|------------|
| `border` | `border: 1px solid #dee2e6` | Standard grå ramme |
| `rounded` | `border-radius: 0.375rem` | Avrundede hjørner |
| `rounded-circle` | `border-radius: 50%` | Sirkelform |

### List Styling

| Bootstrap Klasse | CSS Ekvivalent | Forklaring | Brukes på |
|------------------|----------------|------------|-----------|
| `list-unstyled` | `list-style: none` | Fjerner bullets/nummerering | `<ul>`, `<ol>` |

<div style="page-break-after:always;"></div>

## 📊 Praktiske Eksempler

### Eksempel 1: Form Layout (fra Hello2.razor)

```html
<!-- Standard skjemafelt med Bootstrap -->
<div class="mb-3">                           <!-- Margin bottom for spacing -->
    <label for="nameInput" class="form-label">Navn:</label>  <!-- Bootstrap label -->
    <input type="text" 
           id="nameInput" 
           class="form-control"              <!-- Bootstrap input styling -->
           @oninput="OnNameChanged" 
           value="@inputName" 
           placeholder="Skriv inn navn..." />
</div>
```

### Eksempel 2: Card Layout (fra CounterPlus.razor)

```html
<div class="col-md-6">                       <!-- 50% bredde på medium+ skjermer -->
    <div class="card">                       <!-- Bootstrap card container -->
        <div class="card-header">Two-way binding</div>  <!-- Card header -->
        <div class="card-body">              <!-- Card body -->
            <p class="text-muted">Endringer i input oppdaterer <code>Count</code> direkte.</p>
            <InputNumber @bind-Value="Count" class="form-control" />
            <small class="text-muted">Prøv å endre tallet her.</small>
        </div>
    </div>
</div>
```

### Eksempel 3: Button Group (fra CounterPlus.razor)

```html
<div class="card-body d-flex gap-2 flex-wrap">  <!-- Flex container med gap -->
    <button class="btn btn-primary" @onclick="Increment">+ @Step</button>
    <button class="btn btn-outline-primary" @onclick="() => IncrementBy(Step)">+ (lambda)</button>
    <button class="btn btn-secondary" @onclick="Decrement">- @Step</button>
    <button class="btn btn-outline-danger ms-auto" @onclick="Reset">Reset</button>
</div>
```
<div style="page-break-after:always;"></div>

### Eksempel 4: Responsive Grid (fra MainLayout.razor)

```html
<div class="container-fluid p-0">           <!-- Full bredde, ingen padding -->
    <div class="row g-0 min-vh-100">        <!-- Row uten gutters, min 100vh høyde -->
        <!-- Sidebar -->
        <div class="col-md-3 col-lg-2 bg-dark">   <!-- 25% på md, 17% på lg -->
            <nav class="flex-grow-1 p-3">          <!-- Fyller tilgjengelig plass -->
                <ul class="nav nav-pills flex-column">  <!-- Vertikal navigasjon -->
                    <li class="nav-item mb-1">
                        <a href="/" class="nav-link text-white">🏠 Hjem</a>
                    </li>
                </ul>
            </nav>
        </div>
        
        <!-- Hovedinnhold -->
        <div class="col-md-9 col-lg-10 bg-light">  <!-- 75% på md, 83% på lg -->
            <main class="p-4">                     <!-- 24px padding -->
                @Body
            </main>
        </div>
    </div>
</div>
```

<div style="page-break-after:always;"></div>

## 🎯 Best Practices

### ✅ Anbefalte kombinasjoner

```html
<!-- Standard skjemafelt -->
<div class="mb-3">
    <label class="form-label">Label</label>
    <input class="form-control" />
</div>

<!-- Knapp-grupper -->
<div class="d-flex gap-2 flex-wrap">
    <button class="btn btn-primary">Primær</button>
    <button class="btn btn-outline-secondary">Sekundær</button>
</div>

<!-- Responsive grid -->
<div class="row gy-3">
    <div class="col-md-6">
        <div class="card">
            <div class="card-body">Innhold</div>
        </div>
    </div>
</div>
```

### ⚠️ Vanlige feil å unngå

- ❌ `<button class="btn-primary">` - Glemmer `btn` base-klasse
- ✅ `<button class="btn btn-primary">` - Korrekt

- ❌ `<input class="form-control-sm">` - Glemmer `form-control` base-klasse  
- ✅ `<input class="form-control form-control-sm">` - Korrekt

- ❌ `<div class="col-6 col-md-4">` - Ikke responsive (bruker alltid col-6)
- ✅ `<div class="col-12 col-md-6 col-lg-4">` - Responsive (mobile-first)


