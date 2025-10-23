# 🎯 CSS Flexbox og Grid - Komplett Guide

## 📋 Oversikt


## 🧩 CSS Flexbox - Flexible Box Layout

### 💡 Hva er Flexbox?

Flexbox er et layout-system som gjør det enkelt å distribuere plass og justere elementer i en container, selv når størrelsen på elementene er ukjent eller dynamisk.

### 🏗️ Grunnleggende Konsepter

```css
/* Flexbox Terminologi */
.flex-container {
  display: flex;           /* Hovedaksen (main axis) */
}                         /* Kryssaksen (cross axis) */

/* 
Main Axis: Hovedretning (horizontal som standard)
Cross Axis: Vinkelrett på hovedaksen (vertikal som standard)
Flex Container: Foreldrelet med display: flex
Flex Items: Barneelementer i flex containeren
*/
```

### 📐 Flex Container Egenskaper

#### **1. display: flex**

```html
<!-- Standard flex container -->
<div class="flex-container">
  <div>Element 1</div>
  <div>Element 2</div>
  <div>Element 3</div>
</div>
```

```css
.flex-container {
  display: flex;           /* Block-level flex container */
  /* ELLER */
  display: inline-flex;    /* Inline-level flex container */
}
```
<div style="page-break-after:always;"></div>

#### **2. flex-direction - Retning på hovedaksen**

| CSS Verdi | Bootstrap Klasse | Forklaring | Visuell Layout |
|-----------|------------------|------------|----------------|
| `row` | `flex-row` | Horisontalt, venstre til høyre | `[1][2][3]` |
| `row-reverse` | `flex-row-reverse` | Horisontalt, høyre til venstre | `[3][2][1]` |
| `column` | `flex-column` | Vertikalt, topp til bunn | `[1]`<br>`[2]`<br>`[3]` |
| `column-reverse` | `flex-column-reverse` | Vertikalt, bunn til topp | `[3]`<br>`[2]`<br>`[1]` |

```html
<!-- Bootstrap Flexbox Direction -->
<div class="d-flex flex-column">        <!-- Vertikal stabling -->
  <div>Øverst</div>
  <div>I midten</div>
  <div>Nederst</div>
</div>

<div class="d-flex flex-row">           <!-- Horisontal rekkefølge -->
  <div>Venstre</div>
  <div>Midten</div>
  <div>Høyre</div>
</div>
```
<div style="page-break-after:always;"></div>

#### **3. justify-content - Justering langs hovedaksen**

| CSS Verdi | Bootstrap Klasse | Forklaring | Layout |
|-----------|------------------|------------|--------|
| `flex-start` | `justify-content-start` | Elementer til starten | `[1][2][3]     ` |
| `flex-end` | `justify-content-end` | Elementer til slutten | `     [1][2][3]` |
| `center` | `justify-content-center` | Elementer i midten | `  [1][2][3]   ` |
| `space-between` | `justify-content-between` | Maks avstand mellom | `[1]    [2]    [3]` |
| `space-around` | `justify-content-around` | Lik plass rundt alle | ` [1]  [2]  [3] ` |
| `space-evenly` | `justify-content-evenly` | Perfekt lik avstand | `  [1]  [2]  [3]  ` |

```html
<!-- Bootstrap Justify Content Eksempler -->
<div class="d-flex justify-content-between">
  <button class="btn btn-primary">Venstre</button>
  <button class="btn btn-success">Høyre</button>
</div>

<div class="d-flex justify-content-center">
  <div class="card">Sentrert kort</div>
</div>

<div class="d-flex justify-content-evenly">
  <span>1</span>
  <span>2</span>
  <span>3</span>
</div>
```
<div style="page-break-after:always;"></div>

#### **4. align-items - Justering langs kryssaksen**

