
# Blazor + `<div>` for Layout: Grid og Flex (med Bootstrap)

Denne guiden forklarer **hvordan `<div>`-elementer brukes i Blazor** for å bygge layout ved hjelp av **Bootstrap sitt grid- og flex-system**, og gir en **trinnvis forklaring** av koden.

> **Kort oppsummert:** Blazor rendrer HTML i nettleseren. Selve layouten styres ikke av Blazor, men av **CSS** – typisk **Bootstrap**. Du bruker `<div>`-elementer med **Bootstrap-klasser** for å definere rader, kolonner, fleksbokser, spacing, farger osv. Blazor sin jobb er å plassere komponenter/innhold (`@Body`, child components) inne i denne strukturen.

---

## 1) Hvor passer `<div>` inn i Blazor?

- En **Blazor-komponent** (`.razor`-fil) produserer HTML. `<div>` er den vanligste beholderen for å **gruppere og strukturere** innhold.
- **Layout-komponenter** (f.eks. `MainLayout.razor`) definerer rammen for appen (header/sidepanel/innhold). Inni layouten plasserer Blazor sidenes innhold via `@Body`.
- **CSS-rammeverk** som **Bootstrap** gir klassene du bruker på `<div>` for grid, flex og spacing.  
  *Blazor → HTML → (Bootstrap) CSS styrer layout.*

---

## 2) Bootstrap-grid: container ➜ row ➜ col-*

Grid-systemet i Bootstrap følger alltid **container → row → col**-mønsteret:

- **`.container` / `.container-fluid`**: Ytterste beholder. `-fluid` er full bredde (100%).  
- **`.row`**: Samler kolonner på en rad og håndterer «gutters» (mellomrom mellom kolonner).
- **`.col-*`**: Selve kolonnene. Du kan spesifisere bredder per breakpoint: `col-12`, `col-md-6`, `col-lg-4` osv.

**Breakpoints (vanligst):**

| Prefix | Min bredde | Brukes ofte til |
|-------:|------------|------------------|
| `sm`   | 576 px     | Store mobiler    |
| `md`   | 768 px     | Nettbrett        |
| `lg`   | 992 px     | Små laptop       |
| `xl`   | 1200 px    | Laptop/desktop   |
| `xxl`  | 1400 px    | Store skjermer   |

**Eksempel:**  
`col-md-3 col-lg-2` betyr: fra **md** (≥768px) tar kolonnen **3/12** (25%), fra **lg** (≥992px) tar den **2/12** (~16,7%). Under md (mobil) blir den full bredde (stacker over/under).

<div style="page-break-after:always;"></div>

## 3) Flexbox i Bootstrap (utility-klasser)