| CSS Verdi | Bootstrap Klasse | Forklaring | Layout (vertikal) |
|-----------|------------------|------------|-------------------|
| `stretch` | `align-items-stretch` | Strekk til full høyde | `[====]`<br>`[====]` |
| `flex-start` | `align-items-start` | Til toppen | `[1]`<br>`   ` |
| `flex-end` | `align-items-end` | Til bunnen | `   `<br>`[1]` |
| `center` | `align-items-center` | Vertikal sentrering | `   `<br>`[1]`<br>`   ` |
| `baseline` | `align-items-baseline` | Til tekstbasislinje | `[A] [g]` |

```html
<!-- Bootstrap Align Items -->
<div class="d-flex align-items-center" style="height: 200px;">
  <div>Vertikal sentrert innhold</div>
</div>

<div class="d-flex align-items-start">
  <div class="p-2 bg-primary">Kort</div>
  <div class="p-4 bg-success">Høyere element</div>
</div>
```
<div style="page-break-after:always;"></div>

#### **5. flex-wrap - Linjebryting**

| CSS Verdi | Bootstrap Klasse | Forklaring |
|-----------|------------------|------------|
| `nowrap` | `flex-nowrap` | Ingen linjebryting (standard) |
| `wrap` | `flex-wrap` | Bryt til neste linje ved behov |
| `wrap-reverse` | `flex-wrap-reverse` | Bryt til forrige linje |

```html
<!-- Bootstrap Flex Wrap -->
<div class="d-flex flex-wrap">
  <div class="p-2 m-1 bg-info">Element 1</div>
  <div class="p-2 m-1 bg-info">Element 2</div>
  <div class="p-2 m-1 bg-info">Element 3</div>
  <div class="p-2 m-1 bg-info">Element 4</div>
  <!-- Bryter til ny linje når det blir for smalt -->
</div>
```
<div style="page-break-after:always;"></div>

#### **6. gap - Avstand mellom elementer**

```html
<!-- Bootstrap Gap -->
<div class="d-flex gap-1">        <!-- 4px gap -->
  <button class="btn btn-primary">Knapp 1</button>
  <button class="btn btn-secondary">Knapp 2</button>
</div>

<div class="d-flex gap-3">        <!-- 16px gap -->
  <div class="card">Kort 1</div>
  <div class="card">Kort 2</div>
</div>
```
<div style="page-break-after:always;"></div>

### 🎛️ Flex Item Egenskaper

#### **1. flex-grow - Vekstfaktor**

```css
.flex-item {
  flex-grow: 0;  /* Voks ikke (standard) */
  flex-grow: 1;  /* Voks likt med andre flex-grow: 1 elementer */
  flex-grow: 2;  /* Voks dobbelt så mye som flex-grow: 1 elementer */
}
```

```html
<!-- Bootstrap Flex Grow -->
<div class="d-flex">
  <div class="p-2 bg-primary">Fast bredde</div>
  <div class="p-2 bg-success flex-grow-1">Voksende innhold</div>
  <div class="p-2 bg-warning">Fast bredde</div>
</div>
```

#### **2. flex-shrink - Krympefaktor**

```css
.flex-item {
  flex-shrink: 1;  /* Krymp normalt (standard) */
  flex-shrink: 0;  /* Krymp aldri */
  flex-shrink: 2;  /* Krymp dobbelt så mye */
}
```

#### **3. align-self - Individuell justering**

```html
<!-- Bootstrap Align Self -->
<div class="d-flex align-items-start" style="height: 150px;">
  <div class="p-2 bg-primary">Standard</div>
  <div class="p-2 bg-success align-self-center">Sentrert</div>
  <div class="p-2 bg-warning align-self-end">Til bunnen</div>
</div>
```

<div style="page-break-after:always;"></div>

## 🏗️ CSS Grid - Grid Layout System

### 💡 Hva er CSS Grid?

CSS Grid er et todimensjonalt layout-system som lar deg lage komplekse layouts med rader og kolonner samtidig.

### 🧩 Grunnleggende Grid Konsepter

```css
/* Grid Terminologi */
.grid-container {
  display: grid;
  /* Grid Lines: Linjene som deler opp gridet */
  /* Grid Tracks: Radene og kolonnene */
  /* Grid Cells: Enkelt celle (som en celle i Excel) */
  /* Grid Areas: Rektangulære områder (flere celler) */
}
```

### 📐 Grid Container Egenskaper

#### **1. display: grid**

```html
<!-- Standard grid container -->
<div class="grid-container">
  <div>Element 1</div>
  <div>Element 2</div>
  <div>Element 3</div>
  <div>Element 4</div>
</div>
```

```css
.grid-container {
  display: grid;           /* Block-level grid */
  /* ELLER */
  display: inline-grid;    /* Inline-level grid */
}
```
<div style="page-break-after:always;"></div>

#### **2. grid-template-columns - Kolonne-struktur**

```css
.grid-container {
  /* Fast bredde */
  grid-template-columns: 200px 300px 100px;
  
  /* Prosent-basert */
  grid-template-columns: 25% 50% 25%;
  
  /* Flexible enheter (fr = fraction) */
  grid-template-columns: 1fr 2fr 1fr;  /* 1:2:1 forhold */
  
  /* Blandet */
  grid-template-columns: 200px 1fr auto;
  
  /* Repeterende mønstre */
  grid-template-columns: repeat(3, 1fr);        /* 3 like kolonner */
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));  /* Responsive */
}
```
<div style="page-break-after:always;"></div>

#### **3. grid-template-rows - Rad-struktur**

```css
.grid-container {
  grid-template-rows: 100px 200px auto;  /* 3 rader */
  grid-template-rows: repeat(3, 100px);  /* 3 rader á 100px */
}
```

#### **4. gap - Avstand mellom grid-elementer**

```css
.grid-container {
  gap: 10px;              /* 10px mellom alle elementer */
  row-gap: 10px;          /* 10px mellom rader */
  column-gap: 20px;       /* 20px mellom kolonner */
}
```

<div style="page-break-after:always;"></div>

### 🎯 Grid Item Plassering

#### **1. grid-column og grid-row**

```html
<div class="grid-container">
  <div class="item1">Header</div>
  <div class="item2">Sidebar</div>
  <div class="item3">Main Content</div>
  <div class="item4">Footer</div>
</div>
```

```css
.grid-container {
  display: grid;
  grid-template-columns: 200px 1fr;
  grid-template-rows: auto 1fr auto;
  gap: 10px;
}

.item1 { 
  grid-column: 1 / 3;      /* Strekk over kolonne 1 til 3 (begge kolonner) */
  grid-row: 1;             /* Rad 1 */
}

.item2 { 
  grid-column: 1;          /* Kolonne 1 */
  grid-row: 2;             /* Rad 2 */
}

.item3 { 
  grid-column: 2;          /* Kolonne 2 */
  grid-row: 2;             /* Rad 2 */
}

.item4 { 
  grid-column: 1 / 3;      /* Strekk over begge kolonner */
  grid-row: 3;             /* Rad 3 */
}
```
<div style="page-break-after:always;"></div>

#### **2. grid-area - Navngitte områder**

```css
.grid-container {
  display: grid;
  grid-template-areas: 
    "header header"
    "sidebar main"
    "footer footer";
  grid-template-columns: 200px 1fr;
  grid-template-rows: auto 1fr auto;
}

.header  { grid-area: header; }
.sidebar { grid-area: sidebar; }
.main    { grid-area: main; }
.footer  { grid-area: footer; }
```

<div style="page-break-after:always;"></div>

## 🤝 Bootstrap og CSS Layout Integration

### 🎨 Bootstrap Flexbox Klasser

#### **Grunnleggende Flex Classes**