[Learn CSS Flexbox in 20 Minutes](https://www.youtube.com/watch?v=wsTv9y931o8&t=23s)

Bootstrap gir utility-klasser for **Flexbox** slik at du slipper å skrive egen CSS:

- **`d-flex`**: Slår på flex (`display:flex`)
- **`flex-row` / `flex-column`**: Retning horisontalt/vertikalt
- **`flex-grow-1`**: La elementet vokse og fylle tilgjengelig plass
- **`align-items-*` / `justify-content-*`**: Justering på tvers/langs hovedaksen
- **`h-100`**: Høyde 100% av forelder
- **`min-vh-100`**: Minimum **viewport-høyde** 100% (full skjermhøyde)
- **`g-0`**: Ingen «gutter» (kolonnemellomrom) på raden
- **`p-0`, `p-3`, `m-3`, `mb-1`**: Padding/margin-utilities

<div style="page-break-after:always;"></div>

## 4) Din kode – linje for linje

**Koden:**

```html
<div class="container-fluid p-0">
    <div class="row g-0 min-vh-100">
        <!-- Venstre kolonne - Navigasjon -->
        <div class="col-md-3 col-lg-2 bg-dark">
            <div class="d-flex flex-column h-100">
                <div class="bg-primary text-white p-3">
                    <h5 class="mb-0">BlazorPlayGround</h5>
                </div>
                
                <nav class="flex-grow-1 p-3">
                    <ul class="nav nav-pills flex-column">
                        <li class="nav-item mb-1">
                            <a href="/" class="nav-link text-white">🏠 Hjem</a>
                        </li>
                        <li class="nav-item mb-1">
                            <a href="/01/Hello" class="nav-link text-white">👋 Hello</a>
                        </li>
                        <li class="nav-item mb-1">
                            <a href="/01/Hello2" class="nav-link text-white">👋 Hello2</a>
                        </li>
                        <li class="nav-item mb-1">
                            <a href="/03/counterplus" class="nav-link text-white">� CounterPlus</a>
                        </li>
                        <li class="nav-item mb-1">
                            <a href="/03/RoutingLab" class="nav-link text-white">🧭 RoutingLab</a>
                        </li>
                    </ul>
                </nav>
            </div>
        </div>
        
        <!-- Høyre kolonne - Hovedinnhold -->
        <div class="col-md-9 col-lg-10 bg-light">
            <main class="p-4">
                @Body
            </main>
        </div>
    </div>
</div>

<div id="blazor-error-ui" data-nosnippet>
    An unhandled error has occurred.
    <a href="." class="reload">Reload</a>
    <span class="dismiss">🗙</span>
</div>
```

**Forklaring:**

1. **`<div class="container-fluid p-0">`**  
   - `container-fluid`: full bredde (tar hele viewportens bredde).  
   - `p-0`: null padding, altså innholdet starter helt i kanten.

2. **`<div class="row g-0 min-vh-100">`**  
   - `row`: oppretter en grid-rad med støtte for kolonner under.  
   - `g-0`: fjerner default «gutter» (mellomrom) mellom kolonner.  
   - `min-vh-100`: gjør raden minst like høy som **hele** viewporten (100% av skjermhøyden).  
     → Dette gir en fullhøyde to-kolonne-layout.

3. **Venstre kolonne (`<div class="col-md-3 col-lg-2 bg-dark">`)**  
   - Bredde: 100% på mobil, **3/12** fra md (≥768px), **2/12** fra lg (≥992px).  
   - `bg-dark`: mørk bakgrunn for sidepanel.  
   - Innholdet er igjen en beholder med **flex**:  
     - `d-flex flex-column h-100`: vertikal stabling, høyde = 100% av kolonnen.  
       - Headerstripe (`bg-primary text-white p-3`) med tittel.  
       - `nav.flex-grow-1 p-3`: navigasjonen vokser for å fylle resten av høyden.  
         - `ul.nav.nav-pills.flex-column`: vertikal pill-navigering.  
         - `li.nav-item` + `a.nav-link`: Bootstrap-nav-elementer. `text-white` gir hvit tekst.

4. **Høyre kolonne (`<div class="col-md-9 col-lg-10 bg-light">`)**  
   - Tar resten av bredden: **9/12** fra md og **10/12** fra lg.  
   - `bg-light`: lys bakgrunn for innholdet.  
   - `main.p-4`: hovedinnhold med padding.  
   - `@Body`: **Blazor-plassholder** – her rendres innholdet til den aktive siden. I en layout-komponent (`MainLayout.razor`) betyr det: *«Sett inn siden som navigasjonen peker til her»*.

5. **`<div id="blazor-error-ui" ...>`**  
   - Default Blazor-feilbanner som vises når det oppstår en **unhandled error**.  
   - Har «Reload»-lenke og en knapp for å skjule. `data-nosnippet` hindrer at søkemotorer siterer feilmeldingen.

**Merk om `� CounterPlus`**: Dette ser ut som en **encoding-feil** i emojis/tegnsett. Bytt `�` til riktig emoji (f.eks. `⚡`) eller fjern det.

<div style="page-break-after:always;"></div>

## 5) Hvordan plassere dette i en Blazor-layout

I en standard Blazor-app (Server, WASM eller Web App) har du en **layout-komponent** (typisk `MainLayout.razor`). Her er et eksempel på hvordan koden passer inn:

```razor
@inherits LayoutComponentBase

<div class=\"container-fluid p-0\">
  <div class=\"row g-0 min-vh-100\">
    <div class=\"col-md-3 col-lg-2 bg-dark\">
      <!-- sidebar content -->
    </div>
    <div class=\"col-md-9 col-lg-10 bg-light\">
      <main class=\"p-4\">
        @Body
      </main>
    </div>
  </div>
</div>
```

Og i en side/komponent som skal bruke denne layouten:

```razor
@page \"/\"
@layout MainLayout

<h1>Home</h1>
<p>Welcome!</p>
```

> I Blazor Web App (.NET 8+) kan du ha layout både som «classic» layout eller som **Razor Components** under `Components/`-mappen. Prinsippet er det samme: `@Body` (eller `@Body` via `LayoutView`) er stedet siden rendres.

<div style="page-break-after:always;"></div>

## 6) Oversikt: Nyttige Bootstrap-klasser for layout

### Grid og kolonner

| Klasse | Effekt |
|-------|--------|
| `container` / `container-fluid` | Beholder (fast bredde eller full bredde) |
| `row` | Rad som holder kolonner |
| `col`, `col-6`, `col-md-3`, `col-lg-2` | Kolonne, evt. bredde per breakpoint |
| `g-0` / `g-1` / … / `g-5` | Gutter (mellomrom) på rader og kolonner |

### Flexbox

| Klasse | Effekt |
|-------|--------|
| `d-flex` | `display:flex` |
| `flex-row` / `flex-column` | Retning |
| `flex-wrap` | Tillat linjebryting |
| `flex-grow-1` | La elementet vokse |
| `align-items-start/center/end/stretch` | Tverr-akse justering |
| `justify-content-start/center/end/between/around` | Hoved-akse justering |

### Størrelse og høyde

| Klasse | Effekt |
|-------|--------|
| `h-100` / `w-100` | 100% høyde/bredde av forelder |
| `min-vh-100` / `min-vw-100` | Minst full viewport høyde/bredde |

### Spacing (marg/padding)

| Klasse | Effekt |
|-------|--------|
| `p-0` … `p-5` | Padding (alle sider) |
| `pt-3`, `px-4`, `py-2` | Padding spesifikk akse/side |
| `m-0` … `m-5` | Margin |
| `mb-0`, `mt-1` osv. | Margin på spesifikke sider |

### Farger og tekst

| Klasse | Effekt |
|-------|--------|
| `bg-dark`, `bg-light`, `bg-primary`, `bg-secondary` | Bakgrunnsfarger |
| `text-white`, `text-dark`, `text-muted` | Tekstfarger |
| `nav`, `nav-pills`, `nav-link`, `nav-item` | Navigasjonskomponenter |

<div style="page-break-after:always;"></div>

## 7) Responsive strategier i praksis

- **Start mobil-først**: Bruk `col-12` som default, utvid med `col-md-*`, `col-lg-*` osv.
- **Skjul/vis ved breakpoint**: `d-none d-md-block` (skjul på mobil, vis på md+).
- **Fiksert sidebar på store skjermer**: `col-lg-2` + `position-sticky` inni med `top-0` for sticky meny.

**Eksempel (sticky sidebar):**

```html
<div class=\"col-lg-2 bg-dark\">
  <div class=\"position-sticky top-0 vh-100 overflow-auto p-3\">
    <!-- sidebar content -->
  </div>
</div>
```

---

## 8) Vanlige fallgruver og tips

- **Glemte rader**: Ikke legg `col-*` direkte inni `container`, bruk alltid `row` som mellomsteg.
- **Gutter-kollisjon**: Når du bruker `g-0`, mister du luft mellom kolonner. Legg evt. padding på innholdet.
- **Full høyde**: For å få kolonner som strekker seg i høyden, bruk `min-vh-100` på en `row` eller `h-100` i kombinasjon med at forelder har definert høyde.
- **Encoding/emoji**: Pass på tegnsett (UTF-8). Bytt ut ukjente symboler (`�`).

<div style="page-break-after:always;"></div>

## 9) Komplett layout-mal (kopierbar)

```razor
@inherits LayoutComponentBase

<div class=\"container-fluid p-0\">
  <div class=\"row g-0 min-vh-100\">
    <!-- Sidebar -->
    <div class=\"col-md-3 col-lg-2 bg-dark\">
      <div class=\"d-flex flex-column h-100\">
        <div class=\"bg-primary text-white p-3\">
          <h5 class=\"mb-0\">BlazorPlayGround</h5>
        </div>
        <nav class=\"flex-grow-1 p-3\">
          <ul class=\"nav nav-pills flex-column\">
            <li class=\"nav-item mb-1\">
              <a href=\"/\" class=\"nav-link text-white\">🏠 Home</a>
            </li>
            <li class=\"nav-item mb-1\">
              <a href=\"/01/Hello\" class=\"nav-link text-white\">👋 Hello</a>
            </li>
            <li class=\"nav-item mb-1\">
              <a href=\"/01/Hello2\" class=\"nav-link text-white\">👋 Hello2</a>
            </li>
            <li class=\"nav-item mb-1\">
              <a href=\"/03/counterplus\" class=\"nav-link text-white\">⚡ CounterPlus</a>
            </li>
            <li class=\"nav-item mb-1\">
              <a href=\"/03/RoutingLab\" class=\"nav-link text-white\">🧭 RoutingLab</a>
            </li>
          </ul>
        </nav>
      </div>
    </div>

    <!-- Main content -->
    <div class=\"col-md-9 col-lg-10 bg-light\">
      <main class=\"p-4\">
        @Body
      </main>
    </div>
  </div>
</div>

<div id=\"blazor-error-ui\" data-nosnippet>
  An unhandled error has occurred.
  <a href=\".\" class=\"reload\">Reload</a>
  <span class=\"dismiss\">🗙</span>
</div>
```

<div style="page-break-after:always;"></div>

## 10) Sjekkliste før produksjon

- [ ] Bootstrap er lastet (CSS og ev. JS/Bundle)
- [ ] Grid-struktur følger `container(-fluid) ➜ row ➜ col-*`
- [ ] Riktig breakpoint-bredde for sidebar/innhold (`col-md-3 col-lg-2` og `col-md-9 col-lg-10`)
- [ ] `min-vh-100` eller tilsvarende for fullhøyde
- [ ] Konsekvent spacing (`p-*`, `m-*`) og gutter (`g-*`)
- [ ] Kontraster (tekst/bakgrunn) tilfredsstiller tilgjengelighet

---

### Bonus: Hurtigreferanse

```text
Containers:        container | container-fluid
Rows:              row
Columns:           col | col-6 | col-md-4 | col-lg-3 | ...
Gutters:           g-0 .. g-5
Flex:              d-flex | flex-column | flex-row | flex-grow-1 | align-items-center | justify-content-between
Sizing/Height:     h-100 | w-100 | min-vh-100 | min-vw-100
Spacing:           p-0..5 | m-0..5 | px-3 | py-2 | mb-1 | mt-3
Colors/Text:       bg-dark | bg-light | bg-primary | text-white | text-muted
Navigation:        nav | nav-pills | nav-item | nav-link
Position/Sticky:   position-sticky | top-0 | vh-100 | overflow-auto
```

---

**Oppsummert:**  

- `<div>` er grunnsteinen i layouten.  
- **Bootstrap** gir verktøyene (grid og flex).  
- **Blazor** sørger for at riktig innhold rendres på rett sted (`@Body`, komponenter), mens **CSS** styrer hvor og hvordan det ser ut.