```html
<!-- Flex Container -->
<div class="d-flex">                    <!-- display: flex -->
<div class="d-inline-flex">             <!-- display: inline-flex -->

<!-- Direction -->
<div class="d-flex flex-row">           <!-- horizontal -->
<div class="d-flex flex-column">        <!-- vertikal -->
<div class="d-flex flex-row-reverse">   <!-- horizontal, omvendt -->
<div class="d-flex flex-column-reverse"> <!-- vertikal, omvendt -->

<!-- Wrap -->
<div class="d-flex flex-wrap">          <!-- wrap elementer -->
<div class="d-flex flex-nowrap">        <!-- ikke wrap -->

<!-- Justify Content (hovedakse) -->
<div class="d-flex justify-content-start">
<div class="d-flex justify-content-end">
<div class="d-flex justify-content-center">
<div class="d-flex justify-content-between">
<div class="d-flex justify-content-around">
<div class="d-flex justify-content-evenly">

<!-- Align Items (kryss-akse) -->
<div class="d-flex align-items-start">
<div class="d-flex align-items-end">
<div class="d-flex align-items-center">
<div class="d-flex align-items-baseline">
<div class="d-flex align-items-stretch">

<!-- Align Self (individuell justering) -->
<div class="align-self-start">
<div class="align-self-end">
<div class="align-self-center">
```
<div style="page-break-after:always;"></div>

#### **Bootstrap Gap Classes**

```html
<!-- Gap mellom flex/grid elementer -->
<div class="d-flex gap-0">    <!-- 0px -->
<div class="d-flex gap-1">    <!-- 4px -->
<div class="d-flex gap-2">    <!-- 8px -->
<div class="d-flex gap-3">    <!-- 16px -->
<div class="d-flex gap-4">    <!-- 24px -->
<div class="d-flex gap-5">    <!-- 48px -->
```
<div style="page-break-after:always;"></div>

### 🏗️ Bootstrap Grid System vs CSS Grid

#### **Bootstrap Grid (Flexbox-basert)**

```html
<!-- Bootstrap Grid - 12 kolonner system -->
<div class="container">
  <div class="row">
    <div class="col-md-4">1/3 bredde på medium+ skjermer</div>
    <div class="col-md-8">2/3 bredde på medium+ skjermer</div>
  </div>
  <div class="row">
    <div class="col-6 col-md-4">50% på mobil, 33% på desktop</div>
    <div class="col-6 col-md-8">50% på mobil, 67% på desktop</div>
  </div>
</div>
```

#### **CSS Grid (Native Grid)**

```html
<!-- CSS Grid - Mer fleksibel struktur -->
<div class="custom-grid">
  <header>Header</header>
  <nav>Navigasjon</nav>
  <main>Hovedinnhold</main>
  <aside>Sidebar</aside>
  <footer>Footer</footer>
</div>
```

```css
.custom-grid {
  display: grid;
  grid-template-areas: 
    "header header header"
    "nav main aside"
    "footer footer footer";
  grid-template-columns: 200px 1fr 200px;
  grid-template-rows: auto 1fr auto;
  min-height: 100vh;
  gap: 1rem;
}

header { grid-area: header; }
nav { grid-area: nav; }
main { grid-area: main; }
aside { grid-area: aside; }
footer { grid-area: footer; }
```

<div style="page-break-after:always;"></div>

## 📊 Praktiske Eksempler og Use Cases

### 🎯 Eksempel 1: Navbar med Flexbox

```html
<!-- Responsive navbar med flexbox -->
<nav class="navbar bg-primary">
  <div class="container-fluid d-flex justify-content-between align-items-center">
    <!-- Logo/Brand -->
    <a class="navbar-brand text-white fw-bold">
      MyApp
    </a>
    
    <!-- Navigasjonslenker -->
    <div class="d-flex gap-3">
      <a href="#" class="text-white text-decoration-none">Hjem</a>
      <a href="#" class="text-white text-decoration-none">Om oss</a>
      <a href="#" class="text-white text-decoration-none">Kontakt</a>
    </div>
    
    <!-- Bruker-meny -->
    <div class="d-flex align-items-center gap-2">
      <span class="text-white">Bruker</span>
      <button class="btn btn-outline-light btn-sm">Logg ut</button>
    </div>
  </div>
</nav>
```
<div style="page-break-after:always;"></div>

### 🃏 Eksempel 2: Card Layout med Flexbox

```html
<!-- Responsive card grid med flexbox -->
<div class="container my-4">
  <div class="d-flex flex-wrap gap-3 justify-content-center">
    <div class="card" style="width: 300px;">
      <div class="card-body d-flex flex-column">
        <h5 class="card-title">Produkt 1</h5>
        <p class="card-text flex-grow-1">Beskrivelse av produktet...</p>
        <div class="d-flex justify-content-between align-items-center mt-auto">
          <span class="fw-bold">299 kr</span>
          <button class="btn btn-primary btn-sm">Kjøp</button>
        </div>
      </div>
    </div>
    
    <div class="card" style="width: 300px;">
      <div class="card-body d-flex flex-column">
        <h5 class="card-title">Produkt 2</h5>
        <p class="card-text flex-grow-1">Lengre beskrivelse av et annet produkt som tar mer plass...</p>
        <div class="d-flex justify-content-between align-items-center mt-auto">
          <span class="fw-bold">399 kr</span>
          <button class="btn btn-primary btn-sm">Kjøp</button>
        </div>
      </div>
    </div>
  </div>
</div>
```
<div style="page-break-after:always;"></div>

### 📱 Eksempel 3: Responsive Dashboard med CSS Grid

```html
<!-- Dashboard layout med CSS Grid -->
<div class="dashboard-grid">
  <header class="dashboard-header">
    <h1>Dashboard</h1>
  </header>
  
  <nav class="dashboard-nav">
    <ul class="nav flex-column">
      <li class="nav-item">
        <a class="nav-link">Oversikt</a>
      </li>
      <li class="nav-item">
        <a class="nav-link">Rapporter</a>
      </li>
    </ul>
  </nav>
  
  <main class="dashboard-main">
    <div class="row g-3">
      <div class="col-md-6">
        <div class="card">
          <div class="card-body">Statistikk 1</div>
        </div>
      </div>
      <div class="col-md-6">
        <div class="card">
          <div class="card-body">Statistikk 2</div>
        </div>
      </div>
    </div>
  </main>
  
  <aside class="dashboard-sidebar">
    <div class="card">
      <div class="card-body">Notifikasjoner</div>
    </div>
  </aside>
</div>
```

```css
.dashboard-grid {
  display: grid;
  grid-template-areas: 
    "header header header"
    "nav main sidebar"
    "nav main sidebar";
  grid-template-columns: 200px 1fr 250px;
  grid-template-rows: auto 1fr;
  min-height: 100vh;
  gap: 1rem;
}

.dashboard-header { 
  grid-area: header; 
  background: var(--bs-primary);
  color: white;
  padding: 1rem;
}

.dashboard-nav { 
  grid-area: nav; 
  background: var(--bs-light);
  padding: 1rem;
}

.dashboard-main { 
  grid-area: main; 
  padding: 1rem;
}

.dashboard-sidebar { 
  grid-area: sidebar; 
  background: var(--bs-light);
  padding: 1rem;
}

/* Responsive - kollapser på små skjermer */
@media (max-width: 768px) {
  .dashboard-grid {
    grid-template-areas: 
      "header"
      "nav"
      "main"
      "sidebar";
    grid-template-columns: 1fr;
  }
}
```
<div style="page-break-after:always;"></div>

### 🎨 Eksempel 4: Form Layout med Flexbox

```html
<!-- Avansert skjema med flexbox -->
<form class="container my-4">
  <!-- Header med flexbox -->
  <div class="d-flex justify-content-between align-items-center mb-4">
    <h2>Brukerregistrering</h2>
    <button type="button" class="btn btn-outline-secondary">Avbryt</button>
  </div>
  
  <!-- To-kolonne layout -->
  <div class="row g-3">
    <div class="col-md-6">
      <div class="d-flex flex-column gap-3">
        <div>
          <label class="form-label">Fornavn</label>
          <input type="text" class="form-control">
        </div>
        <div>
          <label class="form-label">E-post</label>
          <input type="email" class="form-control">
        </div>
      </div>
    </div>
    
    <div class="col-md-6">
      <div class="d-flex flex-column gap-3">
        <div>
          <label class="form-label">Etternavn</label>
          <input type="text" class="form-control">
        </div>
        <div>
          <label class="form-label">Telefon</label>
          <input type="tel" class="form-control">
        </div>
      </div>
    </div>
  </div>
  
  <!-- Button footer med flexbox -->
  <div class="d-flex justify-content-end gap-2 mt-4">
    <button type="button" class="btn btn-secondary">Tilbake</button>
    <button type="submit" class="btn btn-primary">Registrer</button>
  </div>
</form>
```

<div style="page-break-after:always;"></div>

## 🎲 Når Bruke Flexbox vs Grid

### ✅ Bruk Flexbox når:

1. **Enkel dimensjon:** Du jobber med én retning (horizontal ELLER vertikal)
2. **Innholdsdrevet:** Størrelsen avhenger av innholdet
3. **Komponentlayout:** Navigasjoner, knapp-grupper, kort-innhold
4. **Justering:** Du trenger presis kontroll over justering og fordeling
5. **Responsive komponenter:** Elementer som skal tilpasse seg innhold

```html
<!-- Perfekt for flexbox -->
<div class="d-flex justify-content-between align-items-center">
  <h3>Tittel</h3>
  <button class="btn btn-primary">Handling</button>
</div>

<div class="d-flex gap-2 flex-wrap">
  <span class="badge bg-primary">Tag 1</span>
  <span class="badge bg-secondary">Tag 2</span>
  <span class="badge bg-success">Tag 3</span>
</div>
```

### ✅ Bruk CSS Grid når:

1. **To dimensjoner:** Du trenger kontroll over både rader OG kolonner
2. **Layout-struktur:** Hele sideoppsettet eller store seksjoner
3. **Overlappende elementer:** Elementer som skal overlappe hverandre
4. **Kompleks posisjonering:** Avanserte layoutmønstre
5. **Design-drevet:** Størrelsen er bestemt av designet, ikke innholdet

```html
<!-- Perfekt for CSS Grid -->
<div class="page-layout">
  <header>Header</header>
  <nav>Navigation</nav>
  <main>Main Content</main>
  <aside>Sidebar</aside>
  <footer>Footer</footer>
</div>
```
<div style="page-break-after:always;"></div>

### 🤝 Kombinere Flexbox og Grid

```html
<!-- Grid for overordnet layout -->
<div class="dashboard-grid">
  <header class="dashboard-header">
    <!-- Flexbox for header-innhold -->
    <div class="d-flex justify-content-between align-items-center">
      <h1>Dashboard</h1>
      <div class="d-flex gap-2">
        <button class="btn btn-outline-light btn-sm">Innstillinger</button>
        <button class="btn btn-light btn-sm">Profil</button>
      </div>
    </div>
  </header>
  
  <main class="dashboard-main">
    <!-- Bootstrap grid for innhold -->
    <div class="row g-3">
      <div class="col-md-8">
        <div class="card">
          <div class="card-body">
            <!-- Flexbox for kort-innhold -->
            <div class="d-flex justify-content-between align-items-start mb-3">
              <h5 class="card-title">Statistikk</h5>
              <button class="btn btn-outline-primary btn-sm">Oppdater</button>
            </div>
            <p class="card-text">Innhold her...</p>
          </div>
        </div>
      </div>
    </div>
  </main>
</div>
```

<div style="page-break-after:always;"></div>

## 🛠️ Bootstrap Utilities for Layout

### 📐 Display Utilities

```html
<!-- Display kontroll -->
<div class="d-none">Skjult</div>
<div class="d-block">Block</div>
<div class="d-inline">Inline</div>
<div class="d-inline-block">Inline-block</div>
<div class="d-flex">Flex</div>
<div class="d-grid">Grid</div>

<!-- Responsive display -->
<div class="d-none d-md-block">Skjult på mobil, synlig på desktop</div>
<div class="d-block d-md-none">Synlig på mobil, skjult på desktop</div>
```

### 📏 Sizing Utilities

```html
<!-- Bredde -->
<div class="w-25">25% bredde</div>
<div class="w-50">50% bredde</div>
<div class="w-75">75% bredde</div>
<div class="w-100">100% bredde</div>
<div class="w-auto">Auto bredde</div>

<!-- Høyde -->
<div class="h-25">25% høyde</div>
<div class="h-50">50% høyde</div>
<div class="h-75">75% høyde</div>
<div class="h-100">100% høyde</div>

<!-- Min/Max størrelser -->
<div class="min-vw-100">Min 100% viewport bredde</div>
<div class="min-vh-100">Min 100% viewport høyde</div>
```

### 🎯 Position Utilities

```html
<!-- Position -->
<div class="position-static">Static</div>
<div class="position-relative">Relative</div>
<div class="position-absolute">Absolute</div>
<div class="position-fixed">Fixed</div>
<div class="position-sticky">Sticky</div>

<!-- Positioning -->
<div class="position-absolute top-0 start-0">Øverst til venstre</div>
<div class="position-absolute top-0 end-0">Øverst til høyre</div>
<div class="position-absolute bottom-0 start-0">Nederst til venstre</div>
<div class="position-absolute bottom-0 end-0">Nederst til høyre</div>
```

<div style="page-break-after:always;"></div>

## 🎨 Praktiske Tips og Tricks

### 💡 Flexbox Tips

```html
<!-- Perfekt sentrering -->
<div class="d-flex justify-content-center align-items-center min-vh-100">
  <div class="text-center">
    <h1>Perfekt sentrert</h1>
    <p>Både horisontalt og vertikalt</p>
  </div>
</div>

<!-- Sticky footer med flexbox -->
<div class="d-flex flex-column min-vh-100">
  <header class="bg-primary text-white p-3">Header</header>
  <main class="flex-grow-1 p-3">Hovedinnhold som vokser</main>
  <footer class="bg-dark text-white p-3">Footer som holder seg nederst</footer>
</div>

<!-- Equal height cards -->
<div class="row g-3">
  <div class="col-md-4 d-flex">
    <div class="card flex-fill">
      <div class="card-body d-flex flex-column">
        <h5 class="card-title">Kort 1</h5>
        <p class="card-text flex-grow-1">Kort innhold</p>
        <button class="btn btn-primary mt-auto">Handling</button>
      </div>
    </div>
  </div>
</div>
```

<div style="page-break-after:always;"></div>

### 🏗️ CSS Grid Tips

```css
/* Responsive grid uten media queries */
.auto-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 1rem;
}

/* Masonry-lignende layout */
.masonry-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  grid-auto-rows: max-content;
  gap: 1rem;
}

/* Overlappende elementer */
.overlap-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-template-rows: 1fr 1fr;
}

.overlap-item {
  grid-column: 1 / 3;
  grid-row: 1 / 3;
  z-index: 2;
}
```

<div style="page-break-after:always;"></div>

## 📚 Sammendrag og Beste Praksis

### ✅ **Husk dette:**

1. **Mobile-first:** Start alltid med mobil layout, utvid til desktop
2. **Semantisk HTML:** Bruk riktige HTML-tags før du legger til layout-klasser
3. **Bootstrap først:** Bruk Bootstrap-klasser når mulig, custom CSS når nødvendig
4. **Flexbox for komponenter:** Navigasjoner, knapp-grupper, kort-innhold
5. **Grid for layout:** Sidestrukturer, dashboards, komplekse layouts
6. **Test responsivt:** Sjekk alltid på ulike skjermstørrelser

### 🎯 **Layout Hierarchy:**

```
1. Bootstrap Grid System (container/row/col)
   ↓
2. CSS Grid (for komplekse layouts)
   ↓
3. Flexbox (for komponent-justering)
   ↓
4. Spacing utilities (margin/padding)
```

### 📱 **Responsive Design Checklist:**

- ✅ Mobile-first design
- ✅ Bruk Bootstrap breakpoints
- ✅ Test på ulike enheter
- ✅ Optimaliser touch-targets (minst 44px)
- ✅ Readable text på alle skjermstørrelser
- ✅ Tilgjengelig navigasjon på mobil

